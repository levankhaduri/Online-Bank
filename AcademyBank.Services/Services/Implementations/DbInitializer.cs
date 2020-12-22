using AcademyBank.Models.Enums;
using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL;
using AcademyBank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademyBank.BLL.Helpers.Implementations;

namespace AcademyBank.BLL.Services.Implementations
{
    public class DbInitializer : IDbInitializer
    {
        private readonly BankDbContext _context;
        private readonly IEncryptionService _encryptionService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public DbInitializer(BankDbContext context,
            IEncryptionService encryptionService,
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _encryptionService = encryptionService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            var user = await _userManager.FindByNameAsync("admin");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    PhoneNumber = "78945612",
                    UserInfo = new UserInfo()
                    {
                        Address = "ItAcademy",
                        City = "Tbilisi",
                        FirstName = "Admin",
                        LastName = "Adminashvili",
                        PassportId = "555777888",
                        Sex = "Male",
                        ImageName = "A.png"
                    },

                    LoginReports = new List<LoginReport>
                    {
                        new LoginReport
                        {
                            LastLogin = DateTime.Now,
                            FirstLogin = DateTime.Now,
                            AvgPerday=1,
                            PerMonth=30,
                            CounterLogsIn=30,
                        }
            }
                }, "asdASD123!@#");

            }
            var role = await _roleManager.FindByNameAsync("Admin");

            if (role == null)
            {
                await _roleManager.CreateAsync(new IdentityRole<int>() { Name = "Admin" });
                var createdAdmin = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(createdAdmin, "Admin");
            }

            user = await _userManager.FindByNameAsync("Andrii");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Andrii",
                    Email = "Andrii@gmail.com",
                    PhoneNumber = "14881488",
                    UserInfo = new UserInfo()
                    {
                        Address = "Gldanula",
                        City = "Gldanopolis",
                        FirstName = "Andrii",
                        LastName = "Turianskyi",
                        PassportId = "123456789",
                        Sex = "Male",
                        ImageName = "A.png"
                    }
                }, "asdASD123!@#");
            }

            user = await _userManager.FindByNameAsync("Jimmy");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Jimmy",
                    Email = "jimmypage@gmail.com",
                    PhoneNumber = "197345682",
                    UserInfo = new UserInfo()
                    {
                        Address = "Plato",
                        City = "JimmyWown",
                        FirstName = "Jimmy",
                        LastName = "Page",
                        PassportId = "1000101",
                        Sex = "Male",
                        ImageName = "jimmy.png"
                    }
                }, "asdASD123!@#");
            }

            user = await _userManager.FindByNameAsync("John");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "John",
                    Email = "John@gmail.com",
                    PhoneNumber = "15978453",
                    UserInfo = new UserInfo()
                    {
                        Address = "Foreignstan",
                        City = "Cityburg",
                        FirstName = "John",
                        LastName = "Bergmann",
                        PassportId = "5558888777",
                        Sex = "Male",
                        ImageName = "J.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Irinka");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Irinka",
                    Email = "Irinka@gmail.com",
                    PhoneNumber = "11111222",
                    UserInfo = new UserInfo()
                    {
                        Address = "Gagarini",
                        City = "Tbilisi",
                        FirstName = "Irinka",
                        LastName = "Inashvili",
                        PassportId = "1000103",
                        Sex = "Female",
                        ImageName = "I.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Vano");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Vano",
                    Email = "Vano@gmail.com",
                    PhoneNumber = "5554445",
                    UserInfo = new UserInfo()
                    {
                        Address = "Vazisubani",
                        City = "Tbilisi",
                        FirstName = "Vano",
                        LastName = "Tsiklauri",
                        PassportId = "1000104",
                        Sex = "Male",
                        ImageName = "V.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Nini");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Nini",
                    Email = "Nini@gmail.com",
                    PhoneNumber = "4448444",
                    UserInfo = new UserInfo()
                    {
                        Address = "Makhata",
                        City = "Tbilisi",
                        FirstName = "Nini",
                        LastName = "Kurtanidze",
                        PassportId = "1000105",
                        Sex = "Female",
                        ImageName = "N.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Aleksandre");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Aleksandre",
                    Email = "Aleksandre@gmail.com",
                    PhoneNumber = "4777585",
                    UserInfo = new UserInfo()
                    {
                        Address = "Uznadze",
                        City = "Tbilisi",
                        FirstName = "Aleksandre",
                        LastName = "Gabelashvili",
                        PassportId = "1000106",
                        Sex = "Male",
                        ImageName = "A.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Sergii");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Sergii",
                    Email = "Sergii@gmail.com",
                    PhoneNumber = "48878789",
                    UserInfo = new UserInfo()
                    {
                        Address = "Politkovskaya",
                        City = "Kyiv",
                        FirstName = "Sergii",
                        LastName = "Kovach",
                        PassportId = "1000107",
                        Sex = "Male",
                        ImageName = "S.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Natali");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Natali",
                    Email = "Natali@gmail.com",
                    PhoneNumber = "8888877",
                    UserInfo = new UserInfo()
                    {
                        Address = "Sarajishvili",
                        City = "Akhalkalaki",
                        FirstName = "Natali",
                        LastName = "Japaridze",
                        PassportId = "1000108",
                        Sex = "Female",
                        ImageName = "N.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Anna");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Anna",
                    Email = "Anna@gmail.com",
                    PhoneNumber = "30002001",
                    UserInfo = new UserInfo()
                    {
                        Address = "Guramishvili",
                        City = "Tbilisi",
                        FirstName = "Anna",
                        LastName = "Zakaidze",
                        PassportId = "1000109",
                        Sex = "Female",
                        ImageName = "A.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Giga");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Giga",
                    Email = "Giga@gmail.com",
                    PhoneNumber = "70707080",
                    UserInfo = new UserInfo()
                    {
                        Address = "Marjanishvili",
                        City = "Tbilisi",
                        FirstName = "Giga",
                        LastName = "Baidoshvili",
                        PassportId = "1000111",
                        Sex = "Male",
                        ImageName = "G.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Shota");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Shota",
                    Email = "Shota@gmail.com",
                    PhoneNumber = "125554445",
                    UserInfo = new UserInfo()
                    {
                        Address = "Saburtalo",
                        City = "Tbilisi",
                        FirstName = "Shota",
                        LastName = "Gotsadze",
                        PassportId = "1000112",
                        Sex = "Male",
                        ImageName = "S.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Tamari");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Tamari",
                    Email = "Tamari@gmail.com",
                    PhoneNumber = "11145454",
                    UserInfo = new UserInfo()
                    {
                        Address = "Nojikhevi",
                        City = "Zugdidi",
                        FirstName = "Tamar",
                        LastName = "Nasrashvili",
                        PassportId = "1000113",
                        Sex = "Female",
                        ImageName = "T.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Nana");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Nana",
                    Email = "Nana@gmail.com",
                    PhoneNumber = "99996523",
                    UserInfo = new UserInfo()
                    {
                        Address = "Sarajishvili",
                        City = "Tbilisi",
                        FirstName = "Nana",
                        LastName = "Rogava",
                        PassportId = "1000114",
                        Sex = "Female",
                        ImageName = "N.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Nika");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Nika",
                    Email = "Nika@gmail.com",
                    PhoneNumber = "550040040",
                    UserInfo = new UserInfo()
                    {
                        Address = "Varketili",
                        City = "Tbilisi",
                        FirstName = "Nika",
                        LastName = "Tsivilashvili",
                        PassportId = "1000115",
                        Sex = "Male",
                        ImageName = "N.png"
                    }
                }, "asdASD123!@#");
            }
            user = await _userManager.FindByNameAsync("Tatia");

            if (user == null)
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "Tatia",
                    Email = "Tatia@gmail.com",
                    PhoneNumber = "599797998",
                    UserInfo = new UserInfo()
                    {
                        Address = "Konstitucia",
                        City = "Rustavi",
                        FirstName = "Tatia",
                        LastName = "Sebiskveradze",
                        PassportId = "1000116",
                        Sex = "Female",
                        ImageName = "T.png"
                    }
                }, "asdASD123!@#");
            }

            var existingAccounts = _context.Accounts.Where(x => x.AccountName == "johnAcc" || x.AccountName == "andriiAcc").ToList();

            if (existingAccounts != null && existingAccounts.Count != 0)
                return;

            _context.Database.EnsureCreated();

            var transaction = new TransactionsHistory()
            {
                Date = DateTime.Now,
                Currency = "USD",
                Amount = 100,
                Description = "Sending someone some money",
            };

            var accountDeposit1 = new AccountDeposit()
            {
                Status = Status.IsActive.ToString(),
                DepositAmount = 100,
                AccountId = 1,
                Term = typeof(Term).GetValues().ToArray()[0]
            };

            var accountDeposit2 = new AccountDeposit()
            {
                Status = Status.Pending.ToString(),
                DepositAmount = 100,
                AccountId = 2,
                Term = typeof(Term).GetValues().ToArray()[1]
            };

            var accountDeposit3 = new AccountDeposit()
            {
                Status = Status.Pending.ToString(),
                DepositAmount = 5000,
                AccountId = 3,
                Term = typeof(Term).GetValues().ToArray()[2]
            };
            _context.Deposits.Add(new Deposit()
            {
                Annual = 20,
                Benefits = 0,
                Bonus = 0,
                Description = "Some deposit",
                InterestPaymentDate = new DateTime(2000, 01, 01),
                MaxAMount = 5000,
                MinAmount = 200,
                Name = "Deposit01",
                Replenishment = 0,
                AccountDeposits = new List<AccountDeposit>
                        { accountDeposit1 },
                Currency = "USD",
                InterestRate = 3
            });

            _context.Deposits.Add(new Deposit()
            {
                Annual = 20,
                Benefits = 0,
                Bonus = 0,
                Description = "Some deposit",
                InterestPaymentDate = new DateTime(2000, 01, 01),
                MaxAMount = 7000,
                MinAmount = 1000,
                Name = "Deposit02",
                Replenishment = 0,
                AccountDeposits = new List<AccountDeposit>
                        { accountDeposit2 },
                Currency = "USD",
                InterestRate = 2
            });

            _context.Deposits.Add(new Deposit()
            {
                Annual = 20,
                Benefits = 0,
                Bonus = 0,
                Description = "Some deposit",
                InterestPaymentDate = new DateTime(2000, 01, 01),
                MaxAMount = 2000,
                MinAmount = 300,
                Name = "Deposit03",
                Replenishment = 0,
                AccountDeposits = new List<AccountDeposit>
                        { accountDeposit3 },
                Currency = "USD",
                InterestRate = 1
            });

            var accountLoan1 = new AccountLoan()
            {
                Currency = "USD",
                Status = Status.IsActive.ToString(),
                Sum = 2000,
                Term = typeof(Term).GetValues().ToArray()[1],
                Employment = "SomePosition"
            };

            var accountLoan2 = new AccountLoan()
            {
                Currency = "USD",
                Status = Status.Pending.ToString(),
                Sum = 2000,
                Term = typeof(Term).GetValues().ToArray()[2],
                Employment = "SomePosition"
            };

            var accountLoan3 = new AccountLoan()
            {
                Currency = "USD",
                Status = Status.Pending.ToString(),
                Sum = 2000,
                Term = typeof(Term).GetValues().ToArray()[0],
                Employment = "SomePosition"
            };
            _context.Accounts.Add(
                new Account()
                {
                    UserId = (await _userManager.FindByNameAsync("admin")).Id,
                    AccountNumber = "123BANK123",
                    AccountName = "BankBalance",
                    Balance = 10000000000
                }
                );

            _context.Accounts.Add(
                        new Account()
                        {
                            UserId = (await _userManager.FindByNameAsync("Andrii")).Id,
                            AccountNumber = "98745612358",
                            AccountName = "andriiAcc",
                            Balance = 200000,
                            Cards = new List<Card>
                            {
                                new Card()
                                {
                                    CardNumber = _encryptionService.Encrypt("5987458521365478"),
                                    CCV = 985,
                                    CreatedAt = DateTime.Now,
                                    ExpireDate = DateTime.Now.AddYears(3),
                                    Status = CardStatus.Active.ToString(),
                                    CardType = "MasterCard"
                                },

                                new Card()
                                {
                                    CardNumber = _encryptionService.Encrypt("5987458521365479"),
                                    CCV = 985,
                                    CreatedAt = DateTime.Now,
                                    ExpireDate = DateTime.Now.AddYears(3),
                                    Status = CardStatus.Active.ToString(),
                                    CardType = "MasterCard"
                                },

                                new Card()
                                {
                                    CardNumber = _encryptionService.Encrypt("5987458521365470"),
                                    CCV = 985,
                                    CreatedAt = DateTime.Now,
                                    ExpireDate = DateTime.Now.AddYears(3),
                                    Status = CardStatus.Active.ToString(),
                                    CardType = "MasterCard"
                                }
                            },
                            AccountLoans = new List<AccountLoan>
                            { accountLoan1, accountLoan2 },
                            AccrueAccountLoans = new List<AccountLoan>
                            { accountLoan1, accountLoan3 },
                            Transactions = new List<TransactionsHistory>
                            { transaction },
                            AccountDeposits = new List<AccountDeposit>
                            { accountDeposit3 },
                        }
                    );

            _context.Accounts.Add(
                        new Account()
                        {
                            UserId = (await _userManager.FindByNameAsync("Jimmy")).Id,
                            AccountNumber = "123456789",
                            AccountName = "JimmyAcc",
                            Balance = 10000,
                            Cards = new List<Card>
                            {
                                new Card()
                                {
                                    CardNumber = _encryptionService.Encrypt("5987458511365478"),
                                    CCV = 123,
                                    CreatedAt = DateTime.Now,
                                    ExpireDate = DateTime.Now.AddYears(3),
                                    Status = CardStatus.Pending.ToString(),
                                    CardType = CardTypes.MasterCard.ToString()
                                },
                                new Card()
                                {
                                    CardNumber = _encryptionService.Encrypt("5987458511365478"),
                                    CCV = 339,
                                    CreatedAt = DateTime.Now,
                                    ExpireDate = DateTime.Now.AddYears(3),
                                    Status = CardStatus.Pending.ToString(),
                                    CardType = CardTypes.Visa.ToString()
                                },
                            },
                            AccountLoans = new List<AccountLoan>
                            { accountLoan1, accountLoan2 },
                            AccrueAccountLoans = new List<AccountLoan>
                            { accountLoan1, accountLoan3 },
                            Transactions = new List<TransactionsHistory>
                            { transaction },
                            AccountDeposits = new List<AccountDeposit>
                            { accountDeposit3 },
                        }
                    );

            _context.Accounts.Add(
                        new Account()
                        {
                            UserId = (await _userManager.FindByNameAsync("John")).Id,
                            AccountNumber = "555666111222",
                            AccountName = "johnAcc",
                            Balance = 200,
                            Cards = new List<Card>
                            {
                                new Card()
        {
            AccountId = 1,
                                    CardNumber = _encryptionService.Encrypt("2589854785986525"),
                                    CCV = 229,
                                    CreatedAt = DateTime.Now,
                                    ExpireDate = DateTime.Now.AddYears(3),
                                    Status = CardStatus.Active.ToString(),
                                    CardType = "Visa"
                                }
    },
                            AccountDeposits = new List<AccountDeposit>
                            { accountDeposit1, accountDeposit2
},
                            MoneyTransfers = new List<TransactionsHistory>
                            { transaction },
                            AccountLoans = new List<AccountLoan>
                            { accountLoan3 },
                            AccrueAccountLoans = new List<AccountLoan>
                            { accountLoan2 },
                        }
                    );

            _context.Loans.Add(new Loan()
            {
                Name = "TestLoan1",
                Percentage = 18,
                ServiceFee = 2,
                AccidentInsurance = 0,
                InsuranceLoan = 0,
                AccountLoans = new List<AccountLoan>
                        { accountLoan1 },
                Currency = "USD",
                InterestRate = 3,
                LoanInterestRate = 3,
                RepaymentSchedule = "Monthly",
                AdvancedPayment = "Without paying commission",
                MinAmount = 2000,
                MaxAmount = 5000,
                Term = typeof(Term).GetValues().ToArray()[0],
                Purpose = typeof(LoanPurposes).GetValues().ToArray()[0]
            });

            _context.Loans.Add(new Loan()
            {
                Name = "TestLoan2",
                Percentage = 18,
                ServiceFee = 2,
                AccidentInsurance = 0,
                InsuranceLoan = 200,
                AccountLoans = new List<AccountLoan>
                        { accountLoan2 },
                Currency = "USD",
                InterestRate = 10,
                LoanInterestRate = 10,
                RepaymentSchedule = "Monthly",
                AdvancedPayment = "Without paying commission",
                MinAmount = 3000,
                MaxAmount = 6000,
                Term = typeof(Term).GetValues().ToArray()[1],
                Purpose = typeof(LoanPurposes).GetValues().ToArray()[1]
            });

            _context.Loans.Add(new Loan()
            {
                Name = "TestLoan3",
                Percentage = 18,
                ServiceFee = 2,
                AccidentInsurance = 0,
                InsuranceLoan = 156,
                AccountLoans = new List<AccountLoan>
                        { accountLoan3 },
                Currency = "USD",
                InterestRate = 20,
                LoanInterestRate = 20,
                RepaymentSchedule = "Monthly",
                AdvancedPayment = "Without paying commission",
                MinAmount = 5000,
                MaxAmount = 100000,
                Term = typeof(Term).GetValues().ToArray()[2],
                Purpose = typeof(LoanPurposes).GetValues().ToArray()[2]
            });

            await _context.SaveChangesAsync();
        }
    }
}
