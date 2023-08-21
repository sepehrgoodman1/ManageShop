using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Services.DateGenerator
{
    public class DateTimeAppGenerator : DateTimeGenerator
    {
        public DateTime Generate()
        {
            return DateTime.Now;
        }
    }
}
