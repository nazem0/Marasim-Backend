using Models;
using ViewModels.UserViewModels;

namespace ViewModels.ReviewViewModels
{
    public class ReviewFullViewModel : BaseModel
    {
        public required int Rate { get; set; }
        public required string Message { get; set; }
        public required DateTime DateTime { get; set; }
        public required string ServiceTitle { get; set; }
        public required int ServiceId { get; set; }
        public required UserMinInfoViewModel User { get; set; }
    }
}

