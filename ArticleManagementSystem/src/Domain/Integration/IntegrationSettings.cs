using Common.Entity;
using FluentValidation;

namespace Domain.Integration
{
    public class IntegrationSetting : AggregateRoot
    {
        public string Code { get; private set; }
        public string Url { get; private set; }
        public string EndPoint { get; private set; }
        public string TokenUrl { get; set; }
        public string TokenEndPoint { get; set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ValidationException("Code can not be empty!");

            Code = code;
        }

        public void SetUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ValidationException("Url can not be empty!");

            Url = url;
        }

        public void SetEndPoint(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ValidationException("Endpoint can not be empty!");

            EndPoint = endpoint;
        }

        public void SetUserName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ValidationException("Integration username can not be empty!");

            UserName = username;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ValidationException("Integration Password can not be empty!");

            Password = password;
        }

        public void SetTokenUrl(string tokenUrl)
        {
            if (string.IsNullOrWhiteSpace(tokenUrl))
                throw new ValidationException("TokenUrl can not be empty!");

            TokenUrl = tokenUrl;
        }

        public void SetTokenEndPoint(string tokenEndPoint)
        {
            if (string.IsNullOrWhiteSpace(tokenEndPoint))
                throw new ValidationException("Token Endpoint can not be empty!");

            TokenEndPoint = tokenEndPoint;
        }

        public void SetClientId(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ValidationException("Client id can not be empty!");

            ClientId = clientId;
        }

        public void SetClientSecret(string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ValidationException("Client secret can not be empty!");

            ClientSecret = clientSecret;
        }
    }
}