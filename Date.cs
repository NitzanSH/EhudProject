using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Shipments
{
    public class Date
    {
        private int day;
        private int month;
        private int year;
        public Date(int day, int month, int year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }
        public Date(Date d)
        {
            if (d == null)
            {
                this.day = 1;
                this.month = 1;
                this.year = 1111;
            }
            else
            {
                this.day = d.day;
                this.month = d.month;
                this.year = d.year;
            }
        }

        public int Day
        {
            get { return day; }
            set { day = value; }
        }
        public int Month
        {
            get { return month; }
            set { month = value; }
        }
        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public static void WriteDate(Date d, string elementName, XmlWriter writer)
        {
            writer.WriteStartElement(elementName);

            writer.WriteElementString("day", d.day.ToString());
            writer.WriteElementString("month", d.month.ToString());
            writer.WriteElementString("year", d.year.ToString());

            writer.WriteEndElement();

        }


        public static Date ReadDate(XmlReader xmlReader)
        {
            int day = 0, month = 0, year = 0;
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    string element = xmlReader.Name;
                    if (element.Equals("day"))
                    {
                        xmlReader.Read();
                        day = int.Parse(xmlReader.Value);
                    }
                    else if (element.Equals("month"))
                    {
                        xmlReader.Read();
                        month = int.Parse(xmlReader.Value);
                    }
                    else if (element.Equals("year"))
                    {
                        xmlReader.Read();
                        year = int.Parse(xmlReader.Value);
                    }
                }
            }
            return (new Date(day, month, year));
        }
    }

}
