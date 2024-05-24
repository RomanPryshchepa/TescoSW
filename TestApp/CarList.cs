using System.Xml.Serialization;

namespace TestApp
{
    [XmlRoot("catalog")]
    public class CarList
    {
        public CarList() { Cars = new List<Car>(); }
        [XmlElement("car")]
        public List<Car> Cars { get; set; }
    }

    [XmlRoot("car")]
    public class Car
    {
        [XmlElement("model_name")]
        public String ModelName { get; set; }

        [XmlElement("sale_date")]
        public DateTime SaleDate { get; set; }

        [XmlElement("price")]
        public Double Price { get; set; }

        [XmlElement("dph")]
        public Double Dph { get; set; }
    }
}
