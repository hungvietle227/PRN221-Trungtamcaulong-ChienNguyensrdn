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
    public class TransactionRepository: ITransactionRepository
    {
        private readonly ITransactionDAO _transactionDAO;

        public TransactionRepository(ITransactionDAO transactionDAO)
        {
            _transactionDAO = transactionDAO;
        }

        public async Task<Transaction> GetTransactionByIdAsync(int transactionId)
        {
            return await _transactionDAO.GetTransactionByIdAsync(transactionId);
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionDAO.GetAllTransactionsAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _transactionDAO.AddTransactionAsync(transaction);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            await _transactionDAO.UpdateTransactionAsync(transaction);
        }

        public async Task DeleteTransactionAsync(Transaction transaction)
        {
            await _transactionDAO.DeleteTransactionAsync(transaction);
        }
    }
}
