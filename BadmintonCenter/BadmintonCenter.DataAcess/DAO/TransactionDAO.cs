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
        Task<Transaction?> GetTransactionByIdAsync(int transactionId);
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task AddTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(Transaction transactionId);
    }
    public class TransactionDAO : ITransactionDAO
    {
        private readonly BadmintonDbContext _context;

        public TransactionDAO(BadmintonDbContext context)
        {
            _context = context;
        }
        public async Task<Transaction?> GetTransactionByIdAsync(int transactionId)
        {
            return await _context.Transactions.FirstOrDefaultAsync(b => b.TransactionId == transactionId);
        }
        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            var addedTransaction = await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            _context.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(Transaction transactionId)
        {
            _context.Remove(transactionId);
            await _context.SaveChangesAsync();
        }
    }
}
