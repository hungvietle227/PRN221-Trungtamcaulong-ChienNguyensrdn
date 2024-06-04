using BadmintonCenter.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.Repository.Interface
{
    public interface IPaymentMethodRepository
    {
        Task<PaymentMethod> GetPaymentMethodByIdAsync(int paymentMethodId);
        Task<List<PaymentMethod>> GetPaymentMethodAsync();
        Task AddPaymentMethodAsync(PaymentMethod paymentMethod);
        Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task DeletePaymentMethodAsync(int paymentMethodId);
    }
}
