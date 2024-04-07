using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ClassNTutorContext _context;

        public PaymentRepository(ClassNTutorContext context)
        {
            _context = context;
        }

        public IQueryable<Payment> SearchAndFilterPayment(SearchFilterPaymentDto entity)
        {

            IQueryable<Payment> result = _context.Payments;

            if (entity.PaymentAmount != 0 && entity.PaymentAmount != null)
            {
                result = result.Where(p => p.PaymentAmount >= entity.PaymentAmount);
            }

            if (entity.PayerId != 0)
            {
                result = result.Where(p => p.PayerId == entity.PayerId);
            }

            if (entity.RequestId != 0)
            {
                result = result.Where(p => p.RequestId == entity.RequestId);
            }

            if ((entity.CreatedFrom != DateTime.MinValue && entity.CreatedFrom != null)
                && (entity.CreatedTo != DateTime.MinValue && entity.CreatedTo != null))
            {
                result = result.Where(p => p.CreatedAt >= entity.CreatedFrom && p.CreatedAt <= entity.CreatedTo);
            }

            return result;
        }
        public async Task<Payment> GetPaymentById(long paymentId)
        {
            var result = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId.Equals(paymentId)).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> UpdateStatusPaymentToSuspend(long paymentId)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId.Equals(paymentId)).ConfigureAwait(false);
            if (payment != null)
            {
                payment.Status = "SUSPEND";
                _context.Payments.Update(payment);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateDescriptionPayment(long paymentId, string paymentDescription)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId.Equals(paymentId)).ConfigureAwait(false);
            if (payment != null)
            {
                payment.PaymentDesc = paymentDescription;
                _context.Payments.Update(payment);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            return false;
        }
    }
}
