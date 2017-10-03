using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;

namespace PasswordKeeper.Core
{
    sealed class EncryptorAES
    {
        public byte[] EncryptStringToBytes(string plainText, SecureString password, byte[] IV)
        {
            if (plainText == null || plainText.Length == 0)
                throw new ArgumentNullException("plainText");
            if (password == null || password.Length == 0)
                throw new ArgumentNullException("password");
            if (IV == null || IV.Length == 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = ConvertToByte32Array(password.Unsecure());
                aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        public string DecryptStringFromBytes(byte[] cipherText, SecureString password, byte[] IV)
        {
            if (cipherText == null || cipherText.Length == 0)
                throw new ArgumentNullException("cipherText");
            if (password == null || password.Length == 0)
                throw new ArgumentNullException("password");
            if (IV == null || IV.Length == 0)
                throw new ArgumentNullException("IV");

            byte[] Key = ConvertToByte32Array(password.Unsecure());
            string plainText = null;            
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plainText;
        }

        private byte[] ConvertToByte32Array(string text)
        {
            byte[] byte32Array = new byte[32];
            for (int i = 0; i < text.Length; i++)
            {
                byte32Array[i] = (byte)text[i];
            }
            return byte32Array;
        }
    }
}