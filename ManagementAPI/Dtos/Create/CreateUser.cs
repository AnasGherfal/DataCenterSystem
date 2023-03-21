namespace LTTDataCenter.DTOs.Requests.Create
{
    public class CreateUser
    {
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int EmpId { get; set; }
    }
}
