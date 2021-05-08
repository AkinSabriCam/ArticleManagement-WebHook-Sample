using System;

namespace ArticleApi.Queries.Integration
{
    public class IntegrationDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public string EndPoint { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}