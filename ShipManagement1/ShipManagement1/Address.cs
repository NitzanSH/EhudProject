using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ShipManagement1
{
    public class Address
    {
        private string country;
        private string city;
        private string street;
        private int houseNumber;
        private int zipCode;
        public Address(string country, string city, string street, int houseNumber, int zipCode)
        {
            this.country = country;
            this.city = city;
            this.street = street;
            this.houseNumber = houseNumber;
            this.zipCode = zipCode;
        }
        public Address(Address o)
        {
            if (o != null)
            {
                this.country = o.country;
                this.city = o.city;
                this.street = o.street;
                this.houseNumber = o.houseNumber;
                this.zipCode = o.zipCode;
            }
        }

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        public int HouseNumber
        {
            get { return houseNumber; }
            set { houseNumber = value; }
        }
        public int ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        public static Address ReadAddress(XmlReader xmlReader)
        {
            string country = "", city = "", street = "";
            int houseNumber = 0, zipCode = 0;
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    string element = xmlReader.Name;
                    if (element.Equals("country"))
                    {
                        xmlReader.Read();
                        country = xmlReader.Value;
                    }
                    else if (element.Equals("city"))
                    {
                        xmlReader.Read();
                        city = xmlReader.Value;
                    }
                    else if (element.Equals("street"))
                    {
                        xmlReader.Read();
                        street = xmlReader.Value;
                    }
                    else if (element.Equals("houseNumber"))
                    {
                        xmlReader.Read();
                        houseNumber = int.Parse(xmlReader.Value);
                    }
                    else if (element.Equals("zipCode"))
                    {
                        xmlReader.Read();
                        zipCode = int.Parse(xmlReader.Value);
                    }
                }
            }
            return (new Address(country, city, street, houseNumber, zipCode));
        }

        public static void WriteAddress(Address addr, string elementName, XmlWriter writer)
        {
            writer.WriteStartElement(elementName);

            writer.WriteElementString("country", addr.country);
            writer.WriteElementString("city", addr.city);
            writer.WriteElementString("street", addr.street);
            writer.WriteElementString("houseNumber", addr.houseNumber.ToString());
            writer.WriteElementString("zipCode", addr.zipCode.ToString());

            writer.WriteEndElement();

        }

    }
}
