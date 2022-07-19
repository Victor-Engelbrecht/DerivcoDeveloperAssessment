using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceMethods
{
    public class BetService
    {
        private readonly ISQLiteDataAccess _db;
        public BetService(ISQLiteDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> PlaceBet(BetModel model)
        {
            var result = await _db.Insert("Users", "Name", "@Name", model);
            return result;
        }

        public async Task<bool> Payout(UserModel)
        {
            var result = await _db.Insert("Users", "Name", "@Name", userModel);
            return result;
        }
    }
}
