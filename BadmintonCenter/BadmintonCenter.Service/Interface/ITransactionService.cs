using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.Service.Interface
{
    public interface ITransactionService
    {
        Task AddNewTransaction(Transaction transaction);
    }
}
