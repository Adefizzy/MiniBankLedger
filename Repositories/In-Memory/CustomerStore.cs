using System;
using MiniBankLedger.Repositories.Interface;
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;

namespace MiniBankLedger.Repositories.In_Memory;

public class CustomerStore : ICustomer
{
    private readonly static List<Customer> Customers = [];
    public static Customer? CreateCustomer(CustomerDto customerDto)
    {
        int customerId = Customers.Count + 1;

        string dateCreated = DateTime.Now.ToString();

        bool isExisting = Customers.Any(c => c.Email == customerDto.Email || c.PhoneNumber == customerDto.PhoneNumber);

        if (isExisting)
        {
            throw new UniqueExceptions("Invalid: Email and Phone Number must be unique");
        }

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

    public List<Customer> ListCustomers()
    {
        return Customers;
    }

    public void GetCustomerAccount()
    {

    }

    public void CreateAccount()
    {

    }
}
