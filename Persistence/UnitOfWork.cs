using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _entitiesContext;
        public UnitOfWork(AppDbContext entitiesContext)
        {
            _entitiesContext = entitiesContext;
        }

        public HttpStatusCode SaveChanges()
        {

            try
            {
                _entitiesContext.SaveChanges();
            }
            catch (Exception ex)
            {
                //Log exception using logger ex : serilog
                if (ex is DbUpdateException) return HttpStatusCode.InternalServerError;
                else if (ex is DbUpdateConcurrencyException) return HttpStatusCode.Conflict;
                else return HttpStatusCode.InternalServerError;
            }
            return HttpStatusCode.OK;
        }
        public async Task<HttpStatusCode> SaveChangesAsync()
        {
            try
            {
                await _entitiesContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //Log exception using logger ex : serilog
                if (ex is DbUpdateException) return HttpStatusCode.InternalServerError;
                else if (ex is DbUpdateConcurrencyException) return HttpStatusCode.Conflict;
                else if (ex is OperationCanceledException) return HttpStatusCode.BadRequest;
                else return HttpStatusCode.InternalServerError;
            }
            return HttpStatusCode.OK;
        }
    }
}