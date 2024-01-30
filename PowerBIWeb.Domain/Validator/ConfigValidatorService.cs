using FluentValidation;
using PowerBIWeb.Models;

namespace PowerBIWeb.Domain.Validator
{
    public class ConfigValidatorService : AbstractValidator<(AzureAd, PowerBI)>
    {
        public ConfigValidatorService()
        {
            RuleFor(config => config.Item1.AuthenticationMode)
                .NotEmpty().WithMessage("Authentication mode is not set in appsettings.json file");

            RuleFor(config => config.Item1.AuthorityUrl)
                .NotEmpty().WithMessage("Authority is not set in appsettings.json file");

            RuleFor(config => config.Item1.ClientId)
                .NotEmpty().WithMessage("Client Id is not set in appsettings.json file");

            RuleFor(config => config.Item1.AuthenticationMode)
                .Must((config, authMode) => authMode.Equals("serviceprincipal", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrWhiteSpace(config.Item1.TenantId))
                .WithMessage("Tenant Id is not set in appsettings.json file")
                .When(config => config.Item1.AuthenticationMode.Equals("serviceprincipal", StringComparison.InvariantCultureIgnoreCase));

            RuleFor(config => config.Item1.ScopeBase)
                .NotNull().WithMessage("Scope base is not set in appsettings.json file")
                .Must(scopeBase => scopeBase.Length > 0).WithMessage("Scope base is not set in appsettings.json file");

            //RuleFor(config => config.Item2.WorkspaceId)
            //    .NotEmpty().WithMessage("Workspace Id is not set in appsettings.json file")
            //    .Must(IsValidGuid).WithMessage("Please enter a valid guid for Workspace Id in appsettings.json file");

            //RuleFor(config => config.Item2.ReportId)
            //    .NotEmpty().WithMessage("Report Id is not set in appsettings.json file")
            //    .Must(IsValidGuid).WithMessage("Please enter a valid guid for Report Id in appsettings.json file");

            RuleFor(config => config.Item1.AuthenticationMode)
                .Must((config, authMode) => authMode.Equals("masteruser", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrWhiteSpace(config.Item1.PbiUsername))
                .WithMessage("Master user email is not set in appsettings.json file")
                .When(config => config.Item1.AuthenticationMode.Equals("masteruser", StringComparison.InvariantCultureIgnoreCase));

            RuleFor(config => config.Item1.AuthenticationMode)
                .Must((config, authMode) => authMode.Equals("masteruser", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrWhiteSpace(config.Item1.PbiPassword))
                .WithMessage("Master user password is not set in appsettings.json file")
                .When(config => config.Item1.AuthenticationMode.Equals("masteruser", StringComparison.InvariantCultureIgnoreCase));

            RuleFor(config => config.Item1.AuthenticationMode)
                .Must((config, authMode) => authMode.Equals("serviceprincipal", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrWhiteSpace(config.Item1.ClientSecret))
                .WithMessage("Client secret is not set in appsettings.json file")
                .When(config => config.Item1.AuthenticationMode.Equals("serviceprincipal", StringComparison.InvariantCultureIgnoreCase));
        }

        private static bool IsValidGuid(string configParam)
        {
            Guid result;
            return Guid.TryParse(configParam, out result);
        }
    }
}
