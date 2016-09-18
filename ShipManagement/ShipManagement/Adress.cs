using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ShipManagement
{
    public class Address
    {
        private string country;
        private string city;
        private string street;
        private int houseNumber;
        private int zipCode;

        public int ZipCode
        {
            get
            {
                return zipCode;
            }

            set
            {
                zipCode = value;
            }
        }

        public int HouseNumber
        {
            get
            {
                return houseNumber;
            }

            set
            {
                houseNumber = value;
            }
        }

        public string Street
        {
            get
            {
                return street;
            }

            set
            {
                street = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }

        public string Country
        {
            get
            {
                return country;
            }

            set
            {
                country = value;
            }
        }

        public Address(string country, string city, string street, int houseNumber, int zipCode )
        {
            this.Country = country;
            this.City = city;
            this.Street = street;
            this.HouseNumber = houseNumber;
            this.ZipCode = zipCode;
        }

        public Address()
        {
            
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

        public override String ToString()
        {
            return this.Street + this.HouseNumber + ", " + this.City + ", " + this.ZipCode;
        }

        public static Address ReadAddress(XmlReader xmlReader)
        {
            string country = "";
            string city = "";
            string street = "";
            int houseNumber = 0;
            int zipCode = 0;

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    String element = xmlReader.Name;
                    xmlReader.Read();
                    if (element.Equals("country"))
                    {
                        country = xmlReader.Value;
                    }
                    else if (element.Equals("city"))
                    {
                        city = xmlReader.Value;
                    }
                    else if (element.Equals("street"))
                    {
                        street = xmlReader.Value;
                    }
                    else if (element.Equals("houseNumber"))
                    {
                        houseNumber = int.Parse(xmlReader.Value);
                    }
                    else if (element.Equals("zipCode"))
                    {
                        zipCode = int.Parse(xmlReader.Value);
                    }
                }
            }
            return new Address(country, city, street, houseNumber, zipCode);

        }

        public static void WriteShipment(Shipment ship, XmlWriter writer,string w)
        {
            if (w == "from")
            {
                try

                {
                    writer.WriteElementString("country", ship.From.country);
                    writer.WriteElementString("city", ship.From.city);
                    writer.WriteElementString("street", ship.From.street);
                    writer.WriteElementString("houseNumber", ship.From.houseNumber.ToString());
                    writer.WriteElementString("houseNumber", ship.From.zipCode.ToString());
                }
                catch { }
            }
            else
            {
                try
                {
                    writer.WriteElementString("country", ship.To.country);
                    writer.WriteElementString("city", ship.To.city);
                    writer.WriteElementString("street", ship.To.street);
                    writer.WriteElementString("houseNumber", ship.To.houseNumber.ToString());
                    writer.WriteElementString("houseNumber", ship.To.zipCode.ToString());
                }
                catch { }
            }
        }
    }
}
