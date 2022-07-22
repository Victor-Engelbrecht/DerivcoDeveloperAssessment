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
            Random random = new Random();
            SpinModel model = new SpinModel() {
                Value = random.Next(0, 37) 
            };
            var result = await _db.Insert<SpinModel, SpinModel>("Spins", "Value", "@Value", model);
            if (result.Any())
            {
                if (await _db.Update("Bets", "SpinId=" + result.ToList().FirstOrDefault(), "SpinId = IS NULL", "NUll"))
                {
                    return model.Value;
                }

                throw new Exception("Could not update bets with current spin");
            }
            throw new Exception("Could not add spin to the database");
        }
    }
}
