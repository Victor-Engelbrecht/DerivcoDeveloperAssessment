using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BetModel
    {
        public int BetId { get; set; }
        public int SpinId { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
        public bool Paid { get; set; } = false;
        public int BetOnNumber { get; set; }
    }
}
