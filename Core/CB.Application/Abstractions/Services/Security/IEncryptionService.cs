namespace CB.Application.Abstractions.Services.Security
{
    public interface IEncryptionService
    {
        public string Encrypt(string plainText);
        public string Decrypt(string cipherText);
    }
}
