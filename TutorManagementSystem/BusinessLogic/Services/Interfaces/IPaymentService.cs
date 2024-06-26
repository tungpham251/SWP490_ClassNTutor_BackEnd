﻿using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<ViewPaging<ResponsePaymentDto>> SearchAndFilterPayment(SearchFilterPaymentDto entity);
        Task<ResponsePaymentDto> GetPaymentById(long paymentId);
        Task<bool> UpdatePayment(long paymentId, string paymentDescription);
        Task<bool> DeletePayment(long paymentId);
        Task<bool> CreatePayment(CreatePaymentDto entity);
        Task<ViewPaging<ResponsePaymentDto>> GetPaymentByCurrentUser(SearchFilterPaymentCurrentUserDto entity, string personId);

        Task<bool> UpdatePaymentDescription(long paymentId, string paymentDescription);
    }
}
