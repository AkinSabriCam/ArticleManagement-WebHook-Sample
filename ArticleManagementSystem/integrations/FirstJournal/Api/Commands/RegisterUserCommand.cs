using MediatR;

namespace Api.Commands
{
    public class RegisterUserCommand : IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}