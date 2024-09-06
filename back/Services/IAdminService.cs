namespace Back.Services
{
    public interface IAdminService
    {
        Task<string> ApproveApplication(int request);
        Task<string> RejectApplication(int request);
    }
}
