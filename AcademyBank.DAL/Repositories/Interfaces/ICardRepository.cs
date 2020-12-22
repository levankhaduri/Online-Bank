using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Interfaces
{
    public interface ICardRepository<T>
    {
        Task<T> GetByNumber(string cardNumber);
        Task Transfer(Account transferFrom, Account transferTo, decimal amount);
        Task UtilityTransfer(Account transferFrom, decimal amount);
        Task<List<T>> GetCardsByUserId(int id);
	}
}
