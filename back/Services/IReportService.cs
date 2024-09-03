using Back.Models;

namespace Back.Services
{
    public interface IReportService
    {
        Task<List<ReportData>> GetValues();
        Task<ReportData> GetValue(int id);
        Task AddValue(ReportData reportData);
        Task UpdateValue(ReportData reportData);
        Task DeleteValue(int id);
    }
}
