using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PasswordKeeper.Core
{
    public static class VaultSerializer
    {
        public static void SaveData(Vault database)
        {
            using (Stream stream = File.Open(Vault.fileName, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, database);
            }
        }

        public static Vault LoadData()
        {
            try
            {
                using (Stream stream = File.Open(Vault.fileName, FileMode.Open))
                {
                    var binaryFormatter = new BinaryFormatter();
                    Vault database = (Vault)binaryFormatter.Deserialize(stream);
                    if (database == null)
                        return new Vault();
                    else
                        return database;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
