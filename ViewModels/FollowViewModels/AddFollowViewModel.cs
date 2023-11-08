using Models;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.FollowViewModels
{
    public  class AddFollowViewModel
    {
        public int VendorId { get; set; }
    }

    public class FollowViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int VendorId { get; set; }
        public DateTime DateTime { get; set; }
    }
}





