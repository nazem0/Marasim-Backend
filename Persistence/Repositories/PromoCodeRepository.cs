using Application.DTOs.PromoCodeDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.Repositories
{
    public class PromoCodeRepository : IPromoCodeRepository
    {
        private readonly DbSet<PromoCode> _promoCodes;
        private readonly IUnitOfWork _unitOfWork;
        public PromoCodeRepository(EntitiesContext entitiesContext, IUnitOfWork unitOfWork)
        {
            _promoCodes = entitiesContext.PromoCodes;
            _unitOfWork = unitOfWork;
        }
        /*
        ████████╗███████╗███████╗████████╗
        ╚══██╔══╝██╔════╝██╔════╝╚══██╔══╝
           ██║   █████╗  ███████╗   ██║   
           ██║   ██╔══╝  ╚════██║   ██║   
           ██║   ███████╗███████║   ██║   
           ╚═╝   ╚══════╝╚══════╝   ╚═╝   
        */
        public HttpStatusCode Add(CreatePromoCodeDTO createPromoCodeDTO)
        {
            _promoCodes.Add(createPromoCodeDTO.ToEntity());
            return _unitOfWork.SaveChanges();
        }
        public PromoCode? GetPromoCodeByServiceId(int ServiceId)
        {
            return _promoCodes.Where(p => p.ServiceId == ServiceId).FirstOrDefault();
        }
        public PromoCode? GetPromoCodeByCode(string Code, int ServiceId)
        {
            return _promoCodes.Where(pc => pc.Code == Code & pc.ServiceId == ServiceId).FirstOrDefault();
        }
        public float UsePromoCode(string code, int serviceId)
        {
            PromoCode? promoCode = GetPromoCodeByCode(code, serviceId);
            if (promoCode is null || promoCode.Count == promoCode.Limit) return 0;
            promoCode.Count++;
            return promoCode.Discount;
        }
        /*
        ████████╗███████╗███████╗████████╗
        ╚══██╔══╝██╔════╝██╔════╝╚══██╔══╝
           ██║   █████╗  ███████╗   ██║   
           ██║   ██╔══╝  ╚════██║   ██║   
           ██║   ███████╗███████║   ██║   
           ╚═╝   ╚══════╝╚══════╝   ╚═╝   
        */
        public HttpStatusCode DeleteByServiceId(int serviceId)
        {
            PromoCode? pc = _promoCodes.Where(pc => pc.ServiceId == serviceId).FirstOrDefault();
            if (pc is null) return HttpStatusCode.NotFound;
            _promoCodes.Remove(pc);
            return _unitOfWork.SaveChanges();
        }

    }
}
