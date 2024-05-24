namespace TestApp
{
    internal class SoldCar
    {
        public String ModelName { get; set; }
        public Double Price { get; set; }
        public Double PriceWithDph { get; set; }

        public SoldCar(string modelName, Double price, Double priceWithDph)
        {
            ModelName = modelName;
            Price = price;
            PriceWithDph = priceWithDph;
        }
    }
}