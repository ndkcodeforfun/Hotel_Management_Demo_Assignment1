using AutoMapper;
using BAL.ModelView;
using BAL.Service.Interfaces;
using DAL.Models;
using DAL.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service.Implements
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public CustomerView? AuthenticateUser(LoginInfoView loginInfoView)
        {
            try
            {
                var customer = _unitOfWork.CustomerRepository.Find(e => e.EmailAddress == loginInfoView.EmailAddress && e.Password == loginInfoView.Password).FirstOrDefault();
                if (customer == null)
                {
                    return null;
                }
                var customerView = _mapper.Map<CustomerView>(customer);
                return customerView;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            
        }

        public bool CreateAccountCustomer(RegisterInfoView newAccount)
        {
            try
            {
                bool status = false;
                newAccount.CustomerStatus = 1;
                var account = _mapper.Map<Customer>(newAccount);
                _unitOfWork.CustomerRepository.Insert(account);
                _unitOfWork.Save();
                var insertedAccount = _unitOfWork.CustomerRepository.Find(a => a.EmailAddress == newAccount.EmailAddress).FirstOrDefault();
                if (insertedAccount != null)
                {
                    status = true;
                    return status;
                }
                return status;
            }
            catch (Exception ex)
            {
                var insertedAccount = _unitOfWork.CustomerRepository.Find(a => a.EmailAddress == newAccount.EmailAddress).FirstOrDefault();
                if (insertedAccount != null)
                {
                    _unitOfWork.CustomerRepository.Delete(insertedAccount);
                    _unitOfWork.Save();
                }
                throw new Exception(ex.Message);
            }
        }

        public Customer GetAccountByUserName(string userName)
        {
            try
            {
                Customer customer = _unitOfWork.CustomerRepository.Find(n => n.CustomerFullName == userName).FirstOrDefault();
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CustomerView> GetAllActive()
        {
            try
            {
                var customers = _unitOfWork.CustomerRepository.Get().Where(c => c.CustomerStatus == 1).ToList();
                List<CustomerView> customerList = new List<CustomerView>();
                foreach (var customer in customers)
                {
                    var customerView = _mapper.Map<CustomerView>(customer);
                    customerList.Add(customerView);
                }
                return customerList;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CustomerView> GetAll()
        {
            try
            {
                var customers = _unitOfWork.CustomerRepository.Get().ToList();
                List<CustomerView> customerList = new List<CustomerView>();
                foreach (var customer in customers)
                {
                    var customerView = _mapper.Map<CustomerView>(customer);
                    customerList.Add(customerView);
                }
                return customerList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CustomerView? GetCusByID(int id)
        {
            try
            {
                var customer = _unitOfWork.CustomerRepository.GetByID(id);
                if(customer == null)
                {
                    return null;
                }
                var customerView = _mapper.Map<CustomerView>(customer);
                return customerView;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsExistedEmail(string email)
        {
            try
            {
                bool status = true;
                var existed = _unitOfWork.CustomerRepository.Find(e => e.EmailAddress == email).FirstOrDefault();
                if (existed == null)
                {
                    status = false;
                }
                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateCustomer(int id, RegisterInfoView updateCustomer)
        {
            try
            {
                bool status = false;
                var checkExist = _unitOfWork.CustomerRepository.GetByID(id);
                if (checkExist != null)
                {

                    _mapper.Map(updateCustomer, checkExist);
                    _unitOfWork.CustomerRepository.Update(checkExist);
                    _unitOfWork.Save(); 
                    status = true;
                    return status;
                    
                    
                }
                return status;
            }
            catch (Exception ex)
            {
               
                throw new Exception(ex.Message);
            }
        }

        public bool isSetStatusCustomer(int id)
        {
            try
            {
                bool status = false;
                var customerExisted = _unitOfWork.CustomerRepository.GetByID(id);
                if (customerExisted != null)
                {
                    if (customerExisted.CustomerStatus == 1)
                    {
                        customerExisted.CustomerStatus = 2;
                        _unitOfWork.CustomerRepository.Update(customerExisted);
                        _unitOfWork.Save();
                        status = true;
                        return status;
                    }
                    else if (customerExisted.CustomerStatus == 2)
                    {
                        customerExisted.CustomerStatus = 1;
                        _unitOfWork.CustomerRepository.Update(customerExisted);
                        _unitOfWork.Save();
                        status = true;
                        return status;
                    }

                }
                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public string? HashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public (string accessToken, string refreshToken) GenerateTokens(CustomerView account)
        {
            throw new NotImplementedException();
        }
    }
}
