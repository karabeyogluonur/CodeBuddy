using CB.Application.Abstractions.Services.Security;
using CB.Application.Utilities.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CB.Services.Security
{
    public class EncryptionService : IEncryptionService
    {
        #region Utilities
        private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, TripleDES.Create().CreateEncryptor(key, iv), CryptoStreamMode.Write))
            {
                var toEncrypt = Encoding.Unicode.GetBytes(data);
                cs.Write(toEncrypt, 0, toEncrypt.Length);
                cs.FlushFinalBlock();
            }

            return ms.ToArray();
        }

        private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using var ms = new MemoryStream(data);
            using var cs = new CryptoStream(ms, TripleDES.Create().CreateDecryptor(key, iv), CryptoStreamMode.Read);
            using var sr = new StreamReader(cs, Encoding.Unicode);
            return sr.ReadToEnd();
        }

        #endregion


        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            using var provider = TripleDES.Create();
            provider.Key = Encoding.ASCII.GetBytes(EncryptionDefaults.PrivateKey[0..16]);
            provider.IV = Encoding.ASCII.GetBytes(EncryptionDefaults.PrivateKey[8..16]);

            var buffer = Convert.FromBase64String(cipherText);
            return DecryptTextFromMemory(buffer, provider.Key, provider.IV);
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            using var provider = TripleDES.Create();
            provider.Key = Encoding.ASCII.GetBytes(EncryptionDefaults.PrivateKey[0..16]);
            provider.IV = Encoding.ASCII.GetBytes(EncryptionDefaults.PrivateKey[8..16]);

            var encryptedBinary = EncryptTextToMemory(plainText, provider.Key, provider.IV);
            return Convert.ToBase64String(encryptedBinary);
        }
    }
}
