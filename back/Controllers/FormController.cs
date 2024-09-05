using Back.Models;
using Back.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Back.Services;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient httpClient;
        private readonly IInterpolService interpolService;

        public ApplicantController(ApplicationDbContext context, HttpClient httpClient, IInterpolService interpolService)
        {
            _context = context;
            this.httpClient = httpClient;
            this.interpolService = interpolService;
        }
        [HttpGet("trial")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllApplicantsTrial()
        {
            return await _context.Applications.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetAllApplications()
        {
            var applications = await _context.Applications
        .Include(a => a.Applicant)
            .ThenInclude(ap => ap.Passport)
        .Include(a => a.Applicant)
            .ThenInclude(ap => ap.Spouse)
        .Include(a => a.Applicant)
            .ThenInclude(ap => ap.Histories)
        .ToListAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true // Optional for more readable output
            };

            return new JsonResult(applications, options);
        }
        // POST: api/Applicant
        [HttpPost]
        public async Task<IActionResult> CreateApplicant([FromBody] FormDTO formDTO)
        {
            if (formDTO == null || formDTO.Applicant == null || formDTO.Application == null)
            {
                return BadRequest("Invalid form data.");
            }

            // Start a transaction to ensure atomic operations
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Check if Passport already exists, if not create a new one
                var passport = await _context.Passports
                    .FirstOrDefaultAsync(p => p.Id == formDTO.Passport.Id && p.Country == formDTO.Applicant.Nationality);

                if (passport == null)
                {
                    if (string.IsNullOrEmpty(formDTO.Passport.Id))
                    {
                        return BadRequest("Passport Id is required."); // Validation for missing Id
                    }

                    passport = new Passport
                    {
                        Id = formDTO.Passport.Id, // Ensure Id is set
                        Country = formDTO.Applicant.Nationality,
                        DateOfExpire = formDTO.Passport.DateOfExpire,
                        DateOfIssue = formDTO.Passport.DateOfIssue,
                        ApplicantNIC = formDTO.Applicant.NIC,
                        ApplicantNationality = formDTO.Applicant.Nationality,
                    };
                    _context.Passports.Add(passport);
                }
                else
                {
                    // Update the existing passport if necessary
                    passport.DateOfExpire = formDTO.Passport.DateOfExpire;
                    passport.DateOfIssue = formDTO.Passport.DateOfIssue;
                    _context.Passports.Update(passport);
                }


                // Create Spouse entity if present
                Spouse spouse = null;
                if (formDTO.Spouse != null)
                {
                    spouse = new Spouse
                    {
                        ApplicantNIC = formDTO.Applicant.NIC,
                        ApplicantNationality = formDTO.Applicant.Nationality,
                        SpouseNIC = formDTO.Spouse.SpouseNIC,
                        Name = formDTO.Spouse.Name,
                        Address = formDTO.Spouse.Address,
                    };
                    _context.Spouses.Add(spouse);
                }

                // Create History entities
                var histories = formDTO.History.Select(h => new History
                {
                    VisaType = h.VisaType,
                    VisaIssuedDate = h.VisaIssuedDate,
                    VisaValidityPeriod = h.VisaValidityPeriod,
                    DateLeaving = h.DateLeaving,
                    LastLocation = h.LastLocation,
                    ApplicantNIC = formDTO.Applicant.NIC,
                    ApplicantNationality = formDTO.Applicant.Nationality,
                }).ToList();
                _context.Histories.AddRange(histories);

                // Create Applicant entity
                var applicant = new Applicant
                {
                    NIC = formDTO.Applicant.NIC,
                    Nationality = formDTO.Applicant.Nationality,
                    FullName = formDTO.Applicant.FullName,
                    Gender = formDTO.Applicant.Gender,
                    BirthDate = formDTO.Applicant.BirthDate,
                    BirthPlace = formDTO.Applicant.BirthPlace,
                    Height = formDTO.Applicant.Height,
                    Address = formDTO.Applicant.Address,
                    TelNo = formDTO.Applicant.TelNo,
                    Email = formDTO.Applicant.Email,
                    Occupation = formDTO.Applicant.Occupation,
                    OccupationAddress = formDTO.Applicant.OccupationAddress,
                    Passport = passport,  // Set Passport relationship
                    Spouse = spouse,      // Set Spouse relationship if exists
                    Histories = histories // Set History relationship
                };
                _context.Applicants.Add(applicant);

                // Create Application entity
                var application = new Application
                {
                    Purpose = formDTO.Application.Purpose,
                    Route = formDTO.Application.Route,
                    TravelMode = formDTO.Application.TravelMode,
                    ArrivalDate = formDTO.Application.ArrivalDate,
                    Period = formDTO.Application.Period,
                    AmountOfMoney = formDTO.Application.AmountOfMoney,
                    MoneyType = formDTO.Application.MoneyType,
                    ApplicantNIC = formDTO.Applicant.NIC,
                    ApplicantNationality = formDTO.Applicant.Nationality,
                    Applicant = applicant  // Set the Applicant relationship
                };
                _context.Applications.Add(application);

                // Call external services for status updates
                string forename = formDTO.Applicant.FullName.Split(' ')[0];
                string surname = formDTO.Applicant.FullName.Split(' ').Last();
                string nationality = formDTO.Applicant.Nationality;
                string gender = formDTO.Applicant.Gender == "Female" ? "F" : "M";

                InterpolDTO interpolObj = new InterpolDTO
                {
                    forename = forename,
                    name = surname,
                    nationality = nationality,
                    gender = gender
                };

                // Check interpol status
                if (await interpolService.CheckRedNoticedApplicant(interpolObj) == "found")
                {
                    application.Status = "Red";
                }
                if (await interpolService.CheckYellowNoticedApplicant(interpolObj) == "found")
                {
                    application.Status = application.Status == "Red" ? "Red, Yellow" : "Yellow";
                }
                if (await interpolService.CheckUNNoticedApplicant(interpolObj) == "found")
                {
                    application.Status = application.Status == "Red, Yellow" ? "Red, Yellow, UN" :
                                         application.Status == "Red" ? "Red, UN" :
                                         application.Status == "Yellow" ? "Yellow, UN" : "UN";
                }
                else if (application.Status == null)
                {
                    application.Status = "clear";
                }

                // Save all changes to the database
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                return Ok(application.Status);
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of an error
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error saving applicant: {ex.Message}");
            }
        }



    }
}
