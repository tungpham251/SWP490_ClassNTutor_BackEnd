﻿using DataAccess.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IRequestService
    {
        Task<ViewPaging<RequestDto>> GetRequestsForTutor(RequestRequestDto entity);

        Task<ViewPaging<RequestDto>> GetRequestsForParent(RequestRequestDto entity);

        Task<RequestDto> GetById(long id);
        Task<ViewPaging<RequestDto>> GetRequests(RequestRequestDto entity);
        Task<bool> AddRequest(AddRequestDto entity);

        Task<bool> UpdateRequest(UpdateRequestDto entity);

        Task<UpdateRequestDto> AcceptRequest(long requestId);

        Task<UpdateRequestDto> CancelRequest(long requestId);
        Task<UpdateRequestDto> DeclineRequest(long requestId);
    }
}
