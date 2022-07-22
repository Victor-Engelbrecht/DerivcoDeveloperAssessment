using Models;

namespace Services.ServiceMethods
{
    public interface IBetService:IService
    {
        Task<bool> PlaceBet(BetModel model);

        Task<int> Payout(UserModel user);
    }
}