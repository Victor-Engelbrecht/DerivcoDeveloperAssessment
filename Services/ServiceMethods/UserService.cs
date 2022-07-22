using Dapper;
using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceMethods
{
    public class UserService: IUserService
    {
        private readonly ISQLiteDataAccess _db;
        public UserService(ISQLiteDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> InsertUser(UserModel userModel) 
        {
            var result = await _db.Insert("Users", "Name", "@Name", userModel);
            return result;
        }
        
        public async Task<bool> UpdateUser(UserModel userModel)
        {
            var result = await _db.Update("Users", "Name", "@Name", userModel);
            return result;
        }

        public async Task<bool> DeleteUser(UserModel userModel)
        {
            var result = await _db.Delete("Users", "UserId", "@UserId", userModel);
            return result;
        }

        public async Task<UserModel> GetUser(int Id)
        {
            var model = await _db.GetAllById<UserModel>("Users","UserId="+Id);
            if (model.Any())
            {
                return model.FirstOrDefault();
            }
            return new UserModel();
            
        }

        //Just a test to learn how to do the
        public async Task<int> Add(int number1, int number2) 
        {
            return await Task.Run(() => number1 + number2);
        }
    }
}
