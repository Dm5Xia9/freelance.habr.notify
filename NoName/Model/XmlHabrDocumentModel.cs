using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NoName
{
    class XmlHabrDocumentModel
    {
        public XmlDocument habrXmlDocument;
        public XmlHabrDocumentModel()
        {
            habrXmlDocument = XmlHabrDocument();
        }
        private XmlDocument XmlHabrDocument()
        {
            Stream responseStream = null;
            try
            {
                WebRequest request = WebRequest.Create("https://freelance.habr.com/user_rss_tasks/vZHxL8Voxt9AJzsZtjTVAw==");
                WebResponse webResponse = request.GetResponse();
                responseStream = webResponse.GetResponseStream();
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось получить данные ", e);
            }
            XmlDocument HabrXmlDocument = new XmlDocument();
            HabrXmlDocument.Load(responseStream);
            return HabrXmlDocument;
        }
    }
}
