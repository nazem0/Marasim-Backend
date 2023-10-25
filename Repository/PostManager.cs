using System;
using Models;

namespace Repository
{
	public class PostManager :MainManager<Post>
    {
            public PostManager(EntitiesContext _dBContext) : base(_dBContext) { }
	}
}

