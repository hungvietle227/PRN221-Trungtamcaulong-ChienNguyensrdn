using BadmintonCenter.BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.DAO
{
    public interface IPaymentMethodDAO
    {
        Task<PaymentMethod> GetPaymentMethodByIdAsync(int paymentMethodId);
        Task<List<PaymentMethod>> GetPaymentMethodAsync();
        Task AddPaymentMethodAsync(PaymentMethod paymentMethod);
        Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task DeletePaymentMethodAsync(int paymentMethodId);
    }

    public class PaymentMethodDAO : IPaymentMethodDAO
    {
        private readonly BadmintonDbContext _context;

        public PaymentMethodDAO(BadmintonDbContext context)
        {
            _context = context;
        }

        public async Task AddPaymentMethodAsync(PaymentMethod paymentMethod)
        {
            await _context.PaymentMethods.AddAsync(paymentMethod);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaymentMethodAsync(int packageId)
        {
            var paymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(p => p.PaymentMethodId == packageId);
            _context.PaymentMethods.Remove(paymentMethod);
        }

        public async Task<List<PaymentMethod>> GetPaymentMethodAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod> GetPaymentMethodByIdAsync(int paymentMethodId)
        {
            var paymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(a => a.PaymentMethodId == paymentMethodId);
            return paymentMethod;
        }

        public async Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Update(paymentMethod);
            await _context.SaveChangesAsync();
        }
    }
}
