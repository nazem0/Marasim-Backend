using System;
using Models;

namespace ViewModels.ReactViewModel
{
	public class ReactViewModel
	{
        public required int ID { get; set; }
        public required string UserID { get; set; }
        public required int PostID { get; set; }
        public required DateTime DateTime { get; set; }
        public required string UserName { get; set; }
        public required string UserPicUrl { get; set; }
    }
}

