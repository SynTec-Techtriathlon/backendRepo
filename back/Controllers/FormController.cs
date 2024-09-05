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
        //user get by number
        [HttpGet("{no}")]
        public async Task<ActionResult<Application>> GetApplicationById(int no)
        {
            // Fetch the application by the No field (ID)
            var application = await _context.Applications
                .Include(a => a.Applicant)
                    .ThenInclude(ap => ap.Passport)
                .Include(a => a.Applicant)
                    .ThenInclude(ap => ap.Spouse)
                .Include(a => a.Applicant)
                    .ThenInclude(ap => ap.Histories)
                .FirstOrDefaultAsync(a => a.No == no);

            if (application == null)
            {
                return NotFound("Application not found.");
            }

            // Optional: serialize with custom options if needed
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return new JsonResult(application, options);
        }

        // POST: api/Applicant
        [HttpPost]
        public async Task<IActionResult> CreateApplicant([FromBody] FormDTO formDTO)
        {
            if (formDTO == null || formDTO.Applicant == null || formDTO.Application == null)
            {
                return BadRequest("Invalid form data.");
            }

            // Create or find the existing Passport entity
            var passport = await _context.Passports
                .FirstOrDefaultAsync(p => p.Id == formDTO.Passport.Id && p.Country == formDTO.Applicant.Nationality);

            if (passport == null)
            {
                passport = new Passport
                {
                    Id = formDTO.Passport.Id,
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

            // Create the Spouse entity
            var spouse = formDTO.Spouse != null ? new Spouse
            {
                ApplicantNIC = formDTO.Applicant.NIC,
                ApplicantNationality = formDTO.Applicant.Nationality,
                SpouseNIC = formDTO.Spouse.SpouseNIC,
                Name = formDTO.Spouse.Name,
                Address = formDTO.Spouse.Address,
            } : null;

            // Create the History entities
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

            // Create new Applicant entity
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
            };
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
                CreatedAt = DateTime.Now,
            };


            // Extract form data
            string forename = formDTO.Applicant.FullName.Split(' ')[0];  // Assuming forename is the first part of FullName
            string surname = formDTO.Applicant.FullName.Split(' ').Last(); // Assuming surname is the last part of FullName
            string nationality = formDTO.Applicant.Nationality;
            int ageMin = 18; // Default value, adjust as needed
            int ageMax = 120; // Default value, adjust as needed
            string gender = formDTO.Applicant.Gender == "Female" ? "F" : "M"; // Assuming "F" for Female and "M" for Male

            InterpolDTO interpolObj = new InterpolDTO
            {
                forename = forename,
                name = surname,
                nationality = nationality,
                gender = gender
            };

            if(await interpolService.CheckRedNoticedApplicant(interpolObj) == "found")
            {
               application.Status = "Red";
            }
            if(await interpolService.CheckYellowNoticedApplicant(interpolObj) == "found")
            {
                if(application.Status == "Red")
                {
                    application.Status = "Red, Yellow";
                }
                else
                {
                    application.Status = "Yellow";
                }
            }
            if(await interpolService.CheckUNNoticedApplicant(interpolObj) == "found")
            {
                if(application.Status == "not found" +
                    "")
                {
                    application.Status = "UN";
                }
                else
                {
                    application.Status = application.Status+", UN";
                }
            }
            else if(await interpolService.CheckUNNoticedApplicant(interpolObj) == "not found" && 
                    await interpolService.CheckYellowNoticedApplicant(interpolObj) == "not found" &&
                    await interpolService.CheckRedNoticedApplicant(interpolObj) == "not found")
            {
                application.Status = "clear";
            }


            _context.Applicants.Add(applicant);
            _context.Applications.Add(application);
            if (spouse != null)
            {
                _context.Spouses.Add(spouse);
            }
            if (histories.Count > 0)
            {
                _context.Histories.AddRange(histories);
            }
            _context.Passports.Add(passport);
            await _context.SaveChangesAsync();


            return Ok(application.Status);
        }


    }
}
