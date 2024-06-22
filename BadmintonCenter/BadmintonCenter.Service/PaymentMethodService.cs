using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.Common.DTO.Booking;
using BadmintonCenter.Common.DTO.Payment;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace BadmintonCenter.Service
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<PaymentMethod> GetPaymentMethodById(int id)
        {
            return await _paymentMethodRepository.GetPaymentMethodByIdAsync(id);
        }

        public async Task<PaymentMethod?> GetPaymentMethodByName(string name)
        {
            return await _paymentMethodRepository.GetPaymentMethodByNameAsync(name);
        }
    }
}
