using Back.Models.DTO;

namespace Back.Services
{
    public interface IAPIcallService
    {
        Task<string> CallAPI(InterpolDTO data);
    }
}
