using AcademyBank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBank.DAL.Repositories.Interfaces
{
    public interface ICardRequestRepository<T>
    {
        Task<IEnumerable<Card>> GetPendingCards();
        Task<IEnumerable<Card>> GetRejectedCards();
        Task<IEnumerable<Card>> GetBlockedCards();
        Task<IEnumerable<Card>> GetActiveCards();
        Task<Card> GetById(int id);
        Task<Card> ApproveCard(int id, Card card);
        Task<Card> Update(T t);
        Task<Card> Delete(T t);
        Task<Card> Reject(int id);
		Task<IEnumerable<Card>> GetExpiredCards();
        Task<Card> BlockCardById(int id);
        Task<Card> UnblockCardById(int id);
    }
}
