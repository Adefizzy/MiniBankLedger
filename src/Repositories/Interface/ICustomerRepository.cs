using System;
using MiniBankLedger.Domain;

namespace MiniBankLedger.Repositories.Interface;

public interface ICustomerRepository
{
     Customer Save(Customer customer);
    List<Customer> FindAll();

    Customer? FindById(int id);

    Customer? FindByEmail(string email);
}
