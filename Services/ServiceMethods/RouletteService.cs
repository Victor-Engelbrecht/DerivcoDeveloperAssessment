using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceMethods
{
    public class RouletteService
    {
        private readonly ISQLiteDataAccess _db;
        public RouletteService(ISQLiteDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> Spin()
        {
            var result = await _db.Insert("Users", "Name", "@Name", userModel);
            return result;
        }
    }
}
