using System;
using MiniBankLedger.Repositories.Interface;
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;

namespace MiniBankLedger.Repositories.In_Memory;

public class InMemoryCustomerRepository : ICustomerRepository
{
    private readonly static List<Customer> Customers = [];


    public static Customer Save(CustomerDto customerDto)
    {
        int customerId = Customers.Count + 1;

        string dateCreated = DateTime.UtcNow.ToString();

        Customer customer = new()
        {
            FirstName = customerDto.FirstName,
            LastName = customerDto.LastName,
            PhoneNumber = customerDto.PhoneNumber,
            Email = customerDto.Email,
            DateCreated = dateCreated,
            CustomerId = customerId
        };

        Customers.Add(customer);

        return customer;
    }

    public static List<Customer> FindAll()
    {
        return Customers;
    }
}
