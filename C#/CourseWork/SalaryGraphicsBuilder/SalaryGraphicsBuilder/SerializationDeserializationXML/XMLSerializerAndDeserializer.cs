using SalaryGraphicsBuilder.CodeOfExtractingData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SalaryGraphicsBuilder.SerializationDeserializationXML
{
    public static class XMLSerializerAndDeserializer
    {
        public static string SerializeProfession(Profession CurrentProfession)
        {
            XmlSerializer XMLSerializerForProfession = new XmlSerializer(typeof(Profession));
            StringWriter StringWriterForProfession = new StringWriter();
            string XMLFileForCurrentProfession = string.Empty;
            using (XmlWriter XMLWriterForProfession = XmlWriter.Create(StringWriterForProfession))
            {
                XMLSerializerForProfession.Serialize(XMLWriterForProfession, CurrentProfession);
                XMLFileForCurrentProfession = StringWriterForProfession.ToString();
            }
            //возвращаем XML как строку
            return XMLFileForCurrentProfession;
        }
    }
}
