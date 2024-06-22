using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.Service.Interface
{
    public interface IPaymentMethodService
    {
        Task<PaymentMethod> GetPaymentMethodById(int id);
        Task<PaymentMethod?> GetPaymentMethodByName(string name);
    }
}
