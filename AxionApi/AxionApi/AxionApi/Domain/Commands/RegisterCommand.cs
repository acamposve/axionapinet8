namespace Domain.Commands;

public sealed class RegisterCommand(string firstname, string lastname, string phonenumber, string address, string username, string password, string email)
{
    public string Firstname { get; init; } = firstname;
    public string Lastname { get; init; } = lastname;
    public string PhoneNumber { get; init; } = phonenumber;
    public string Address { get; init; } = address;
    public string Username { get; init; } = username;
    public string Password { get; init; } = password;
    public string Email { get; init; } = email;
}
