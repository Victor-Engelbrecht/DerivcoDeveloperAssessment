using Models;

namespace Services.ServiceMethods
{
    public interface IRouletteService:IService
    {
        Task<int> Spin();
    }
}