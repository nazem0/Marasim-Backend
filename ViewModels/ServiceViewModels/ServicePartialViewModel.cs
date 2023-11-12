using System;
using Models;
using ViewModels.ServiceAttatchmentViewModels;

namespace ViewModels.ServiceViewModels
{
	public class ServicePartialViewModel
	{
        public required int Id { get; set; }
        public required int VendorId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }
        //public int? ReviewsCount { get; set; }
        //public int? ReservationsCount { get; set; }
        public required IEnumerable<ServiceAttatchmentViewModel> ServiceAttachments { set; get; }
    }
}

