using AcademyBank.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyBank.Web.Mappings
{
    public class UserStoreProfile : Profile
    {
        public UserStoreProfile()
        {
            CreateMap<Loan, AccountLoanViewModel>().ReverseMap();
            CreateMap<Deposit, AccountDepositViewModel>().ReverseMap();
            CreateMap<AccountLoanViewModel, AccountLoan>().ReverseMap();
            CreateMap<AccountDepositViewModel, AccountDeposit>().ReverseMap();
        }
    }
}
