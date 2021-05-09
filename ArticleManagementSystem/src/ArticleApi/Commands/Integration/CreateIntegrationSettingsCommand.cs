using ArticleApi.Queries.Integration;
using FluentValidation;
using MediatR;

namespace ArticleApi.Commands.Integration
{
    public class CreateIntegrationSettingsCommand : IRequest<IntegrationSettingsDto>
    {
        public string Code { get; set; }
        public string Url { get; set; }
        public string EndPoint { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class CreateIntegrationSettingsCommandValidator : AbstractValidator<CreateIntegrationSettingsCommand>
    {
        public CreateIntegrationSettingsCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull()
                .WithMessage("Integration code can not be null or empty! ").WithErrorCode("AMS201");

            RuleFor(x => x.Url).NotEmpty().NotNull()
                .WithMessage("Integration url can not be null or empty! ").WithErrorCode("AMS202");
            
            RuleFor(x => x.EndPoint).NotEmpty().NotNull()
                .WithMessage("Integration endpoint can not be null or empty! ").WithErrorCode("AMS203");
           
            RuleFor(x => x.UserName).NotEmpty().NotNull()
                .WithMessage("Integration username not be null or empty! ").WithErrorCode("AMS204");

            RuleFor(x => x.Password).NotEmpty().NotNull()
                .WithMessage("Integration password can not be null or empty! ").WithErrorCode("AMS205");
        }
    }
}