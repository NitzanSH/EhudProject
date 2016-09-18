using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ShipManagement
{
    public class Shipment
    {
        private Address from;
        private Address to;
        private Date recieveDate;
        private Date deliverDate;
        private double sum;
        private string currency;
        private bool payed;
        private string id;

        public Shipment(Shipment s)
        {
            if (s != null)
            {
                this.from = new Address(s.from);
                this.to = new Address(s.to);
                this.recieveDate = new Date(s.recieveDate);
                this.deliverDate = new Date(s.deliverDate);
                this.sum = s.sum;
                this.currency = s.currency;
                this.payed = s.payed;
                this.id = s.id;
            }
            else
            {
                this.from = new Address(null);
                this.to = new Address(null);
                this.recieveDate = new Date(null);
                this.deliverDate = new Date(null);
            }
        }

        public Address From
        {
            get
            {
                return from;
            }

            set
            {
                from = value;
            }
        }
        public Address To
        {
            get
            {
                return to;
            }

            set
            {
                to = value;
            }
        }
        public Date RecieveDate
        {
            get
            {
                return recieveDate;
            }

            set
            {
                recieveDate = value;
            }
        }
        public Date DeliverDate
        {
            get
            {
                return deliverDate;
            }

            set
            {
                deliverDate = value;
            }
        }
        public double Sum
        {
            get
            {
                return sum;
            }

            set
            {
                sum = value;
            }
        }
        public string Currency
        {
            get
            {
                return currency;
            }

            set
            {
                currency = value;
            }
        }
        public bool Payed
        {
            get
            {
                return payed;
            }

            set
            {
                payed = value;
            }
        }
        public string ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public Shipment(string id, Address from, Address to, Date receiveDate, Date deliverDate, double sum, string currency, Boolean payed)
        {
            this.From = from;
            this.To = to;
            this.RecieveDate = receiveDate;
            this.DeliverDate = deliverDate;
            this.Sum = sum;
            this.Currency = currency;
            this.Payed = payed;
            this.ID = id;
        }
        
        public override String ToString()
        {
            String s = "Shipment (" + this.RecieveDate.ToString() + " - " + this.DeliverDate.ToString() + ")\n";
            s += "From " + this.From.ToString() + "\n";
            s += "To " + this.From.ToString() + "\n" + Environment.NewLine;

            if (this.Payed)
                s += "Payed: ";
            else
                s += "Payment due: ";

            if (this.Currency.Equals("$"))
                s += "$" + this.Sum;
            else
                s += this.Sum + " " + this.Currency;

            return s;
        }
        public static Shipment HandleShipment(XmlReader xmlReader)
        {
            Address from = null, to = null;
            Date recieveDate = null, deliverDate = null;
            string id = "";
            double sum = 0;
            bool payed = false;
            String currency = "";
            XmlReader subTree;

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    String element = xmlReader.Name;
                    if (element.Equals("id"))
                    {
                        xmlReader.Read();
                        id = xmlReader.Value;
                    }
                    else if (element.Equals("from"))
                    {
                        subTree = xmlReader.ReadSubtree();
                        from = Address.ReadAddress(subTree);
                    }
                    else if (element.Equals("to"))
                    {
                        subTree = xmlReader.ReadSubtree();
                        to = Address.ReadAddress(subTree);
                    }
                    else if (element.Equals("recieveDate"))
                    {
                        subTree = xmlReader.ReadSubtree();
                        recieveDate = Date.ReadDate(subTree);
                    }
                    else if (element.Equals("deliverDate"))
                    {
                        subTree = xmlReader.ReadSubtree();
                        deliverDate = Date.ReadDate(subTree);
                    }
                    else if (element.Equals("sum"))
                    {
                        xmlReader.Read();
                        sum = Double.Parse(xmlReader.Value);
                    }
                    else if (element.Equals("currency"))
                    {
                        xmlReader.Read();
                        currency = xmlReader.Value;
                    }
                    else if (element.Equals("payed"))
                    {
                        xmlReader.Read();
                        payed = bool.Parse(xmlReader.Value);
                    }
                    
                    

                }
            }
            return new Shipment(id, from, to, recieveDate, deliverDate, sum, currency, payed);
        }
        public static void WriteShipment(Shipment ship, XmlWriter writer)
        {
            try
            {

                writer.WriteStartElement("Shipment");
                writer.WriteElementString("id", ship.ID);
                writer.WriteStartElement("from");
                Address.WriteShipment(ship, writer,"from");
                writer.WriteEndElement();
                writer.WriteStartElement("to");
                Address.WriteShipment(ship, writer,"to");
                writer.WriteEndElement();
                writer.WriteStartElement("recieveDate");
                Date.WriteShipment(ship, writer,"recive");
                writer.WriteEndElement();
                writer.WriteStartElement("deliverDate");
                Date.WriteShipment(ship, writer, "deliver");
                writer.WriteEndElement();
                writer.WriteElementString("sum", ship.sum.ToString());
                writer.WriteElementString("payed", ship.payed.ToString());
                writer.WriteElementString("currency", ship.currency);
                writer.WriteEndElement();


            }
            catch
            {
                
            }
        }
        
    }
}
