using System;
using System.Xml;

namespace TemplateMethod
{
    public abstract class DataAccessHandler
    {
        public void Execute()
        {
            Connect();
            Download();
            Process();
            Disconnect();
        }

        public abstract void Connect();
        public abstract void Download();
        public abstract void Process();
        public abstract void Disconnect();
    }

    public class DataRow
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Database
    {
        public Database()
        {
            dataRows = new List<DataRow>();

            DataRow d1 = new();
            d1.Name = "a";
            d1.Age = 1;

            DataRow d2 = new();
            d2.Name = "b";
            d2.Age = 2;

            DataRow d3 = new();
            d3.Name = "c";
            d3.Age = 3;

            dataRows.Add(d1);
            dataRows.Add(d2);
            dataRows.Add(d3);
        }

        public List<DataRow> GetData()
        {
            return dataRows;
        }
        private List<DataRow> dataRows;
    }

    public class ColumnSummator : DataAccessHandler
    {
        public override void Connect()
        {
            database = new();
        }

        public override void Download()
        {
            data = database.GetData();
        }

        public override void Process()
        {
            result = 0;
            for(int i = 0; i < data.Count; i++)
            {
                result += data[i].Age;
            }
        }

        public override void Disconnect()
        {
            data = null;
            database = null;
        }

        public int result { get; private set; }

        private Database? database;
        private List<DataRow>? data;
    }

    public class XmlFileReader
    {
        public XmlFileReader()
        {
            document = new();
            document.LoadXml("<note>" +
                                "<to>Tove</to>" +
                                "<from>Jani</from>" +
                                "<heading>Reminder</heading>" +
                                "<body>Don't forget me this weekend!</body>" +
                            "</note>");
        }

        public XmlDocument GetData()
        {
            return document;
        }

        private XmlDocument document;
    }


    public class LongestNodeLengthGetter : DataAccessHandler
    {
        public override void Connect()
        {
            reader = new XmlFileReader();
        }

        public override void Download()
        {
            document = reader.GetData();
        }

        public override void Process()
        {
            result = LongestNodeLength(document.FirstChild);
        }
        public override void Disconnect()
        {
            document = null;
            reader = null;
        }

        private int LongestNodeLength(XmlNode node)
        {
            int longestLength = node.Name.Length;

            foreach(XmlNode child in node.ChildNodes)
            {
                longestLength = Math.Max(longestLength, LongestNodeLength(child));
            }

            if(node.NextSibling != null)
            {
                longestLength = Math.Max(longestLength, LongestNodeLength(node.NextSibling));
            }

            return longestLength;
        }

        public int result { get; private set; }

        private XmlFileReader? reader;
        private XmlDocument? document;
    }

}
