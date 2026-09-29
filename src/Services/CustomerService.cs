using System;
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Repositories.In_Memory;

namespace MiniBankLedger.Services;

public class CustomerService
{
    public Customer? CreateCustomer(CustomerDto customerDto)
    {

        try
        {
            bool isExisting = InMemoryCustomerRepository.FindAll().Any(c => c.Email == customerDto.Email || c.PhoneNumber == customerDto.PhoneNumber);

            if (isExisting)
            {
                throw new UniqueExceptions("Invalid: Email and Phone Number must be unique");
            }

            Customer customer = InMemoryCustomerRepository.Save(customerDto);

            return customer;
        }
        catch (UniqueExceptions e)
        {
            Console.WriteLine($"********{e.Message}*********");
            return null;
        }
    }
}
