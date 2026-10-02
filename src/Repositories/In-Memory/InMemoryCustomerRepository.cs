using System;
using MiniBankLedger.Repositories.Interface;
using MiniBankLedger.Domain;

namespace MiniBankLedger.Repositories.In_Memory;

public class InMemoryCustomerRepository : ICustomerRepository
{
    private readonly static List<Customer> Customers = [];


    public Customer Save(Customer customer)
    {
        Customers.Add(customer);

        return customer;
    }

    public List<Customer> FindAll()
    {
        return Customers;
    }

    public Customer? FindById(int id)
    {
        return Customers.Find(c => c.CustomerId == id);
    }

    public Customer? FindByEmail(string email)
    {
        return Customers.Find(c => c.Email == email);
    }
}
