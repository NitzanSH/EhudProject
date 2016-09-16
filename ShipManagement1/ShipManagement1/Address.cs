using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipManagement1
{
    class Address
    {
        private String country;
        private String city;
        private String street;
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

        #region Get and Set
        public String Country
        {
            get { return country; }
            set { country = value; }
        }

        public String City
        {
            get { return city; }
            set { city = value; }
        }

        public String Street
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

        #endregion
    }
}
