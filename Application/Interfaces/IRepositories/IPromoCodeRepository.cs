using Application.DTOs.PromoCodeDTOs;
using Domain.Entities;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface IPromoCodeRepository
    {
        public HttpStatusCode Add(CreatePromoCodeDTO createPromoCodeDTO);
        public PromoCode? GetPromoCodeByServiceId(int ServiceId);
        public PromoCode? GetPromoCodeByCode(string Code, int ServiceId);
        public float UsePromoCode(string code, int serviceId);
        public HttpStatusCode DeleteByServiceId(int serviceId);
    }
}
