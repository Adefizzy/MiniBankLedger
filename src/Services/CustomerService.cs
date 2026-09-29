using System;
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Exceptions;
using MiniBankLedger.Repositories.In_Memory;
using MiniBankLedger.Repositories.Interface;

namespace MiniBankLedger.Services;

public class CustomerService(ICustomerRepository customerRepository)
{
    public Customer? CreateCustomer(CustomerDto customerDto)
    {

        try
        {
            bool isExisting = customerRepository.FindAll()
                    .Any(c => c.Email == customerDto.Email || c.PhoneNumber == customerDto.PhoneNumber);


            if (isExisting)
            {
                throw new UniqueExceptions("Invalid: Email and Phone Number must be unique");
            }

            List<Customer> customers = customerRepository.FindAll();

            int customerId = customers.Count + 1;

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

            Customer savedCustomer = InMemoryCustomerRepository.Save(customer);

            return customer;
        }
        catch (UniqueExceptions e)
        {
            Console.WriteLine($"********{e.Message}*********");
            return null;
        }
    }
}
