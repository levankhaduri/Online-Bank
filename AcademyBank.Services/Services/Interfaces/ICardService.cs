using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface ICardService
    {
        Task<Card> Create(Card card);
        Task<IEnumerable<Card>> Get();
        Task<Card> GetById(int id);
        Task<Card> GetByNumber(string cardNumber);
        Task Transfer(Account transferFrom, Account transferTo, decimal amount);
        Task UtilityTransfer(Account transferFrom, decimal amount);
        Task<IEnumerable<Card>> GetUserCards(int id);
        Task<Card> Update(Card card);
        Task Delete(Card card);
		Task<List<Card>> GetCardsByUserId(int id);
	}
}
