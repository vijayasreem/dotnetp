﻿


using dotnetp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public interface IBookingModelRepository
    {
        Task<int> CreateAsync(BookingModel booking);
        Task<BookingModel> GetByIdAsync(int id);
        Task<IEnumerable<BookingModel>> GetAllAsync();
        Task<bool> UpdateAsync(BookingModel booking);
        Task<bool> DeleteAsync(int id);
    }
}
