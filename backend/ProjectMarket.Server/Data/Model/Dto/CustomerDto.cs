namespace ProjectMarket.Server.Data.Model.Dto;

public class CustomerDto(
   int? id,
   string name,
   string email,
   string password,
   DateTime? registrationDate)
{
    public int? CustomerId { get; init; } = id;
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public DateTime RegistrationDate { get; set; } = registrationDate ?? DateTime.Now;
}