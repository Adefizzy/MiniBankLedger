using System;
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Exceptions;
using MiniBankLedger.Repositories.In_Memory;
using MiniBankLedger.Repositories.Interface;

namespace MiniBankLedger.Services;

public class CustomerService(ICustomerRepository customerRepository)
{
    public CustomersResponse? CreateCustomer(CustomerDto customerDto)
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


            Customer customer = new()
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                PhoneNumber = customerDto.PhoneNumber,
                Email = customerDto.Email,
                CustomerId = customerId
            };

            Customer savedCustomer = InMemoryCustomerRepository.Save(customer);

            return new CustomersResponse(
                CustomerId: customer.CustomerId,
                FirstName: customer.FirstName,
                LastName: customer.LastName,
                Email: customer.Email,
                PhoneNumber: customer.PhoneNumber
                );
        }
        catch (UniqueExceptions e)
        {
            Console.WriteLine($"********{e.Message}*********");
            return null;
        }
    }



    public CustomersResponse? SearchCustomer(string idOrEmail)
    {
        try
        {
            Customer? customer;
            if (int.TryParse(idOrEmail, out int id))
            {
                customer = customerRepository.FindById(id);
            }
            else if (idOrEmail.Contains('@'))
            {
                customer = customerRepository.FindByEmail(email: idOrEmail);
            }
            else
            {
                throw new InvalidEntryException("Entry must be id or email");
            }

            if (customer is not null)
            {
                return new CustomersResponse(
                    CustomerId: customer.CustomerId,
                    FirstName: customer.FirstName,
                    LastName: customer.LastName,
                    Email: customer.Email,
                    PhoneNumber: customer.PhoneNumber
                    );
            }

            throw new NotFoundException($"Customer with {idOrEmail} is not found");
        }
        catch (InvalidEntryException e)
        {
            Console.WriteLine($"********{e.Message}*********");
            return null;
        }
        catch (NotFoundException e)
        {
            Console.WriteLine($"********{e.Message}*********");
            return null;
        }


    }


    public CustomersResponse[] AllCustomers()
    {
        List<Customer> customers = customerRepository.FindAll();

        return [.. customers.Select(c => new CustomersResponse(
            CustomerId: c.CustomerId, FirstName: c.FirstName, LastName: c.LastName, Email: c.Email, PhoneNumber: c.PhoneNumber))];
    }
}
