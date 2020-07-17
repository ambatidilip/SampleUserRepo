namespace SampleUserRepo.Models.Services
{
    public class ErrorMessage
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public ErrorMessage(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}