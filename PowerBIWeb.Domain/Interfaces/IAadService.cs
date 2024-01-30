namespace PowerBIWeb.Domain.Interfaces
{
    public interface IAadService
    {
        Task<string> GetAccessToken();
    }
}
