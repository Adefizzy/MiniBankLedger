using System;
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;

namespace MiniBankLedger.Repositories.Interface;

public interface ICustomer
{
    abstract static Customer? CreateCustomer(CustomerDto customerDto);
    List<Customer> ListCustomers();
    void GetCustomerAccount();
    void CreateAccount();
}
