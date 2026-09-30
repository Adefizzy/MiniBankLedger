namespace MiniBankLedger.Domain.DTOs;

public record CustomerDto(
    string FirstName, 
    string LastName, 
    string Email, 
    string PhoneNumber
    );
