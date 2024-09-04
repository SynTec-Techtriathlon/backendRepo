using Back.Models;
using Microsoft.EntityFrameworkCore;

namespace Back.Services
{
    public class ReportService: IReportService
    {
        private readonly ApplicationDbContext _context;
        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ReportData>> GetValues()
        {
            return await _context.ReportData.ToListAsync();
        }
        public async Task<ReportData> GetValue(int id)
        {
            return await _context.ReportData.FindAsync(id);
        }
        public async Task AddValue(ReportData reportData)
        {
            _context.ReportData.Add(reportData);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateValue(ReportData reportData)
        {
            _context.ReportData.Update(reportData);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteValue(int id)
        {
            var obj = await _context.ReportData.FindAsync(id);
            if (obj == null)
            {
                throw new InvalidOperationException("ReportData not found.");
            }

            _context.ReportData.Remove(obj);
            await _context.SaveChangesAsync();
        }



    }
}
