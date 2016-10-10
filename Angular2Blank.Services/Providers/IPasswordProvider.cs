namespace Angular2Blank.Services.Providers
{
    public interface IPasswordProvider
    {
        string GetHash(string password);
        bool VerifyPassword(string hash, string password);
    }
}
