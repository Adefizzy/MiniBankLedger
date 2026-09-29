using System;
using MiniBankLedger.Domain;

namespace MiniBankLedger.Repositories.Interface;

public interface ICustomerRepository
{
    abstract static Customer Save(Customer customer);
    List<Customer> FindAll();

    Customer? FindById(int id);

    Customer? FindByEmail(string email);
}
