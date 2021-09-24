using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Api.Search.Interfaces
{
    public interface ICustumerService
    {
        Task<(bool isSuccess, dynamic Customer, string errorMessage)> GetCustomerAsync(int id);
    }
}
