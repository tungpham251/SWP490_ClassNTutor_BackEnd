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
                newPayment.CreatedAt = newPayment.UpdatedAt = DateTime.Now;

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

        public async Task<IEnumerable<ResponsePaymentDto>> GetPaymentByCurrentUser(string personId)
        {
            var currentId = long.Parse(personId);
            var search = await _context.Payments.Include(p => p.Payer).ThenInclude(t => t.Person)
                                                .Include(p => p.Request).ThenInclude(r => r.Person)
                                                .Where(p => p.PayerId.Equals(currentId) || p.RequestId.Equals(currentId))
                                                .ToListAsync()
                                                .ConfigureAwait(false);
            var result = _mapper.Map<IEnumerable<ResponsePaymentDto>>(search);
            return result;
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

        public async Task<bool> UpdatePayment(long paymentId, string paymentDescription)
        {
            var result = await _paymentRepository.UpdateDescriptionPayment(paymentId, paymentDescription).ConfigureAwait(false);
            return result;
        }
    }
}
