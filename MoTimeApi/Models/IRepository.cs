namespace MoTimeApi.Models
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //max
        Task<Event[]> GetAllEventReportAsync();
        Task<Event> GetEventReportAsync(int Id);
        //alert

        //max
        Task<MaximumHour[]> GetAllMaximumHourAsync();
        Task<MaximumHour> GetMaximumHourAsync(int Id);
        //alert
        Task<AlertType[]> GetAllAlertTypeAsync();
        Task<AlertType> GetAlertTypeAsync(int Id);


        //client
        Task<Client[]> GetAllClientAsync();
        Task<Client> GetClientAsync(int Id);


        //project
        Task<Project[]> GetAllProjectAsync();
        Task<Project> GetProjectAsync(int Id);


        //titles
        Task<Title[]> GetAllTitleAsync();
        Task<Title> GetTitleAsync(int Id);


        //employee type
        Task<EmployeeType[]> GetAllEmployeeTypeAsync();
        Task<EmployeeType> GetEmployeeTypeAsync(int etypeId);


        ////calendar item
        //Task<CalendarItem[]> GetAllCalendarItemAsync();
        //Task<CalendarItem> GetCalendarItemAsync(int Id);


        //////calendar 
        Task<Calendar[]> GetAllCalendarAsync();
        Task<Calendar> GetCalendarAsync(int Id);


        ////employee
        Task<Employee[]> GetAllEmployeeAsync();
        Task<Employee> GetEmployeeAsync(int Id);


        //employee status
        Task<EmployeeStatus[]> GetAllEmployeeStatusAsync();
        Task<EmployeeStatus> GetEmployeeStatusAsync(int Id);


        ////user
        Task<User[]> GetAllUserAsync();
        Task<User> GetUserAsync(int Id);


        ////manager type
        Task<ManagerType[]> GetAllManagerTypeAsync();
        Task<ManagerType> GetManagerTypeAsync(int Id);


        // resource
        Task<Resource[]> GetAllResourceAsync();
        Task<Resource> GetResourceAsync(int Id);


        // resource type
        Task<ResourceType[]> GetAllResourceTypeAsync();
        Task<ResourceType> GetResourceTypeAsync(int Id);


        //// division
        Task<Division[]> GetAllDivisionAsync();
        Task<Division> GetDivisionAsync(int Id);


        //// region
        Task<BranchRegion[]> GetAllRegionAsync();
        Task<BranchRegion> GetRegionAsync(int Id);


        //// project status
        Task<ProjectStatus[]> GetAllProjectStatusAsync();
        Task<ProjectStatus> GetProjectStatusAsync(int Id);


        //// claim item
        Task<ClaimItem[]> GetAllClaimItemAsync();
        Task<ClaimItem> GetClaimItemAsync(int Id);


        //// project allocation
        Task<ProjectAllocation[]> GetAllProjectAllocationAsync();
        Task<ProjectAllocation> GetProjectAllocationAsync(int Id);

        // chat bot 
        Task<ProjectAllocation[]> GetProjectAllocationsForEmployeeAsync(int Id);

        //// question
        Task<Question[]> GetAllQuestionsAsync();
        Task<Question> GetQuestionAsync(int Id);
    }
}









