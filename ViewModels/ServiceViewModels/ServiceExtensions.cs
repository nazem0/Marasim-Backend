using Models;

namespace ViewModels.ServiceViewModels
{
    public static class ServiceExtensions
    {
        public static Service ToModel(this CreateServiceViewModel Data,int VendorID)
        {
            return new Service()
            {
                Title = Data.Title,
                Description = Data.Description,
                Price = Data.Price,
                VendorID = VendorID
            };
        }
    }
}