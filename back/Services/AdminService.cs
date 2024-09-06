using Microsoft.EntityFrameworkCore;

namespace Back.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        public readonly IConfiguration _configuration;
        public AdminService(ApplicationDbContext context, IEmailService emailService, IConfiguration configuration) { 
            this._context = context;
            this._emailService = emailService;
            this._configuration = configuration;
        }
        public async Task<string> ApproveApplication(int request)
        {
            var Application = await _context.Applications.FindAsync(request);
            var Applicant = await _context.Applicants.FirstOrDefaultAsync(u => u.NIC == Application.ApplicantNIC && u.Nationality == Application.ApplicantNationality);
            if (Application == null)
            {
                throw new Exception("Invalid Email");
            }
            _emailService.ApproveUserMail(Applicant.Email, Applicant.FullName);
            Application.Status = "Approve";
            await _context.SaveChangesAsync();
            return ("Approved successfully");
        }

        public async Task<string> RejectApplication(int request)
        {
            var Application = await _context.Applications.FindAsync(request);
            var Applicant = await _context.Applicants.FirstOrDefaultAsync(u => u.NIC == Application.ApplicantNIC && u.Nationality == Application.ApplicantNationality);
            if (Application == null)
            {
                throw new Exception("Invalid Email");
            }
            _emailService.RejectUserMail(Applicant.Email, Applicant.FullName);
            Application.Status = "Reject";
            await _context.SaveChangesAsync();
            return ("Rejected successfully");
        }
    }
}
