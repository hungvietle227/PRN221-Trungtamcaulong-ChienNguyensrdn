using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.DataAcess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.Repository
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly IPaymentMethodDAO _paymentMethodDAO;

        public PaymentMethodRepository(IPaymentMethodDAO paymentMethodDAO)
        {
            _paymentMethodDAO = paymentMethodDAO;
        }
        public Task AddPaymentMethodAsync(PaymentMethod paymentMethod)
        {
            return _paymentMethodDAO.AddPaymentMethodAsync(paymentMethod);
        }

        public Task DeletePaymentMethodAsync(int paymentMethodId)
        {
            return _paymentMethodDAO.DeletePaymentMethodAsync(paymentMethodId);
        }

        public Task<List<PaymentMethod>> GetPaymentMethodAsync()
        {
            return _paymentMethodDAO.GetPaymentMethodAsync();
        }

        public Task<PaymentMethod> GetPaymentMethodByIdAsync(int paymentMethodId)
        {
            return _paymentMethodDAO.GetPaymentMethodByIdAsync(paymentMethodId);
        }

        public async Task<PaymentMethod?> GetPaymentMethodByNameAsync(string name)
        {
            var paymentMethods = await _paymentMethodDAO.GetPaymentMethodAsync();
            return paymentMethods.FirstOrDefault(p => p.MethodName.ToUpper() == name.ToUpper());
        }

        public Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            return _paymentMethodDAO.UpdatePaymentMethodAsync(paymentMethod);
        }
    }
}
