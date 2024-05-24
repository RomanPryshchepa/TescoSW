using System.IO;
using System.Windows;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace TestApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            try
            {
                string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                openFileDialog.InitialDirectory = @projectDirectory;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while getting project directory: {0}", ex.Message.ToString());
            }
            
            openFileDialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                Dictionary<String, SoldCar> soldCars = new Dictionary<String, SoldCar>();
                using (StreamReader reader = new StreamReader(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CarList));
                    CarList carList = null;
                    try
                    {
                        carList = (CarList)serializer.Deserialize(reader);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while processing the file: {0}", ex.Message.ToString());
                    }

                    if (carList == null)
                    {
                        MessageBox.Show("Failed to get data from file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    foreach (Car car in carList.Cars)
                    {
                        if (car.SaleDate.DayOfWeek == DayOfWeek.Saturday || car.SaleDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            if (soldCars.ContainsKey(car.ModelName)) 
                            {
                                soldCars[car.ModelName].Price += car.Price;
                                soldCars[car.ModelName].PriceWithDph += (1 + car.Dph / 100) * car.Price;
                            }
                            else
                            {
                                soldCars.Add(car.ModelName, new SoldCar(car.ModelName, car.Price, (1 + car.Dph / 100) * car.Price));
                            }
                        }
                    }
                    dataGrid.ItemsSource = soldCars.Values;
                }
            }
        }
    }
}