using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ShipManagement1
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

        public Shipment(string id, Address from, Address to, Date recieveDate, Date deliverDate, double sum,
            string currency, bool payed)
        {
            this.from = from;
            this.to = to;
            this.recieveDate = recieveDate;
            this.deliverDate = deliverDate;
            this.sum = sum;
            this.currency = currency;
            this.payed = payed;
            this.id = id;
        }

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

        #region Get and Set
        public Address From
        {
            get { return from; }
            set { from = value; }
        }
        public Address To
        {
            get { return to; }
            set { to = value; }
        }
        public Date RecieveDate
        {
            get { return recieveDate; }
            set { recieveDate = value; }
        }
        public Date DeliverDate
        {
            get { return deliverDate; }
            set { deliverDate = value; }
        }
        public double Sum
        {
            get { return sum; }
            set { sum = value; }
        }
        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }
        public bool Payed
        {
            get { return payed; }
            set { payed = value; }
        }
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        #region Read and Write the XML file
        public static void WriteShipment(Shipment ship, XmlWriter writer)
        {
            writer.WriteStartElement("Shipment");

            writer.WriteElementString("id", ship.ID.ToString());
            if (ship.from != null)
                Address.WriteAddress(ship.from, "from", writer);
            if (ship.to != null)
                Address.WriteAddress(ship.to, "to", writer);
            if (ship.recieveDate != null)
                Date.WriteDate(ship.recieveDate, "recieveDate", writer);
            if (ship.deliverDate != null)
                Date.WriteDate(ship.deliverDate, "deliverDate", writer);
            writer.WriteElementString("sum", ship.Sum.ToString());
            writer.WriteElementString("currency", ship.currency);
            writer.WriteElementString("payed", ship.payed.ToString());

            writer.WriteEndElement();

        }

        public static Shipment ReadShipment(XmlReader xmlReader)
        {
            Address from = null, to = null;
            Date recieveDate = null, deliverDate = null;
            double sum = 0;
            bool payed = false;
            string id = null;
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
                    else if (element.Equals("payed"))
                    {
                        xmlReader.Read();
                        payed = bool.Parse(xmlReader.Value);
                    }
                    else if (element.Equals("currency"))
                    {
                        xmlReader.Read();
                        currency = xmlReader.Value;
                    }
                }
            }
            return new Shipment(id, from, to, recieveDate, deliverDate, sum, currency, payed);
        }

        #endregion
    }
}
