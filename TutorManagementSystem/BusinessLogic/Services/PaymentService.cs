using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ClassNTutorContext _context;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(ClassNTutorContext context, IPaymentRepository paymentRepository, IMapper mapper)
        {
            _context = context;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreatePayment(CreatePaymentDto entity)
        {
            try
            {
                var lastPayment = await _context.Payments.OrderBy(x => x.PaymentId).LastOrDefaultAsync().ConfigureAwait(false);

                var newPayment = _mapper.Map<Payment>(entity);
                newPayment.PaymentId = lastPayment.PaymentId + 1;
                newPayment.CreatedAt = DateTime.Now;
                newPayment.RequestDate = DateTime.Now;
                newPayment.PaymentType = "AccountMaintenanceFee";
                newPayment.Status = "UNPAID";
                await _context.Payments.AddAsync(newPayment).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePayment(long paymentId)
        {
            var result = await _paymentRepository.UpdateStatusPaymentToSuspend(paymentId).ConfigureAwait(false);
            return result;
        }

        public async Task<ViewPaging<ResponsePaymentDto>> GetPaymentByCurrentUser(SearchFilterPaymentCurrentUserDto entity, string personId)
        {
            var search = _paymentRepository.SearchAndFilterPaymentByCurrentUser(entity, personId);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.PaymentId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<ResponsePaymentDto>>(pagingList);

            return new ViewPaging<ResponsePaymentDto>(result, pagination);
        }

        public async Task<ResponsePaymentDto> GetPaymentById(long paymentId)
        {
            var search = await _paymentRepository.GetPaymentById(paymentId).ConfigureAwait(false);
            var result = _mapper.Map<ResponsePaymentDto>(search);
            return result;
        }

        public async Task<ViewPaging<ResponsePaymentDto>> SearchAndFilterPayment(SearchFilterPaymentDto entity)
        {
            var search = _paymentRepository.SearchAndFilterPayment(entity);

            var pagingList = await search.Skip(entity.PagingRequest.PageSize * (entity.PagingRequest.CurrentPage - 1))
                .Take(entity.PagingRequest.PageSize).OrderBy(x => x.PaymentId)
                .ToListAsync()
                .ConfigureAwait(false);

            var pagination = new Pagination(await search.CountAsync(), entity.PagingRequest.CurrentPage,
                entity.PagingRequest.PageRange, entity.PagingRequest.PageSize);

            var result = _mapper.Map<IEnumerable<ResponsePaymentDto>>(pagingList);

            return new ViewPaging<ResponsePaymentDto>(result, pagination);
        }

        public async Task<bool> UpdatePayment(long paymentId, string status)
        {
            var result = await _paymentRepository.UpdateStatusPayment(paymentId, status).ConfigureAwait(false);
            return result;
        }


        public async Task<bool> UpdatePaymentDescription(long paymentId, string paymentDescription)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId.Equals(paymentId)).ConfigureAwait(false);
            if (payment != null)
            {
                payment.PaymentDesc = paymentDescription;
                payment.PayDate = DateTime.Now;
                payment.UpdatedAt = DateTime.Now;
                payment.Status = "SENT";

                _context.Payments.Update(payment);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            return false;
        }
    }
}
