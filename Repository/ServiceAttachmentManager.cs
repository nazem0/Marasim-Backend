﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{


	public class ServiceAttachmentManager : MainManager<ServiceAttachmentManager>
	{
		public ServiceAttachmentManager(EntitiesContext _dBContext) : base(_dBContext) { }
	}
}
