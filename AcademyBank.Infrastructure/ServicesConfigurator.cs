using AcademyBank.BLL.Helpers.Implementations;
using AcademyBank.BLL.Services;
using AcademyBank.BLL.Services.Implementations;
using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.DAL;
using AcademyBank.DAL.Repositories.Implementations;
using AcademyBank.DAL.Repositories.Interfaces;
using AcademyBank.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AcademyBank.ScheduledJobs;
using AcademyBank.ScheduledJobs.CronJobConfig;
using AcademyBank.ScheduledJobs.CronJobConfig.Services;

namespace AcademyBank.Infrastructure
{
    public static class ServicesConfigurator
    {
        public static void AddProjectDatadaseConfiguration(this IServiceCollection services)
        {
            services.AddDbContext<BankDbContext>();
        }

        public static void AddProjectRepositories(this IServiceCollection services)
        { 
            services.AddScoped<IRepository<Loan>, LoanRepository>();
            services.AddScoped<IRepository<TransactionsHistory>, HistoryRepository>();
            services.AddScoped<IRepository<Deposit>, DepositRepository>();
            services.AddScoped<IRepository<AccountDeposit>, AccountDepositrepository>();
            services.AddScoped<IRepository<Card>, CardRepository>();
            services.AddScoped<ICardRepository<Card>, CardRepository>();
            services.AddScoped<IRepository<UserInfo>, UserInfoRepository>();
            services.AddScoped<IRepository<AccountLoan>, AccountLoanRepository>();
            services.AddScoped<IAccountLoanRepository<AccountLoan>, AccountLoanRepository>();
            services.AddScoped<IRepository<Account>, AccountRepository>();
            services.AddScoped<IAccountRepository<Account>, AccountRepository>();
            services.AddScoped<IRepository<AccountDeposit>, AccountDepositrepository>();
            services.AddScoped<IAccountDepositRepository<AccountDeposit>, AccountDepositrepository>();
            services.AddScoped<IAccountLoanRepository<AccountLoan>, AccountLoanRepository>();
            services.AddScoped<ICardRequestRepository<Card>, CardRequestRepository>();
            services.AddScoped<ILoginReportRepository<LoginReport>, LoginReportRepository>();
            services.AddScoped<ITransfersReportRepository<TransfersReport>, TransfersReportRepository>();
            services.AddScoped<ICountersReportRepository<CountersReport>, CountersReportRepository>();
            services.AddScoped<IFiltersReportRepository<FiltersReport>, FiltersReportRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IAccountLoanService, AccountLoanService>();
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<IDepositService, DepositService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IAccountDepositService, AccountDepositService>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ICardRequestService, CardRequestService>();
            services.AddScoped<ILoginReportService, LoginReportService>();
            services.AddScoped<ICountersReportService, CountersReportService>();
            services.AddScoped<ITransfersReportService, TransfersReportService>();
            services.AddScoped<IFiltersReportService, FiltersReportService>();

        }

        public static void AddCronos(this IServiceCollection services)
        {
            services.AddCronJob<DepositCronJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = "50 12 * * *";
            });
            services.AddCronJob<LoanCronJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = "50 12 * * *";
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<SignInManager<User>>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<RoleManager<IdentityRole<int>>>();

            services.AddIdentity<User, IdentityRole<int>>(config =>
            {
                config.Password.RequiredLength = Convert.ToInt32(configuration.GetSection("Data").GetSection("RequiredLength").Value);
                config.Password.RequiredUniqueChars = Convert.ToInt32(configuration.GetSection("Data").GetSection("RequiredUniqueChars").Value);
                config.Password.RequireDigit = Convert.ToBoolean(configuration.GetSection("Data").GetSection("RequireDigit").Value);
                config.Password.RequireLowercase = Convert.ToBoolean(configuration.GetSection("Data").GetSection("RequireLowercase").Value);
                config.Password.RequireUppercase = Convert.ToBoolean(configuration.GetSection("Data").GetSection("RequireUppercase").Value);
            })
                 .AddEntityFrameworkStores<BankDbContext>()
                 .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "app_cookie";
                config.LoginPath = "/User/Login";
            });

            services.AddAuthorizationCore(config =>
            {
                config.DefaultPolicy = new AuthorizationPolicyBuilder()
                                            .RequireAuthenticatedUser()
                                            .RequireClaim(ClaimTypes.Name)
                                            .Build();
                config.AddPolicy(Constants.OnlyAdmin, p =>
                {
                    p.RequireRole(Constants.AdminRole);
                });
                config.AddPolicy(Constants.OnlyUser, p =>
                {
                    p.RequireUserName(Constants.User);
                });
            });
        }

        public static void AddProjectJwt(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["JwtIssuer"],
                        ValidAudience = configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
        }
    }
}
