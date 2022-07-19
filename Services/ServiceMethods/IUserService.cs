using Models;

namespace Services.ServiceMethods
{
    public interface IUserService : IService
    {
        Task<bool> InsertUser(UserModel userModel);
        Task<int> Add(int number1, int number2);
    }
}