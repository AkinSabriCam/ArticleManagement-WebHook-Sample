namespace Domain.Integration.Dtos
{
    public class CreateIntegrationDto
    {
        public string Code { get; set; }
        public string Url { get; set; }
        public string EndPoint { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}