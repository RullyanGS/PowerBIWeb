using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PowerBIWeb.Interfaces;
using PowerBIWeb.Models;
using System.Text.Json;

namespace PowerBIWeb.UI.Web.Controllers
{
    public class EmbedInfoController : Controller
    {
        private readonly IValidator<(AzureAd, PowerBI)> validator;
        private readonly IPbiEmbedService pbiEmbedService;
        private readonly IOptions<AzureAd> azureAd;
        private readonly IOptions<PowerBI> powerBI;

        public EmbedInfoController(IValidator<(AzureAd, PowerBI)> validator,
                                   IPbiEmbedService pbiEmbedService,
                                   IOptions<AzureAd> azureAd,
                                   IOptions<PowerBI> powerBI)
        {
            this.validator = validator;
            this.pbiEmbedService = pbiEmbedService;
            this.azureAd = azureAd;
            this.powerBI = powerBI;
        }

        /// <summary>
        /// Returns Embed token, Embed URL, and Embed token expiry to the client
        /// </summary>
        /// <returns>JSON containing parameters for embedding</returns>
        [HttpGet]
        public string GetEmbedInfo(string WorkspaceId, string ReportId)
        {
            try
            {
                // Validate whether all the required configurations are provided in appsettings.json
                var validationResult = validator.Validate((azureAd.Value, powerBI.Value));
                if (!validationResult.IsValid)
                {
                    HttpContext.Response.StatusCode = 400;
                    var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
                    return errors;
                }

                //var firstWorkspace = powerBI.Value.Workspaces.FirstOrDefault();
                //var firstReport = firstWorkspace.Reports.FirstOrDefault();
                //EmbedParams embedParams = pbiEmbedService.GetEmbedParams(new Guid(firstWorkspace.WorkspaceId), new Guid(firstReport.ReportId));

                EmbedParams embedParams = pbiEmbedService.GetEmbedParams(new Guid(WorkspaceId), new Guid(ReportId));

                return JsonSerializer.Serialize<EmbedParams>(embedParams);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return ex.Message + "\n\n" + ex.StackTrace;
            }
        }
    }
}
