using BirdTrading.Web.Models;

namespace BirdTrading.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDTO> SendAsync(RequestDTO requestDto, bool withBearer = true);
    }
}
