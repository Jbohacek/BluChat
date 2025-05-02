namespace BluChat.Core.Common.Interfaces;

public interface IPasswordManager
{
    public string HashPassword(string password);

    public bool VerifyPassword(string password, string hashedPassword);
}