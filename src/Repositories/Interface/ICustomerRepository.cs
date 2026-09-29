using System;
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;

namespace MiniBankLedger.Repositories.Interface;

public interface ICustomerRepository
{
    abstract static Customer Save(CustomerDto customerDto);
    abstract static List<Customer> FindAll();
}
