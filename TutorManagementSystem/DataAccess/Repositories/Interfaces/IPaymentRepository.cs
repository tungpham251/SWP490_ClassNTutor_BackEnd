using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        IQueryable<Payment> SearchAndFilterPayment(SearchFilterPaymentDto entity);
        IQueryable<Payment> SearchAndFilterPaymentByCurrentUser(SearchFilterPaymentCurrentUserDto entity, string personId);
        Task<Payment> GetPaymentById(long paymentId);
        Task<bool> UpdateStatusPaymentToSuspend(long paymentId);
        Task<bool> UpdateDescriptionPayment(long paymentId, string paymentDescription);
    }
}
