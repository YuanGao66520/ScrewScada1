using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Module
{
    public class SerializableTool
    {
        public static void Save2File<T>(T obj,string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fs,obj);
                fs.Flush();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            {
                if (fs != null) fs.Close();
            }
        }
        public static T FromByFile<T>( string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return (T)bf.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
