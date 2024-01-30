namespace PowerBIWeb.Models
{
    public class PowerBI
    {
        public List<PowerBIWorkspace> Workspaces { get; set; }
    }

    public class PowerBIWorkspace
    {
        public string WorkspaceName { get; set; }
        public string WorkspaceId { get; set; }
        public List<PowerBIReport> Reports { get; set; }
    }

    public class PowerBIReport
    {
        public string ReportName { get; set; }
        public string ReportId { get; set; }
    }


}
