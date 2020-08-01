using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace NoName
{
    class MainParsingModel
    {
        public List<string> listTitle;
        public List<string> listDate;
        public List<string> listLink;
        public List<string> listDescription;

        XmlHabrDocumentModel habrXmlDocumentModel = new XmlHabrDocumentModel();
        XmlDocument habrXmlDocument;

        int numParsing;
        public MainParsingModel(int _numParsing)
        {
            numParsing = _numParsing;
            habrXmlDocument = habrXmlDocumentModel.habrXmlDocument;
            listTitle = ParsingTitleNewHabr();
            listDate = ParsingPubDateNewHabr();
            listLink = ParsingLinkNewHabr();
            listDescription = ParsingDescriptionNewHabr();
        }
        private List<string> ParsingTitleNewHabr()
        {
            List<string> TitleParsing = new List<string>();

            for (int i = 1; i < numParsing + 1; i++)
                TitleParsing.Add(habrXmlDocument.GetElementsByTagName("title")[i].InnerText);
            return TitleParsing;
        }
        private List<string> ParsingPubDateNewHabr()
        {
            List<string> PubDateParsing = new List<string>();

            for (int i = 0; i < numParsing; i++)
                PubDateParsing.Add(habrXmlDocument.GetElementsByTagName("pubDate")[i].InnerText);
            return PubDateParsing;
        }
        private List<string> ParsingLinkNewHabr()
        {
            List<string> LinkParsing = new List<string>();

            for (int i = 1; i < numParsing + 1; i++)
                LinkParsing.Add(habrXmlDocument.GetElementsByTagName("link")[i].InnerText);
            return LinkParsing;
        }
        private List<string> ParsingDescriptionNewHabr()
        {
            List<string> DescriptionParsing = new List<string>();

            for (int i = 0; i < numParsing; i++)
            {
                DescriptionParsing.Add(habrXmlDocument.GetElementsByTagName("description")[i].InnerText);
                DescriptionParsing[i] = DescriptionParsing[i].Replace("<br>", " \n ");
                DescriptionParsing[i] = Regex.Replace(DescriptionParsing[i], "<[^>]+>", string.Empty);
            }
            return DescriptionParsing;
        }
    }
}
