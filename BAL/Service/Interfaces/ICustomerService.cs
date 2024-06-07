using BAL.ModelView;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service.Interfaces
{
    public interface ICustomerService
    {
        CustomerView AuthenticateUser(LoginInfoView loginInfoView);

        string? HashPassword(string password);
        (string accessToken, string refreshToken) GenerateTokens(CustomerView account);

        CustomerView GetCusByID(int id);

        List<CustomerView> GetAllActive();

        List<CustomerView> GetAll();

        bool IsExistedEmail(string email);

        Customer GetAccountByUserName(string userName);


        bool CreateAccountCustomer(RegisterInfoView newAccount);

        bool UpdateCustomer(int id, RegisterInfoView updateCustomer);

        bool isSetStatusCustomer(int id);
    }
}
