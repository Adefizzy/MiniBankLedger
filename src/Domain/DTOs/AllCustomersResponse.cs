namespace MiniBankLedger.Domain.DTOs;

public record class CustomersResponse(int CustomerId, string FirstName, string LastName, string Email, string PhoneNumber)
{

}
