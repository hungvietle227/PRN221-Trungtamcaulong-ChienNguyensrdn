using BadmintonCenter.BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.DAO
{
    public interface ITransactionDAO
    {
        Task<Transaction> GetTransactionByIdAsync(int TransactionId);
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task AddTransactionAsync(Transaction Transaction);
        Task UpdateTransactionAsync(Transaction Transaction);
        Task DeleteTransactionAsync(Transaction TransactionId);
    }
    public class TransactionDAO : ITransactionDAO
    {
        private readonly BadmintonDbContext _context;

        public TransactionDAO(BadmintonDbContext context)
        {
            _context = context;
        }
        public async Task<Transaction> GetTransactionByIdAsync(int TransactionId)
        {
            return _context.Transactions.FirstOrDefault(b => b.TransactionId == TransactionId);
        }
        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task AddTransactionAsync(Transaction Transaction)
        {
            var addedTransaction = await _context.Transactions.AddAsync(Transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransactionAsync(Transaction Transaction)
        {
            _context.Update(Transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(Transaction TransactionId)
        {
            _context.Remove(TransactionId);
            await _context.SaveChangesAsync();
        }
    }
}
