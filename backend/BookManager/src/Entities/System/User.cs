using Microsoft.EntityFrameworkCore;

[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User : BaseModel{
    public string Username {get; set;}
    public string Name {get; set;}
    public string Email {get; set;}
    public UserRole UserRole {get; set;}
    public string PasswordHash {get; set;}
}

public enum UserRole{
    Admin,
    Customer
}