using System;
using MiniBankLedger.Domain;

namespace MiniBankLedger.Repositories.Interface;

public interface ICustomerRepository
{
    abstract static Customer Save(Customer customer);
    abstract static List<Customer> FindAll();
}
