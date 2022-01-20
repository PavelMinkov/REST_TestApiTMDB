using System;
using System.IO;
using System.Xml.Serialization;
using TestApiTMDB.DataModels;

namespace TestApiTMDB.Utils
{
    class PropertyReader
    {
        private Properties properties;

        public PropertyReader()
        {
            ReadPropertiesFromXML();
        }

        private void ReadPropertiesFromXML()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(Properties));
            try
            {
                string path = Directory.GetCurrentDirectory();
                path = path.Substring(0, path.Length - 17);
                path = Path.Combine(path, @"DataTest\Properties.xml");
                using (Stream fStream = File.OpenRead(path))
                {
                    properties = (Properties)xmlFormat.Deserialize(fStream);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Can`t open properties file");
            }
        }
        public string GetURL()
        {
            return properties.URL;
        }
        public string GetApiKey()
        {
            return properties.Api_Key;
        }
    }
}
