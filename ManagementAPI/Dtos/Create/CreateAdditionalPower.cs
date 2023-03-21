namespace LTTDataCenter.DTOs.Requests.Create
{
    public class CreateAdditionalPower
    {
        public int Id { get; set; }
        public string Power { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
