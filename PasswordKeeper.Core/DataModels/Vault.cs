using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;

namespace PasswordKeeper.Core
{
    [Serializable]
    public sealed class Vault
    {
        public static readonly string fileName = "Vault.bin";
        private List<FolderModel> _rootFolderModels;
        public List<FolderModel> RootFolderModels
        {
            get { return _rootFolderModels; }
            private set { _rootFolderModels = value; }
        }
        private byte[] _initializationVector;
        private byte[] _encodedWordByte;
        private byte[] _wordByte;
        
        [NonSerialized]
        private static SecureString _key;
        internal static SecureString Key
        {
            get { return _key; }
        }

        public static bool Exists
        {
            get { return File.Exists(fileName); }
        }

        public Vault()
        {

        }

        /// <summary>
        /// Constructor for first initialization of database
        /// </summary>
        /// <param name="magicWord"></param>
        /// <param name="password"></param>
        public Vault(SecureString password)
        {
            //change that constructor add open and seal methods
            //also vault should be responsible for getting password for specified login entry
            RootFolderModels = new List<FolderModel>();

            //for testing purposes

            RootFolderModels.Add(new FolderModel("A"));
            RootFolderModels.Add(new FolderModel("B"));

            //for testing purposes


            var random = RandomNumberGenerator.Create();
            _initializationVector = new byte[16];
            random.GetBytes(_initializationVector);
            byte[] salt = CreateSalt(32);
            //Fix that
            _wordByte = GenerateSaltedHash( ConvertToByte("Temporal") , salt);
            EncryptorAES AES = new EncryptorAES();
            _encodedWordByte = AES.EncryptStringToBytes(ConvertByteToString(_wordByte), password, _initializationVector);
        }
        
        internal void TryLogIn(SecureString password)
        {
            try
            {
                byte[] passwordByte = ConvertToByte32Array(password.Unsecure());
                EncryptorAES AES = new EncryptorAES();
                string decodedMagicWord = AES.DecryptStringFromBytes(_encodedWordByte, password, _initializationVector);
                byte[] decodedMagicWordByte = ConvertToByte(decodedMagicWord);
                if (Enumerable.SequenceEqual(decodedMagicWordByte, _wordByte))
                {
                    _key = password;
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