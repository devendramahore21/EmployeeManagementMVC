namespace EmployeeManagementMVC.RequestModels
{
    public class EmployeeQueryParams
    {
        public string? Search { get; set; }

        public string? SortOrder { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 5;
    }
}
