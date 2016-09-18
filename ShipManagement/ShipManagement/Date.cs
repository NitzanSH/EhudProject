using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ShipManagement
{
    public class Date
    {
        private int day;
        private int month;
        private int year;

        public int Day
        {
            get
            {
                return day;
            }

            set
            {
                day = value;
            }
        }

        public int Month
        {
            get
            {
                return month;
            }

            set
            {
                month = value;
            }
        }

        public int Year
        {
            get
            {
                return year;
            }

            set
            {
                year = value;
            }
        }

        public Date(int day, int month, int year)
        {
            this.Day = day;
            this.Month = month;
            this.Year = year;
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
        public Date()
        {

        }

        public override String ToString()
        {
            return this.Day + "." + this.Month + "." + this.Year;
        }

        public static Date ReadDate(XmlReader xmlReader)
        {
            int day = 0;
            int month = 0;
            int year = 0;

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    String element = xmlReader.Name;
                    xmlReader.Read();

                    if (element.Equals("day"))
                    {
                        day = int.Parse(xmlReader.Value);
                    }
                    else if (element.Equals("month"))
                    {
                        month = int.Parse(xmlReader.Value);
                    }
                    else if (element.Equals("year"))
                    {
                        year = int.Parse(xmlReader.Value);
                    }
                }
            }
            return new Date(day, month, year);
        }

        public static void WriteShipment(Shipment ship, XmlWriter writer,string w)
        {
            if (w == "recive")
            {
                try
                {
                    writer.WriteElementString("day", ship.RecieveDate.day.ToString());
                    writer.WriteElementString("month", ship.RecieveDate.month.ToString());
                    writer.WriteElementString("year", ship.RecieveDate.year.ToString());
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    writer.WriteElementString("day", ship.DeliverDate.day.ToString());
                    writer.WriteElementString("month", ship.DeliverDate.month.ToString());
                    writer.WriteElementString("year", ship.DeliverDate.year.ToString());
                }
                catch
                {

                }
            }
        }
    }
}
