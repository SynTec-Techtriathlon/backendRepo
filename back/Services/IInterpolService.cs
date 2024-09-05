using Back.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Back.Services
{
    public interface IInterpolService
    {
        Task<string> CheckRedNoticedApplicant(InterpolDTO value);
        Task<string> CheckYellowNoticedApplicant(InterpolDTO value);
        Task<string> CheckUNNoticedApplicant(InterpolDTO value);
    }
}
