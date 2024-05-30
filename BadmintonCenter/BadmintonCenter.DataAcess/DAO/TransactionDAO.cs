using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.DataAcess.DAO
{
    public interface ITransactionDAO
    {
        Task<Transaction> GetUserByIdAsync(int transactionId);
        Task<List<Transaction>> GetAllUsersAsync();
        Task AddUserAsync(Transaction transaction);
        Task UpdateUserAsync(Transaction transaction);
        Task DeleteUserAsync(int transactionId);
    }
    public class TransactionDAO
    {
        private readonly BadmintonDbContext _context;

        public TransactionDAO(BadmintonDbContext context)
        {
            _context = context;
        }
        public async Task<Transaction> GetTransactionByIdAsync (int transactionId)
        {
            //
            //return _context.Transactions.FirstOrDefault(b => b.TransactionId == transactionId);
            throw new NotImplementedException();
        }

        public Transaction AddTransaction(Transaction transaction) 
        { 
            //
            throw new NotImplementedException(); 
        }

        public Transaction UpdateTransaction(Transaction transaction) 
        {
            //
            throw new NotImplementedException(); 
        }

        public Transaction DeleteTransaction(int transactionId) 
        { 
            //
            throw new NotImplementedException(); 
        }

    }
}
