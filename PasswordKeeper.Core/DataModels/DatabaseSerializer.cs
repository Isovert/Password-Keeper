using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PasswordKeeper.Core
{
    public static class DatabaseSerializer
    {
        public static void SaveData(Database database)
        {
            using (Stream stream = File.Open(Database.fileName, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, database);
            }
        }

        public static Database LoadData()
        {
            try
            {
                using (Stream stream = File.Open(Database.fileName, FileMode.Open))
                {
                    var binaryFormatter = new BinaryFormatter();
                    Database database = (Database)binaryFormatter.Deserialize(stream);
                    if (database == null)
                        return new Database();
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
