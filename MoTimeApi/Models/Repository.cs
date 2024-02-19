using Microsoft.EntityFrameworkCore;
using MoTimeApi.Models;
using MoTimeApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MoTimeApi.Models
{
    public class Repository : IRepository
    {
        private readonly MoTimeDatabaseContext _appDbContext;
        public Repository(MoTimeDatabaseContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add<T>(T entity) where T : class
        {
            _appDbContext.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _appDbContext.Remove(entity);
        }

        //// events
        public async Task<Event[]> GetAllEventReportAsync()
        {
            IQueryable<Event> query = _appDbContext.Events;
            return await query.ToArrayAsync();
        }
        public async Task<Event> GetEventReportAsync(int Id)
        {
            IQueryable<Event> query = _appDbContext.Events.Where(c => c.Id == Id);
            return await query.FirstOrDefaultAsync();
        }

        //// max hours
        public async Task<MaximumHour[]> GetAllMaximumHourAsync()
        {
            IQueryable<MaximumHour> query = _appDbContext.MaximumHours;
            return await query.ToArrayAsync();
        }
        public async Task<MaximumHour> GetMaximumHourAsync(int Id)
        {
            IQueryable<MaximumHour> query = _appDbContext.MaximumHours.Where(c => c.MaxHoursId == Id);
            return await query.FirstOrDefaultAsync();
        }

        // resource type
        public async Task<ResourceType[]> GetAllResourceTypeAsync()
        {
            IQueryable<ResourceType> query = _appDbContext.ResourceTypes;
            return await query.ToArrayAsync();
        }
        public async Task<ResourceType> GetResourceTypeAsync(int Id)
        {
            IQueryable<ResourceType> query = _appDbContext.ResourceTypes.Where(c
           => c.ResourceTypeId == Id);
            return await query.FirstOrDefaultAsync();
        }

        // alert type
        public async Task<AlertType[]> GetAllAlertTypeAsync()
        {
            IQueryable<AlertType> query = _appDbContext.AlertTypes;
            return await query.ToArrayAsync();
        }
        public async Task<AlertType> GetAlertTypeAsync(int Id)
        {
            IQueryable<AlertType> query = _appDbContext.AlertTypes.Where(c
           => c.AlertTypeId == Id);
            return await query.FirstOrDefaultAsync();
        }

        // employee type
        public async Task<EmployeeType[]> GetAllEmployeeTypeAsync()
        {
            IQueryable<EmployeeType> query = _appDbContext.EmployeeTypes;
            return await query.ToArrayAsync();
        }
        public async Task<EmployeeType> GetEmployeeTypeAsync(int etypeId)
        {
            IQueryable<EmployeeType> query = _appDbContext.EmployeeTypes.Where(c
           => c.EmployeeTypeId == etypeId);
            return await query.FirstOrDefaultAsync();
        }

        //// calendar 
        public async Task<Calendar[]> GetAllCalendarAsync()
        {
            IQueryable<Calendar> query = _appDbContext.Calendars;
            return await query.ToArrayAsync();
        }
        public async Task<Calendar> GetCalendarAsync(int Id)
        {
            IQueryable<Calendar> query = _appDbContext.Calendars.Where(c => c.CalendarId == Id);
            return await query.FirstOrDefaultAsync();
        }

        // employee
        public async Task<Employee[]> GetAllEmployeeAsync()
        {
            IQueryable<Employee> query = _appDbContext.Employees.Include(p => p.User)
                                                                .Include(p => p.Division)
                                                                .Include(p => p.Region)
                                                                .Include(p => p.Resource)
                                                                .Include(p => p.EmployeeType)
                                                                .Include(p => p.EmployeeStatus);

            return await query.ToArrayAsync();
        }
        public async Task<Employee> GetEmployeeAsync(int Id)
        {
            IQueryable<Employee> query = _appDbContext.Employees.Where(c => c.EmployeeId == Id);

            return await query.FirstOrDefaultAsync();
        }

        // for chat bot
        public async Task<ProjectAllocation[]> GetProjectAllocationsForEmployeeAsync(int employeeId)
        {
            IQueryable<ProjectAllocation> query = _appDbContext.ProjectAllocations
                .Include(p => p.Project)
                .Include(p => p.Employee.User)
                .Include(p => p.ClaimItem)
                .Where(p => p.EmployeeId == employeeId); // Filter by employeeId

            return await query.ToArrayAsync();
        }


        // project allocation
        public async Task<ProjectAllocation[]> GetAllProjectAllocationAsync()
        {
            IQueryable<ProjectAllocation> query = _appDbContext.ProjectAllocations.Include(p => p.Project)
                                                                                  .Include(p => p.Employee.User)
                                                                                  .Include(p => p.ClaimItem);
            return await query.ToArrayAsync();
        }
        public async Task<ProjectAllocation> GetProjectAllocationAsync(int Id)
        {
            IQueryable<ProjectAllocation> query = _appDbContext.ProjectAllocations.Where(c => c.ProjectAllocationId == Id);


            return await query.FirstOrDefaultAsync();
        }

        // project
        public async Task<Project> GetProjectAsync(int Id)
        {
            IQueryable<Project> query = _appDbContext.Projects.Where(c => c.ProjectId == Id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Project[]> GetAllProjectAsync()
        {
            IQueryable<Project> query = _appDbContext.Projects.Include(p => p.Admin)
                                                              .Include(p => p.Client)
                                                              .Include(p => p.ProjectStatus);
            return await query.ToArrayAsync();
        }

        // claim item
        public async Task<ClaimItem> GetClaimItemAsync(int Id)
        {
            IQueryable<ClaimItem> query = _appDbContext.ClaimItems.Where(c => c.ClaimItemId == Id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<ClaimItem[]> GetAllClaimItemAsync()
        {
            IQueryable<ClaimItem> query = _appDbContext.ClaimItems.Include(p => p.ClaimType);


            return await query.ToArrayAsync();
        }

        // resource 
        public async Task<Resource[]> GetAllResourceAsync()
        {
            IQueryable<Resource> query = _appDbContext.Resources.Include(p => p.ResourceType);
            return await query.ToArrayAsync();
        }
        public async Task<Resource> GetResourceAsync(int Id)
        {
            IQueryable<Resource> query = _appDbContext.Resources.Where(c => c.ResourceId == Id);
            return await query.FirstOrDefaultAsync();
        }

        // employee status 
        public async Task<EmployeeStatus[]> GetAllEmployeeStatusAsync()
        {
            IQueryable<EmployeeStatus> query = _appDbContext.EmployeeStatuses;
            return await query.ToArrayAsync();
        }
        public async Task<EmployeeStatus> GetEmployeeStatusAsync(int Id)
        {
            IQueryable<EmployeeStatus> query = _appDbContext.EmployeeStatuses.Where(c => c.EmployeeStatusId == Id);
            return await query.FirstOrDefaultAsync();
        }

        // user
        public async Task<User[]> GetAllUserAsync()
        {
            IQueryable<User> query = _appDbContext.Users;
            return await query.ToArrayAsync();
        }
        public async Task<User> GetUserAsync(int Id)
        {
            IQueryable<User> query = _appDbContext.Users.Where(c => c.UserId == Id);
            return await query.FirstOrDefaultAsync();
        }

        // division
        public async Task<Division[]> GetAllDivisionAsync()
        {
            IQueryable<Division> query = _appDbContext.Divisions;
            return await query.ToArrayAsync();
        }

        public async Task<Division> GetDivisionAsync(int Id)
        {
            IQueryable<Division> query = _appDbContext.Divisions.Where(c => c.DivisionId == Id);
            return await query.FirstOrDefaultAsync();
        }

        // region
        public async Task<BranchRegion[]> GetAllRegionAsync()
        {
            IQueryable<BranchRegion> query = _appDbContext.BranchRegions;
            return await query.ToArrayAsync();
        }

        public async Task<BranchRegion> GetRegionAsync(int Id)
        {
            IQueryable<BranchRegion> query = _appDbContext.BranchRegions.Where(c => c.RegionId == Id);
            return await query.FirstOrDefaultAsync();
        }

        // manager
        public async Task<ManagerType> GetManagerTypeAsync(int Id)
        {
            IQueryable<ManagerType> query = _appDbContext.ManagerTypes.Where(c => c.ManagerTypeId == Id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<ManagerType[]> GetAllManagerTypeAsync()
        {
            IQueryable<ManagerType> query = _appDbContext.ManagerTypes;
            return await query.ToArrayAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        // titles
        public async Task<Title> GetTitleAsync(int Id)
        {
            IQueryable<Title> query = _appDbContext.Titles.Where(c => c.TitleId == Id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Title[]> GetAllTitleAsync()
        {
            IQueryable<Title> query = _appDbContext.Titles;
            return await query.ToArrayAsync();
        }

        // client
        public async Task<Client> GetClientAsync(int Id)
        {
            IQueryable<Client> query = _appDbContext.Clients.Where(c => c.ClientId == Id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Client[]> GetAllClientAsync()
        {
            IQueryable<Client> query = _appDbContext.Clients;
            return await query.ToArrayAsync();
        }

        // project status
        public async Task<ProjectStatus> GetProjectStatusAsync(int Id)
        {
            IQueryable<ProjectStatus> query = _appDbContext.ProjectStatuses.Where(c => c.ProjectStatusId == Id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<ProjectStatus[]> GetAllProjectStatusAsync()
        {
            IQueryable<ProjectStatus> query = _appDbContext.ProjectStatuses;
            return await query.ToArrayAsync();
        }

        // questions
        public async Task<Question> GetQuestionAsync(int Id)
        {
            IQueryable<Question> query = _appDbContext.Questions.Where(c => c.QuestionId == Id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Question[]> GetAllQuestionsAsync()
        {
            IQueryable<Question> query = _appDbContext.Questions;
            return await query.ToArrayAsync();
        }

    }
}


