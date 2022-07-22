using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceMethods
{
    public class RouletteService : IRouletteService
    {
        private readonly ISQLiteDataAccess _db;
        public RouletteService(ISQLiteDataAccess db)
        {
            _db = db;
        }

        public async Task<int> Spin()
        {
            //TODO:
            //This should suffice for adding data ons spins for now though all the logic for a spin should most likely move here sicne it doesn't tell the user if they have won or not
            //Logic get number... insert into SpinsTable... update BetTable... Get bet result for all current bets... Send bets result and quantity won/lost to users
            Random random = new Random();
            SpinModel spinResult = new SpinModel(){
                Value = random.Next(0, 37) 
            };
            var insertSuccessResult = await _db.Insert<SpinModel, SpinModel>("Spins", "Value", "@Value", spinResult);
            if (insertSuccessResult.Any())
            {
                if (await _db.Update("Bets", "SpinId=" + insertSuccessResult.ToList().FirstOrDefault().SpinId, "SpinId IS NULL", "NUll"))
                {
                    return spinResult.Value;
                }
                throw new Exception("Could not update bets with current spin");
            }
            throw new Exception("Could not add spin to the database");
        }
    }
}
