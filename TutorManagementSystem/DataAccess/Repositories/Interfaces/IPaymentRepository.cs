using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        IQueryable<Payment> SearchAndFilterPayment(SearchFilterPaymentDto entity);
        Task<Payment> GetPaymentById(long paymentId);
        Task<bool> UpdateStatusPaymentToSuspend(long paymentId);
        Task<bool> UpdateDescriptionPayment(long paymentId, string paymentDescription);
    }
}
