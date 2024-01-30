using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using PowerBIWeb.Models;

namespace PowerBIWeb.Interfaces
{
    public interface IPbiEmbedService
    {
        PowerBIClient GetPowerBIClient();
        EmbedParams GetEmbedParams(Guid workspaceId, Guid reportId, Guid additionalDatasetId = default);
        EmbedParams GetEmbedParams(Guid workspaceId, IList<Guid> reportIds, IList<Guid> additionalDatasetIds = null);
        EmbedToken GetEmbedToken(Guid reportId, IList<Guid> datasetIds, Guid targetWorkspaceId = default);
        EmbedToken GetEmbedToken(IList<Guid> reportIds, IList<Guid> datasetIds, Guid targetWorkspaceId = default);
        EmbedToken GetEmbedToken(IList<Guid> reportIds, IList<Guid> datasetIds, IList<Guid> targetWorkspaceIds = null);
        EmbedToken GetEmbedTokenForRDLReport(Guid targetWorkspaceId, Guid reportId, string accessLevel = "view");
    }
}
