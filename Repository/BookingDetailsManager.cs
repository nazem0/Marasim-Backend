using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository
{
    public class BookingDetailsManager : MainManager<BookingDetails>
    {
        public BookingDetailsManager(EntitiesContext _dBContext) : base(_dBContext)
        {
        }
    }
}
