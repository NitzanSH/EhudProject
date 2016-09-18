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

namespace ShipManagement1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Shipment> shipments;
        Shipment modifiedShip;
        string mode;

        public MainWindow()
        {
            InitializeComponent();
            shipments = new ObservableCollection<Shipment>();
            LoadShipments();
            ShipsLB.ItemsSource = shipments;
            AbleControls(true);
            mode = null;
        }

        #region Button Clicks
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ShipsLB.SelectedIndex < 0)
                return;
            AbleControls(false);
            modifiedShip = CloneShipment(shipments[ShipsLB.SelectedIndex]);
            ShipGB.DataContext = modifiedShip;
            mode = "Update";
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            if (ShipsLB.SelectedIndex < 0)
                modifiedShip = CloneShipment(null);
            else
                modifiedShip = CloneShipment(shipments[ShipsLB.SelectedIndex]);
            ShipGB.DataContext = modifiedShip;
            AbleControls(false);
            mode = "Add";

        }

        private void OkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckInput(mode))
                return;

            if (mode == "Update")
            { shipments[ShipsLB.SelectedIndex] = modifiedShip; }
            else if (mode == "Add")
            { shipments.Add(modifiedShip); }
            else
                throw new Exception("Unexpected mode");

            AbleControls(true);

        }

        private void CancelBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ShipsLB.SelectedIndex >= 0)
                ShipGB.DataContext =
        shipments[ShipsLB.SelectedIndex];
            else
                ShipGB.DataContext = null;
            AbleControls(true);

        }
        #endregion

        private bool CheckInput(string mode)
        {
            return true;
        }

        private Shipment CloneShipment(Shipment s)
        {
            return new Shipment(s);
        }

        private void LoadShipments()
        {
            XmlReader Reader = XmlReader.Create(@"C:\Users\USER\Desktop\Shipments.xml");
            while (Reader.Read())
            {
                if (Reader.NodeType == XmlNodeType.Element)
                {
                    if(Reader.Name.Equals("Shipment"))
                    {
                        Shipment Changes = Shipment.ReadShipment(Reader);
                        shipments.Add(Changes);
                    }
                }    
            }
        }

        private void ShipsLB_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if(ShipsLB.SelectedIndex < 0)
                return;

            ShipGB.DataContext = shipments[ShipsLB.SelectedIndex];
            Id.DataContext = shipments[ShipsLB.SelectedIndex];
            AbleControls(true);
        }

        private void AbleControls(bool readOnlyInd)
        {
            ShipGB.IsEnabled = !readOnlyInd;
            ShipsLB.IsEnabled = readOnlyInd;
            Id.IsEnabled = !readOnlyInd;
            OkBTN.IsEnabled = !readOnlyInd;
            CancelBTN.IsEnabled = !readOnlyInd;
            NewBTN.IsEnabled = readOnlyInd;
            EditBTN.IsEnabled = (ShipsLB.SelectedIndex >= 0 && readOnlyInd);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (XmlWriter writer = XmlWriter.Create(@"C:\Users\USER\Desktop\Shipments1.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Shipments");

                foreach (Shipment ship in shipments)
                {
                    Shipment.WriteShipment(ship, writer);
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

        }
    }
}
