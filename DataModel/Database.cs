using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using PasswordKeeper.Utillities;

namespace PasswordKeeper.DataModel
{
    [Serializable]
    sealed class Database
    {
        public static readonly string fileName = "Vault.bin";
        private List<FolderModel> _rootFolderModels;
        public List<FolderModel> RootFolderModels
        {
            get { return _rootFolderModels; }
            private set { _rootFolderModels = value; }
        }
        private byte[] _initializationVector;
        private byte[] _encodedMagicWordByte;
        private byte[] _magicWordByte;
        
        [NonSerialized] private static byte[] _key;
        internal static byte[] Key
        {
            get { return _key; }
        }

        public static bool Exists
        {
            get { return File.Exists(fileName); }
        }

        public Database()
        {

        }

        public Database(string magicWord, string password)
        {
            RootFolderModels = new List<FolderModel>();
            var random = RandomNumberGenerator.Create();
            _initializationVector = new byte[16];
            random.GetBytes(_initializationVector);
            byte[] salt = CreateSalt(32);
            _magicWordByte = GenerateSaltedHash(ConvertToByte(magicWord), salt);
            byte[] passwordByte = ConvertToByte32Array(password);
            EncryptorAES AES = new EncryptorAES();
            _encodedMagicWordByte = AES.EncryptStringToBytes(ConvertByteToString(_magicWordByte), passwordByte, _initializationVector);
        }

        internal void CheckPassword(string password)
        {
            try
            {
                byte[] passwordByte = ConvertToByte32Array(password);
                EncryptorAES AES = new EncryptorAES();
                string decodedMagicWord = AES.DecryptStringFromBytes(_encodedMagicWordByte, passwordByte, _initializationVector);
                byte[] decodedMagicWordByte = ConvertToByte(decodedMagicWord);
                if (Enumerable.SequenceEqual(decodedMagicWordByte, _magicWordByte))
                {
                    _key = passwordByte;
                }
                //else
                //{
                //    throw new Exception("Wrong password!");
                //}
            }
            catch
            {
                throw new UnauthorizedAccessException("Wrong password!");
            }
        }

        private byte[] ConvertToByte(string text)
        {
            byte[] buffer = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                buffer[i] = (byte)text[i];
            }
            return buffer;
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

        private string ConvertByteToString(byte[] byteArray)
        {
            //change to string builder
            string text = "";
            for (int i = 0; i < byteArray.Length; i++)
            {
                text = text + (char)byteArray[i];
            }
            return text;
        }

        private byte[] CreateSalt(int size)
        {
            var random = System.Security.Cryptography.RandomNumberGenerator.Create();
            byte[] buffer = new byte[size];
            random.GetBytes(buffer);
            return buffer;
        }

        private byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];
            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }
            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }
    }
}
