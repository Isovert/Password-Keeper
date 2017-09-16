using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.DataModel
{
    static class DatabaseSerializer
    {
        internal static void SaveData(Database database)
        {
            using (Stream stream = File.Open(Database.fileName, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, database);
            }
        }

        internal static Database LoadData()
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
