using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Shared;
using static System.Console;

namespace MiniBankLedger.Input;
static class CustomerInput
{
    public static CustomerDto CreateCustomerInput()
    {

        string firstName = RequiredInputs.IsRequired(() =>
          {
              Write("Enter customer first name: ");
              return ReadLine();
          });

        string lastName = RequiredInputs.IsRequired(() =>
        {
            Write("Enter customer last name: ");
            return ReadLine();

        });

        string email = RequiredInputs.IsRequired(() =>
       {
           Write("Enter customer email: ");
           return ReadLine();
       });

        string phoneNumber = RequiredInputs.IsRequired(() =>
        {
            Write("Enter customer phone number: ");
            return ReadLine();
        });

        return new CustomerDto(firstName, lastName, email, phoneNumber);
    }
}