using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace NoName
{
    class ActualParsingModel
    {

        public List<string> newListTitle;
        public List<string> newListDate;
        public List<string> newListLink;
        public List<string> newListDescription;

        List<string> ListTitle;

        XmlHabrDocumentModel habrXmlDocumentModel = new XmlHabrDocumentModel();
        XmlDocument habrXmlDocument;

        int NumActual = 0;
        public ActualParsingModel(MainVM mainVM = null)
        {
            habrXmlDocument = habrXmlDocumentModel.habrXmlDocument;
            ListTitle = mainVM.listTitle;
            newListTitle = ActualParsingTitleNewHabr();
            newListDate = ActualParsingPubDateNewHabr();
            newListLink = ActualParsingLinkNewHabr();
            newListDescription = ActualParsingDescriptionNewHabr();
        }
        private List<string> ActualParsingTitleNewHabr()
        {
            List<string> TitleParsing = new List<string>();
            string Title;
            for (int i = 1; i < ListTitle.Count; i++)
            {
                Title = habrXmlDocument.GetElementsByTagName("title")[i].InnerText;
                if (ListTitle[0] == Title) { break; }
                else { TitleParsing.Add(Title); NumActual++; }
            }
            return TitleParsing;
        }
        private List<string> ActualParsingPubDateNewHabr()
        {
            List<string> PubDateParsing = new List<string>();
            for (int i = 0; i < NumActual; i++)
            {
                PubDateParsing.Add(habrXmlDocument.GetElementsByTagName("pubDate")[i].InnerText);
            }
            return PubDateParsing;
        }
        private List<string> ActualParsingLinkNewHabr()
        {
            List<string> LinkParsing = new List<string>();

            for (int i = 1; i < NumActual + 1; i++)
                LinkParsing.Add(habrXmlDocument.GetElementsByTagName("link")[i].InnerText);
            return LinkParsing;
        }
        private List<string> ActualParsingDescriptionNewHabr()
        {
            List<string> DescriptionParsing = new List<string>();

            for (int i = 0; i < NumActual; i++)
            {
                DescriptionParsing.Add(habrXmlDocument.GetElementsByTagName("description")[i].InnerText);
                DescriptionParsing[i] = DescriptionParsing[i].Replace("<br>", " \n ");
                DescriptionParsing[i] = Regex.Replace(DescriptionParsing[i], "<[^>]+>", string.Empty);
            }
            return DescriptionParsing;
        }
    }
}
