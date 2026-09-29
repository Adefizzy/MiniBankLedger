using System;
using MiniBankLedger.Repositories.Interface;
using MiniBankLedger.Domain;

namespace MiniBankLedger.Repositories.In_Memory;

public class InMemoryCustomerRepository : ICustomerRepository
{
    private readonly static List<Customer> Customers = [];


    public static Customer Save(Customer customer)
    {
        Customers.Add(customer);

        return customer;
    }

    public static List<Customer> FindAll()
    {
        return Customers;
    }
}
