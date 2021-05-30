namespace Domain.Integration.Dtos
{
    public class CreateIntegrationSettingsDto
    {
        public string Code { get; set; }
        public string Url { get; set; }
        public string EndPoint { get; set; }
        public string TokenUrl { get; set; }
        public string TokenEndPoint { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}