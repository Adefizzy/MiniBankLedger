
namespace MiniBankLedger.Domain;

public class Customer
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required int CustomerId { get; set; }
    public required string DateCreated { get; set; }


    public override string ToString()
    {
        return @$"
                    id: {CustomerId}, 
                    FirstName: {FirstName}, 
                    LastName: {LastName}, 
                    Email: {Email}, 
                    PhoneNumber: {PhoneNumber},
                    DateCreated: {DateCreated}
                ";
    }

}