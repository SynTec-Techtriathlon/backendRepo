using Back.Models;
using Back.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicantController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormDTO>>> GetAllApplicants()
        {
            var applicants = await _context.Applicants
                .Include(a => a.Applications)
                .Include(a => a.Passport)
                .Include(a => a.Spouse)
                .Include(a => a.Histories)
                .ToListAsync();

            var formDTOs = applicants.Select(a => new FormDTO
            {
                Applicant = new ApplicantDTO
                {
                    NIC = a.NIC,
                    Nationality = a.Nationality,
                    FullName = a.FullName,
                    Gender = a.Gender,
                    BirthDate = a.BirthDate,
                    BirthPlace = a.BirthPlace,
                    Height = a.Height,
                    Address = a.Address,
                    TelNo = a.TelNo,
                    Email = a.Email,
                    Occupation = a.Occupation,
                    OccupationAddress = a.OccupationAddress
                },
                Application = a.Applications.Select(ap => new ApplicationDTO
                {
                    Purpose = ap.Purpose,
                    Route = ap.Route,
                    TravelMode = ap.TravelMode,
                    ArrivalDate = ap.ArrivalDate,
                    Period = ap.Period,
                    AmountOfMoney = ap.AmountOfMoney,
                    MoneyType = ap.MoneyType

                }).FirstOrDefault(), // Assuming one application per applicant; adjust if there are multiple
                
                Passport = new PassportDTO
                {
                    Id = a.Passport.Id,
                    DateOfExpire = a.Passport.DateOfExpire,
                    DateOfIssue = a.Passport.DateOfIssue
                },
                Spouse = a.Spouse != null ? new SpouseDTO
                {
                    SpouseNIC = a.Spouse.SpouseNIC,
                    Name = a.Spouse.Name,
                    Address = a.Spouse.Address
                } : null,
                History = a.Histories.Select(h => new HistoryDTO
                {
                    VisaType = h.VisaType,
                    VisaIssuedDate = h.VisaIssuedDate,
                    VisaValidityPeriod = h.VisaValidityPeriod,
                    DateLeaving = h.DateLeaving,
                    LastLocation = h.LastLocation
                }).ToList()
            }).ToList();

            return Ok(formDTOs);
        }
        // POST: api/Applicant
        [HttpPost]
        public async Task<IActionResult> CreateApplicant([FromBody] FormDTO formDTO)
        {
            if (formDTO == null || formDTO.Applicant == null || formDTO.Application == null)
            {
                return BadRequest("Invalid form data.");
            }

            // Check if the applicant already exists
            var existingApplicant = await _context.Applicants
                .Include(a => a.Applications)
                .FirstOrDefaultAsync(a => a.NIC == formDTO.Applicant.NIC && a.Nationality == formDTO.Applicant.Nationality);

            if (existingApplicant != null)
            {
                return Conflict("An applicant with the given NIC and Nationality already exists.");
            }

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
                Applications = new List<Application>()
            };

            // Create new Application entity
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
            };

            // Create new Passport entity
            var passport = new Passport
            {
                Id = formDTO.Passport.Id,
                Country = formDTO.Applicant.Nationality,
                DateOfExpire = formDTO.Passport.DateOfExpire,
                DateOfIssue = formDTO.Passport.DateOfIssue,
                ApplicantNIC = formDTO.Applicant.NIC,
                ApplicantNationality = formDTO.Applicant.Nationality,
            };

            // Create new Spouse entity
            var spouse = new Spouse
            {
                ApplicantNIC = formDTO.Applicant.NIC,
                ApplicantNationality = formDTO.Applicant.Nationality,
                SpouseNIC = formDTO.Spouse.SpouseNIC,
                Name = formDTO.Spouse.Name,
                Address = formDTO.Spouse.Address,
            };

            // Create new History entities and add them to the applicant
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

            // Associate the related entities with the applicant
            applicant.Applications.Add(application);
            applicant.Passport = passport;
            applicant.Spouse = spouse;
            applicant.Histories = histories;

            // Add the applicant (and the related entities) to the database
            _context.Applicants.Add(applicant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateApplicant), new { id = applicant.NIC }, formDTO);
        }
    }
}
