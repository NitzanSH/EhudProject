using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipManagement1
{
    class Shipment
    {
        private String id;
        private Address from;
        private Address to;
        private Date recieveDate;
        private Date deliverDate;
        private double sum;
        private String currency;
        private bool payed;

        public Shipment(String id, Address from, Address to, Date recieveDate, Date deliverDate, double sum, String currency, bool payed)
        {
            this.id = id;
            this.from = from;
            this.to = to;
            this.recieveDate = recieveDate;
            this.deliverDate = deliverDate;
            this.sum = sum;
            this.currency = currency;
            this.payed = payed;

        }

        #region Get and Set
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

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

        public String Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        public bool Payed
        {
            get { return payed; }
            set { payed = value; }
        }
        #endregion
    }
}
