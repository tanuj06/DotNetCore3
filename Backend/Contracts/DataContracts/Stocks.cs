namespace Backend.Contracts.DataContracts
{
    public class Stocks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int StoreID { get; set; } = 0;
    }
}
