using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace ShipManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Shipment> shipments = new ObservableCollection<Shipment>();
        public static string path = "d:/Users/user/Desktop/ShipManagement/Shipments.xml";
        public static string mode = "";
        public static Shipment modifiedShip;
        public MainWindow()
        {
            InitializeComponent();
            loadxml();
            ShipsLB.ItemsSource = shipments;
            AbleControls(true);
        }
        private void AbleControls(bool readOnlyInd)
        {
            ShipGB.IsEnabled = !readOnlyInd;
            ShipsLB.IsEnabled = readOnlyInd;
            textBox.IsEnabled = !readOnlyInd;
            OkBTN.IsEnabled = !readOnlyInd;
            CancelBTN.IsEnabled = !readOnlyInd;
            NewBTN.IsEnabled = readOnlyInd;
            EditBTN.IsEnabled = (ShipsLB.SelectedIndex >= 0 && readOnlyInd);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (XmlWriter writer = XmlWriter.Create(path))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Shipments");

                foreach (Shipment ship in shipments)
                {
                    Shipment.WriteShipment(ship, writer);
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }

        }
        static void loadxml()
        {
            int i = 0;
            XmlReader xmlReader = XmlReader.Create(path);
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) &&
                    (xmlReader.Name == "Shipment"))
                {
                    XmlReader subTree = xmlReader.ReadSubtree();

                    shipments.Add(Shipment.HandleShipment(subTree));
                }
                i++;
            }
        }
        private void ShipsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShipsLB.SelectedIndex < 0)
                return;
            ShipGB.DataContext = shipments[ShipsLB.SelectedIndex];
            textBox.DataContext = shipments[ShipsLB.SelectedIndex];
            AbleControls(true);
        }
        private Shipment CloneShipment(Shipment s)
        {
            return new Shipment(s);
        }
        private void EditBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ShipsLB.SelectedIndex < 0)
                return;
            AbleControls(false);
            modifiedShip = CloneShipment(shipments[ShipsLB.SelectedIndex]);
            ShipGB.DataContext = modifiedShip;
            textBox.DataContext = modifiedShip;
            mode = "Update";

        }
        private void NewBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ShipsLB.SelectedIndex < 0)
                modifiedShip = CloneShipment(null);
            else
                modifiedShip = CloneShipment(shipments[ShipsLB.SelectedIndex]);
            ShipGB.DataContext = modifiedShip;
            textBox.DataContext = modifiedShip;
            AbleControls(false);
            mode = "Add";
        }
        private void CancelBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ShipsLB.SelectedIndex >= 0)
            {
                ShipGB.DataContext = shipments[ShipsLB.SelectedIndex];
                textBox.DataContext = modifiedShip;
            }
            else
            {
                ShipGB.DataContext = null;
                textBox.DataContext = null;
            }
            AbleControls(true);
        }
        private void OkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckInput(mode))
            {
                MessageBox.Show("check youre InPut, maybe you need to fill something");
                return;
            }

            if (mode == "Update")
            { shipments[ShipsLB.SelectedIndex] = modifiedShip; }
            else if (mode == "Add")
            { shipments.Add(modifiedShip); }
            else
                throw new Exception("Unexpected mode");

            AbleControls(true);
        }
        public bool CheckInput(string mode)
        {
            if (FCountry.Text == "" || FCity.Text == "" || FStreet.Text == "" || FHouse.Text == "" || TCountry.Text == "" || TCity.Text == "" || TStreet.Text == "" || THouse.Text == "" || RDay.Text == "" || RMonth.Text == "" || RYear.Text == "" || DDay.Text == "" || DMonth.Text == "" || DYear.Text == "" || sum.Text == "" || curency.Text == "" || textBox.Text == "" || TZip.Text=="")
            {
                return false;
            }
            string add = DDay.ToString()+".";
            int i = 0;
            while (add[i] != '.')
            {
                if ((add[i] < 48))
                    {
                    return false;
                }
                else if((add[i] <= 57))
                {
                    return false;
                }
                i++;
            }
            i = 0;
            add = RDay.ToString() + ".";
            while (add[i] != '.')
            {
                if ((add[i] < 48))
                {
                    return false;
                }
                else if ((add[i] <= 57))
                {
                    return false;
                }
                i++;
            }
            i = 0;
            add = DMonth.ToString() + ".";
            while (add[i] != '.')
            {
                if ((add[i] < 48))
                {
                    return false;
                }
                else if ((add[i] <= 57))
                {
                    return false;
                }
                i++;
            }
            i = 0;
            add = RMonth.ToString() + ".";
            while (add[i] != '.')
            {
                if ((add[i] < 48))
                {
                    return false;
                }
                else if ((add[i] <= 57))
                {
                    return false;
                }
                i++;
            }
            i = 0;
            add = RYear.ToString() + ".";
            while (add[i] != '.')
            {
                if ((add[i] < 48))
                {
                    return false;
                }
                else if ((add[i] <= 57))
                {
                    return false;
                }
                i++;
            }
            i = 0;
            add = DYear.ToString() + ".";
            while (add[i] != '.')
            {
                if ((add[i] < 48))
                {
                    return false;
                }
                else if ((add[i] <= 57))
                {
                    return false;
                }
                i++;
            }
            i = 0;
            add = FZip.ToString() + ".";
            while (add[i] != '.')
            {
                if ((add[i] < 48))
                {
                    return false;
                }
                else if ((add[i] <= 57))
                {
                    return false;
                }
                i++;
            }
            i = 0;
            add = TZip.ToString() + ".";
            while (add[i] != '.')
            {
                if ((add[i] < 48))
                {
                    return false;
                }
                else if ((add[i] <= 57))
                {
                    return false;
                }
                i++;
            }
            string check = "";
            







            return true;
        }
    }
  }
