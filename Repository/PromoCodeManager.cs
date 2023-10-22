using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class PromoCodeManager : MainManager<PromoCode>
	{
		public PromoCodeManager(EntitiesContext _dBContext) : base(_dBContext) { }

	}
}
