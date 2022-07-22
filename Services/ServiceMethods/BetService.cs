using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceMethods
{
    public class BetService : IBetService
    {
        private readonly ISQLiteDataAccess _db;
        public BetService(ISQLiteDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> PlaceBet(BetModel betModel)
        {
            var result = await _db.Insert("Bets", "Amount, UserId, BetOnNumber", "@Amount, @UserId, @BetOnNumber", betModel);
            return result;
        }

        public async Task<int> Payout(UserModel user)
        {
            var result = await _db.SQLQuery<int, UserModel>("SELECT SUM(Amount) FROM Bets WHERE UserId=@UserId AND Paid=0",user);
            if (result.Any())
            {
                if (await _db.Update("Bets", "Paid=1", "UserId=@UserId", user))
                {
                    return result.FirstOrDefault();
                }
                throw new Exception("Could not gets update bets data to paid");
            }
            return 0;
        }
    }
}
