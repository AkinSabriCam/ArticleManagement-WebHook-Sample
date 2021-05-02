using Common.Entity;

namespace Domain.Integration
{
    public class Integration : AggregateRoot
    {
        public string Code { get; set; }
        public string Url { get; set; }
        public string EndPoint { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}