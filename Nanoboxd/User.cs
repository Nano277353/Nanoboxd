namespace Classes;

public class User
{
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    
    public void EnterCredentials()
    {
        Console.Write("Username: ");
        Username = Console.ReadLine() ?? string.Empty; 
        
        Console.Write("Password: ");
        Password = Console.ReadLine() ?? string.Empty;
    }
}