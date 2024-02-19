using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoTimeApi.Models;
using MoTimeApi.ViewModels;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoTimeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoTimeController : ControllerBase
    {
        private readonly IRepository _repository;

        public MoTimeController(IRepository repository)
        {
            _repository = repository;
        }

        // questions
        [HttpGet]
        [Route("GetAllQuestions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            try
            {
                var results = await _repository.GetAllQuestionsAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetQuestion/{Id}")]
        public async Task<IActionResult> GetQuestionAsync(int Id)
        {
            try
            {
                var result = await _repository.GetQuestionAsync(Id);

                if (result == null) return NotFound("Question does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }


        [HttpPut]
        [Route("EditQuestion/{Id}")]
        public async Task<ActionResult<QuestionViewModel>> EditQuestion(int Id, QuestionViewModel qvm)
        {
            try
            {
                var existingQuestion = await _repository.GetQuestionAsync(Id);
  
                if (existingQuestion == null) return NotFound($"The question does not exist");


                existingQuestion.Question1 = qvm.Question1;
                existingQuestion.Answer = qvm.Answer;
                existingQuestion.IsAnswered = qvm.IsAnswered;
               
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingQuestion);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }

        [HttpDelete]
        [Route("DeleteQuestion/{Id}")]
        public async Task<IActionResult> DeleteQuestion(int Id)
        {
            try
            {
                var existingQuestion = await _repository.GetQuestionAsync(Id);
                if (existingQuestion == null) return NotFound($"The question does not exist");

                _repository.Delete(existingQuestion);
                if (await _repository.SaveChangesAsync())
                    return Ok(existingQuestion);
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }

            return BadRequest("Your request is invalid.");
        }

        // max hours
        [HttpGet]
        [Route("GetAllMaxHours")]
        public async Task<IActionResult> GetAllMaximumHours()
        {

            try
            {
                var results = await _repository.GetAllMaximumHourAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetMaximumHour/{Id}")]
        public async Task<IActionResult> GetMaximumHourAsync(int Id)
        {
            try
            {
                var result = await _repository.GetMaximumHourAsync(Id);

                if (result == null)
                    return NotFound("Project allocation does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPut]
        [Route("EditMaxHour/{Id}")]
        public async Task<ActionResult<MaxHoursViewModel>> EditMaxHour(int Id, MaxHoursViewModel mvm)
        {
            try
            {
                var existing = await _repository.GetMaximumHourAsync(Id);
                if (existing == null) return NotFound($"The max hours does not exist");


                existing.MaxHours = mvm.maxHours;

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existing);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }

        //private string FormatProjectAllocations(List<ProjectAllocation> projectAllocations)
        //{
        //    if (projectAllocations != null && projectAllocations.Any())
        //    {
        //        // Initialize a StringBuilder to efficiently build the response message
        //        var formattedResponse = new StringBuilder("Project allocations:\n");

        //        foreach (var allocation in projectAllocations)
        //        {
        //            // Format each allocation and append it to the response
        //            formattedResponse.AppendLine($"Project: {allocation.Project.ProjectName}, " +
        //                                          $"Claim: {allocation.ClaimItem.ClaimItemName}, " +
        //                                          $"Hours Worked: {allocation.TotalNumHours}");
        //        }

        //        // Convert the StringBuilder to a string and return it
        //        return formattedResponse.ToString();
        //    }
        //    else
        //    {
        //        return "No project allocations found for this employee.";
        //    }
        //}


        //private bool awaitingEmployeeId = false; // Track if the bot is waiting for Employee ID.

        //[HttpPost]
        //[Route("Chatbot")]
        //public async Task<ActionResult<ChatResponse>> ProcessMessage([FromBody] ChatRequest chatRequest)
        //{
        //    if (chatRequest == null || string.IsNullOrWhiteSpace(chatRequest.Message))
        //    {
        //        return BadRequest("Invalid chat request.");
        //    }

        //    try
        //    {
        //        string response;

        //        // Check if the bot is waiting for Employee ID
        //        if (awaitingEmployeeId)
        //        {
        //            // Process the provided Employee ID
        //            int? employeeId = ExtractEmployeeId(chatRequest.Message);
        //            if (employeeId.HasValue)
        //            {
        //                var projectAllocations = await _repository.GetProjectAllocationsForEmployeeAsync(employeeId.Value);

        //                if (projectAllocations != null && projectAllocations.Any())
        //                {
        //                    response = FormatProjectAllocations(projectAllocations);
        //                }
        //                else
        //                {
        //                    response = "No project allocations found for this employee.";
        //                }
        //            }
        //            else
        //            {
        //                response = "Invalid Employee ID. Please enter a valid Employee ID.";
        //            }

        //            // Reset awaitingEmployeeId
        //            awaitingEmployeeId = false;
        //        }
        //        else
        //        {
        //            // Bot is not awaiting Employee ID, provide initial greeting
        //            response = "Hello! I can help you with project allocations. Please enter your Employee ID to retrieve project allocations.";
        //            awaitingEmployeeId = true;
        //        }

        //        var chatResponse = new ChatResponse { Message = response };
        //        return Ok(chatResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error and return a meaningful response
        //        // You should implement proper error handling and logging
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        //[HttpPost]
        //[Route("Chatbot")]
        //public async Task<ActionResult<ChatResponse>> ProcessMessage([FromBody] ChatRequest chatRequest)
        //{
        //    if (chatRequest == null || string.IsNullOrWhiteSpace(chatRequest.Message))
        //    {
        //        return BadRequest("Invalid chat request.");
        //    }

        //    try
        //    {
        //        string response;

        //        // Check if the bot is waiting for Employee ID
        //        if (awaitingEmployeeId)
        //        {
        //            // Process the provided Employee ID
        //            int? employeeId = ExtractEmployeeId(chatRequest.Message);
        //            if (employeeId.HasValue)
        //            {
        //                var projectAllocationsArray = await _repository.GetProjectAllocationsForEmployeeAsync(employeeId.Value);
        //                var projectAllocationsList = projectAllocationsArray.ToList(); // Convert array to list

        //                if (projectAllocationsList != null && projectAllocationsList.Any())
        //                {
        //                    response = FormatProjectAllocations(projectAllocationsList);
        //                }
        //                else
        //                {
        //                    response = "No project allocations found for this employee.";
        //                }
        //            }
        //            else
        //            {
        //                response = "Invalid Employee ID. Please enter a valid Employee ID.";
        //            }

        //            // Reset awaitingEmployeeId
        //            awaitingEmployeeId = false;
        //        }
        //        else
        //        {
        //            // Bot is not awaiting Employee ID, provide initial greeting
        //            response = "Hello! I can help you with project allocations. Please enter your Employee ID to retrieve project allocations.";
        //            awaitingEmployeeId = true;
        //        }

        //        var chatResponse = new ChatResponse { Message = response };
        //        return Ok(chatResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error and return a meaningful response
        //        // You should implement proper error handling and logging
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        //private async Task<bool> IsAwaitingEmployeeId()
        //{
        //    // Implement your logic to determine if the bot is awaiting an Employee ID.
        //    // This could involve checking the conversation state or a database.
        //    return false; // Replace with your logic.
        //}

        //private int? ExtractEmployeeId(string message)
        //{
        //    string[] words = message.Split(' ');
        //    foreach (string word in words)
        //    {
        //        if (int.TryParse(word, out int employeeId))
        //        {
        //            return employeeId;
        //        }
        //    }
        //    return null;
        //}


        [HttpGet]
        [Route("GetProjectAllocationsForEmployee/{employeeId}")]
        public async Task<IActionResult> GetProjectAllocationsForEmployee(int employeeId)
        {
            try
            {
                var projectAllocations = await _repository.GetProjectAllocationsForEmployeeAsync(employeeId);

                if (projectAllocations == null || !projectAllocations.Any())
                {
                    return NotFound("No project allocations found for this employee.");
                }

                var allocations = projectAllocations.Select(p => new
                {
                    p.ProjectAllocationId,
                    p.EmployeeId,
                    p.ProjectId,
                    p.ClaimItemId,
                    p.IsEligibleToClaim,
                    p.ClaimableAmount,
                    p.TotalNumHours,
                    p.BillableOverTime,
                    p.IsOperationalManager,
                    p.IsProjectManager,
                    startDate = p.Project.StartDate,
                    pName = p.Project.ProjectName,
                    claim = p.ClaimItem.ClaimItemName,
                    firstName = p.Employee.User.FirstName,
                    lastName = p.Employee.User.LastName,
                    enddate = p.Project.EndDate

                });

                return Ok(allocations);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }




        //// project allocation
        //[HttpGet]
        //[Route("GetAllProjectAllocations")]
        //public async Task<IActionResult> GetAllProjectAllocations()
        //{
        //    try
        //    {
        //        var results = await _repository.GetAllProjectAllocationAsync();

        //        dynamic allocations = results.Select(p => new
        //        {
        //            p.ProjectAllocationId,
        //            p.EmployeeId,
        //            p.ProjectId,
        //            p.IsEligibleToClaim,
        //            p.ClaimableAmount,
        //            p.TotalNumHours,
        //            p.BillableOverTime,
        //            p.IsOperationalManager,
        //            p.IsProjectManager,
        //            PName = p.Project.ProjectName,
        //            Claim = p.ClaimItem.ClaimItemName,
        //            Firstname = p.Employee.User.FirstName,
        //            Lastname = p.Employee.User.LastName
        //        });

        //        return Ok(allocations);

        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Internal Server Error. Please contact support.");
        //    }
        //}

        [HttpGet]
        [Route("GetProjectAllocation/{Id}")]
        public async Task<IActionResult> GetProjectAllocationAsync(int Id)
        {
            try
            {
                var result = await _repository.GetProjectAllocationAsync(Id);

                if (result == null)
                    return NotFound("Project allocation does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }
        //[HttpGet]
        //[Route("GetAllProjectAllocations")]
        //public async Task<IActionResult> GetAllProjectAllocations()
        //{
        //    try
        //    {
        //        var results = await _repository.GetAllProjectAllocationAsync();

        //        if (results != null && results.Any()) // Ensure there are results.
        //        {
        //            dynamic allocations = results.Select(p => new
        //            {
        //                p.ProjectAllocationId,
        //                p.EmployeeId,
        //                p.ProjectId,
        //                p.IsEligibleToClaim,
        //                p.ClaimableAmount,
        //                p.TotalNumHours,
        //                p.BillableOverTime,
        //                p.IsOperationalManager,
        //                p.IsProjectManager,
        //                pName = p.Project.ProjectName,
        //                claim = p.ClaimItem.ClaimItemName,
        //                firstName = p.Employee.User.FirstName,
        //                lastName = p.Employee.User.LastName
        //            });

        //            return Ok(allocations);
        //        }
        //        else
        //        {
        //            return NotFound("No project allocations found.");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Internal Server Error. Please contact support.");
        //    }
        //}

        //    [HttpGet]
        //    [Route("GetProjectAllocation/{Id}")]
        //    public async Task<IActionResult> GetProjectAllocationAsync(int Id)
        //    {
        //        try
        //        {
        //            var result = await _repository.GetProjectAllocationAsync(Id);

        //            if (result != null)
        //            {
        //                return Ok(result);
        //            }
        //            else
        //            {
        //                return NotFound("Project allocation does not exist.");
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return StatusCode(500, "Internal Server Error. Please contact support.");
        //        }
        //    }






        //  events
        [HttpGet]
        [Route("GetAllEventReports")]
        public async Task<IActionResult> GetAllEventReports()
        {

            try
            {
                var results = await _repository.GetAllEventReportAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetEventReport/{Id}")]
        public async Task<IActionResult> GetEventReportAsync(int Id)
        {
            try
            {
                var result = await _repository.GetEventReportAsync(Id);

                if (result == null)
                    return NotFound("Event does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        // project allocation
        [HttpGet]
        [Route("GetAllProjectAllocations")]
        public async Task<IActionResult> GetAllProjectAllocations()
        {
            try
            {
                var results = await _repository.GetAllProjectAllocationAsync();

                dynamic allocations = results.Select(p => new
                {
                    p.ProjectAllocationId,
                    p.EmployeeId,
                    p.ProjectId,
                    p.IsEligibleToClaim,
                    p.ClaimableAmount,
                    p.TotalNumHours,
                    p.BillableOverTime,
                    p.IsOperationalManager,
                    p.IsProjectManager,
                    pName = p.Project.ProjectName,
                    claim = p.ClaimItem.ClaimItemName,
                    firstName = p.Employee.User.FirstName,
                    lastName = p.Employee.User.LastName
                });

                return Ok(allocations);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddProjectAllocation")]
        public async Task<IActionResult> AddProjectAllocation(ProjectAllocationViewModel avm)
        {

            var allocation = new ProjectAllocation
            {
                ProjectId = avm.ProjectId,
                EmployeeId = avm.EmployeeId,
                IsEligibleToClaim = avm.IsEligibleToClaim,
                ClaimItemId = avm.ClaimItemId,
                ClaimableAmount = avm.ClaimableAmount,
                TotalNumHours = avm.TotalNumHours,
                BillableOverTime = avm.BillableOverTime,
                IsOperationalManager = avm.IsOperationalManager,
                IsProjectManager = avm.IsProjectManager

            };

            try
            {
                _repository.Add(allocation);
                await _repository.SaveChangesAsync();
            }

            catch (Exception)
            {
                return BadRequest("Invalid transaction.");
            }

            return Ok(allocation);
        }


        [HttpDelete]
        [Route("DeleteProjectAllocation/{Id}")]
        public async Task<IActionResult> DeleteProjectAllocation(int Id)
        {
            try
            {
                var existing = await _repository.GetProjectAllocationAsync(Id);
                if (existing == null) return NotFound($"The Project Allocation does not exist");

                _repository.Delete(existing);
                if (await _repository.SaveChangesAsync())
                    return Ok(existing);
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }

            return BadRequest("Your request is invalid.");
        }

        // claimItem
        [HttpGet]
        [Route("GetAllClaimItems")]
        public async Task<IActionResult> GetAllClaimItems()
        {
            try
            {
                var results = await _repository.GetAllClaimItemAsync();

                dynamic items = results.Select(p => new
                {
                    p.ClaimItemId,
                    p.ClaimItemName,
                    pType = p.ClaimType.ClaimTypeName

                });

                return Ok(items);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetClaimItem/{Id}")]
        public async Task<IActionResult> GetClaimItemAsync(int Id)
        {
            try
            {
                var result = await _repository.GetClaimItemAsync(Id);

                if (result == null) return NotFound("Claim Item does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }


        // resource
        [HttpGet]
        [Route("GetAllResources")]
        public async Task<IActionResult> GetAllResources()
        {
            try
            {
                var results = await _repository.GetAllResourceAsync();

                dynamic resources = results.Select(p => new
                {
                    p.ResourceId,
                    p.ResourceName,
                    p.ResourceDescription,
                    pType = p.ResourceType.ResourceTypeName

                });

                return Ok(resources);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetResource/{Id}")]
        public async Task<IActionResult> GetResourceAsync(int Id)
        {
            try
            {
                var result = await _repository.GetResourceAsync(Id);

                if (result == null)
                    return NotFound("Resource does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddResource")]
        public async Task<IActionResult> AddResource(ResourceViewModel rvm)
        {

            var resource = new Resource
            {
                ResourceName = rvm.ResourceName,
                ResourceDescription = rvm.ResourceDescription,
                ResourceTypeId = rvm.ResourceTypeId

            };

            try
            {
                _repository.Add(resource);
                await _repository.SaveChangesAsync();
            }

            catch (Exception)
            {
                return BadRequest("Invalid transaction.");
            }

            return Ok(resource);
        }

        [HttpPut]
        [Route("EditResource/{Id}")]
        public async Task<ActionResult<ResourceViewModel>> EditResource(int Id, ResourceViewModel rvm)
        {
            try
            {
                var existing = await _repository.GetResourceAsync(Id);
                if (existing == null) return NotFound($"The resource does not exist");

                existing.ResourceName = rvm.ResourceName;
                existing.ResourceDescription = rvm.ResourceDescription;
                existing.ResourceTypeId = rvm.ResourceTypeId;

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existing);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }

        [HttpDelete]
        [Route("DeleteResource/{Id}")]
        public async Task<IActionResult> DeleteResource(int Id)
        {
            try
            {
                var existing = await _repository.GetResourceAsync(Id);
                if (existing == null) return NotFound($"The resource does not exist");

                _repository.Delete(existing);
                if (await _repository.SaveChangesAsync())
                    return Ok(existing);
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }

            return BadRequest("Your request is invalid.");
        }

        // resource types
        [HttpGet]
        [Route("GetAllResourceTypes")]
        public async Task<IActionResult> GetAllResourceTypes()
        {
            try
            {
                var results = await _repository.GetAllResourceTypeAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetResourceType/{Id}")]
        public async Task<IActionResult> GetResourceTypeAsync(int Id)
        {
            try
            {
                var result = await _repository.GetResourceTypeAsync(Id);

                if (result == null) return NotFound("Resource type does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }


        [HttpPost]
        [Route("AddResourceType")]
        public async Task<IActionResult> AddResourceType(ResourceTypeViewModel ervm)
        {
            var type = new ResourceType
            {
                ResourceTypeName = ervm.ResourceTypeName,
                ResourceTypeDescription = ervm.ResourceTypeDescription
            };
            try
            {
                _repository.Add(type);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }
            return Ok(type);
        }

        [HttpPut]
        [Route("EditResourceType/{Id}")]
        public async Task<ActionResult<ResourceTypeViewModel>> EditResourceType(int Id, ResourceTypeViewModel ervm)
        {
            try
            {
                var existing = await _repository.GetResourceTypeAsync(Id);
                if (existing == null) return NotFound($"The resource type does not exist");


                existing.ResourceTypeName = ervm.ResourceTypeName;
                existing.ResourceTypeDescription = ervm.ResourceTypeDescription;
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existing);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }

        [HttpDelete]
        [Route("DeleteResourceType/{Id}")]
        public async Task<IActionResult> DeleteResourceType(int Id)
        {
            try
            {
                var existing = await _repository.GetResourceTypeAsync(Id);
                if (existing == null) return NotFound($"The resource type does not exist");

                _repository.Delete(existing);
                if (await _repository.SaveChangesAsync())
                    return Ok(existing);
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }

            return BadRequest("Your request is invalid.");
        }


        // alert type
        [HttpGet]
        [Route("GetAllAlertTypes")]
        public async Task<IActionResult> GetAllAlertTypes()
        {
            try
            {
                var results = await _repository.GetAllAlertTypeAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetAlertType/{Id}")]
        public async Task<IActionResult> GetAlertTypeAsync(int Id)
        {
            try
            {
                var result = await _repository.GetAlertTypeAsync(Id);

                if (result == null) return NotFound("Alert Type does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        // project
        [HttpGet]
        [Route("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var results = await _repository.GetAllProjectAsync();

                dynamic projects = results.Select(p => new
                {
                    p.ProjectId,
                    p.ProjectName,
                    p.StartDate,
                    p.EndDate,
                    p.AdminId,
                    pStatus = p.ProjectStatus.ProjectStatusName,
                    pClient = p.Client.Account


                });

                return Ok(projects);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetProject/{Id}")]
        public async Task<IActionResult> GetProjectAsync(int Id)
        {
            try
            {
                var result = await _repository.GetProjectAsync(Id);

                if (result == null)
                    return NotFound("Project does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddProject")]
        public async Task<IActionResult> AddProject(ProjectViewModel pvm)
        {

            var project = new Project
            {

                ClientId = pvm.ClientId,
                //AdminId = pvm.AdminId,
                ProjectStatusId = pvm.ProjectStatusId,
                ProjectName = pvm.ProjectName,
                StartDate = pvm.StartDate,
                EndDate = pvm.EndDate

            };

            try
            {
                _repository.Add(project);
                await _repository.SaveChangesAsync();
            }

            catch (Exception)
            {
                return BadRequest("Invalid transaction.");
            }

            return Ok(project);
        }

        [HttpPut]
        [Route("EditProject/{Id}")]
        public async Task<ActionResult<ProjectViewModel>> EditProject(int Id, ProjectViewModel pvm)
        {
            try
            {
                var existing = await _repository.GetProjectAsync(Id);
                if (existing == null) return NotFound($"The project does not exist");

                existing.ClientId = pvm.ClientId;
                //existing.AdminId = pvm.AdminId;
                existing.ProjectStatusId = pvm.ProjectStatusId;
                existing.ProjectName = pvm.ProjectName;
                existing.StartDate = pvm.StartDate;
                existing.EndDate = pvm.EndDate;


                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existing);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }

        [HttpDelete]
        [Route("DeleteProject/{Id}")]
        public async Task<IActionResult> DeleteProject(int Id)
        {
            try
            {
                var existing = await _repository.GetProjectAsync(Id);
                if (existing == null) return NotFound($"The Project does not exist");

                _repository.Delete(existing);
                if (await _repository.SaveChangesAsync())
                    return Ok(existing);
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }

            return BadRequest("Your request is invalid.");
        }


        // client
        [HttpGet]
        [Route("GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var results = await _repository.GetAllClientAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetClient/{Id}")]
        public async Task<IActionResult> GetClientAsync(int Id)
        {
            try
            {
                var result = await _repository.GetClientAsync(Id);

                if (result == null) return NotFound("Client does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }


        [HttpPost]
        [Route("AddClient")]
        public async Task<IActionResult> AddClient(ClientViewModel cvm)
        {
            var client = new Client
            {
                //AdminId = cvm.AdminId,
                Account = cvm.Account,
                AccountManager = cvm.AccountManager,
                Department = cvm.Department,
                SiteCode = cvm.SiteCode,
                ProjectCode = cvm.ProjectCode
            };
            try
            {
                _repository.Add(client);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }
            return Ok(client);
        }

        [HttpPut]
        [Route("EditClient/{Id}")]
        public async Task<ActionResult<ClientViewModel>> EditClient(int Id, ClientViewModel cvm)
        {
            try
            {
                var existing = await _repository.GetClientAsync(Id);
                if (existing == null) return NotFound($"The client does not exist");


                //existing.AdminId = cvm.AdminId;
                existing.Account = cvm.Account;
                existing.AccountManager = cvm.AccountManager;
                existing.SiteCode = cvm.SiteCode;
                existing.ProjectCode = cvm.ProjectCode;
                existing.Department = cvm.Department;

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existing);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }

        [HttpDelete]
        [Route("DeleteClient/{Id}")]
        public async Task<IActionResult> DeleteClient(int Id)
        {
            try
            {
                var existing = await _repository.GetClientAsync(Id);
                if (existing == null) return NotFound($"The client does not exist");

                _repository.Delete(existing);
                if (await _repository.SaveChangesAsync())
                    return Ok(existing);
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }

            return BadRequest("Your request is invalid.");
        }


        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<ActionResult> GetAllEmployees()
        {
            try
            {
                var results = await _repository.GetAllEmployeeAsync();

                dynamic employees = results.Select(p => new
                {
                    p.EmployeeId,
                    p.UserId,
                    pFirstName = p.User.FirstName,
                    pLastName = p.User.LastName,
                    pResource = p.Resource.ResourceName,
                    pStatus = p.EmployeeStatus.EmployeeStatusName,
                    pType = p.EmployeeType.EmployeeTypeName,
                    pRegion = p.Region.RegionName,
                    pDivision = p.Division.DivisionName

                });

                return Ok(employees);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetEmployee/{Id}")]
        public async Task<IActionResult> GetEmployeeAsync(int Id)
        {
            try
            {
                var result = await _repository.GetEmployeeAsync(Id);

                if (result == null)
                    return NotFound("Employee does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel employeeViewModel)
        {
            // Map the EmployeeViewModel to the Employee model
            var employee = new Employee
            {

                UserId = employeeViewModel.UserId,
                ResourceId = employeeViewModel.ResourceId,
                EmployeeTypeId = employeeViewModel.EmployeeTypeId,
                EmployeeStatusId = employeeViewModel.EmployeeStatusId,
                RegionId = employeeViewModel.RegionId,
                DivisionId = employeeViewModel.DivisionId,
                ManagerTypeId = employeeViewModel.ManagerTypeId
            };

            try
            {
                // Add the employee to the DbContext
                _repository.Add(employee);
                await _repository.SaveChangesAsync();
            }

            catch (Exception)
            {
                return BadRequest("Invalid transaction.");
            }

            // Return a success response
            return Ok(employee);
        }

        [HttpPut]
        [Route("EditEmployee/{Id}")]
        public async Task<ActionResult<EmployeeViewModel>> EditEmployee(int Id, EmployeeViewModel eModel)
        {
            try
            {
                var existingEmployee = await _repository.GetEmployeeAsync(Id);
                if (existingEmployee == null) return NotFound($"The employee does not exist");

                existingEmployee.UserId = eModel.UserId;
                existingEmployee.ResourceId = eModel.ResourceId;
                existingEmployee.EmployeeTypeId = eModel.EmployeeTypeId;
                existingEmployee.ManagerTypeId = eModel.ManagerTypeId;
                existingEmployee.EmployeeStatusId = eModel.EmployeeStatusId;
                existingEmployee.DivisionId = eModel.DivisionId;
                existingEmployee.RegionId = eModel.RegionId;

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingEmployee);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }

        [HttpDelete]
        [Route("DeleteEmployee/{Id}")]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            try
            {
                var existingEmployee = await _repository.GetEmployeeAsync(Id);
                if (existingEmployee == null) return NotFound($"The employee does not exist");

                _repository.Delete(existingEmployee);
                if (await _repository.SaveChangesAsync())
                    return Ok(existingEmployee);
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }

            return BadRequest("Your request is invalid.");
        }

        // employee types
        [HttpGet]
        [Route("GetAllEmployeeTypes")]
        public async Task<IActionResult> GetAllEmployeeTypes()
        {
            try
            {
                var results = await _repository.GetAllEmployeeTypeAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetEmployeeType/{etypeId}")]
        public async Task<IActionResult> GetEmployeeTypeAsync(int etypeId)
        {
            try
            {
                var result = await _repository.GetEmployeeTypeAsync(etypeId);

                if (result == null) return NotFound("Employee type does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }


        [HttpPost]
        [Route("AddEmployeeType")]
        public async Task<IActionResult> AddEmployeeType(EmployeeTypeViewModel etvm)
        {
            var etype = new EmployeeType
            {
                EmployeeTypeName = etvm.EmployeeTypeName,
                EmployeeTypeDescription = etvm.EmployeeTypeDescription
            };
            try
            {
                _repository.Add(etype);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }
            return Ok(etype);
        }

        [HttpPut]
        [Route("EditEmployeeType/{etypeId}")]
        public async Task<ActionResult<EmployeeTypeViewModel>> EditEmployeeType(int etypeId, EmployeeTypeViewModel typeModel)
        {
            try
            {
                var existingType = await _repository.GetEmployeeTypeAsync(etypeId);
                if (existingType == null) return NotFound($"The employee type does not exist");


                existingType.EmployeeTypeName = typeModel.EmployeeTypeName;
                existingType.EmployeeTypeDescription = typeModel.EmployeeTypeDescription;
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingType);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }

        [HttpDelete]
        [Route("DeleteEmployeeType/{etypeId}")]
        public async Task<IActionResult> DeleteEmployeeType(int etypeId)
        {
            try
            {
                var existingType = await _repository.GetEmployeeTypeAsync(etypeId);
                if (existingType == null) return NotFound($"The employee type does not exist");

                _repository.Delete(existingType);
                if (await _repository.SaveChangesAsync())
                    return Ok(existingType);
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }

            return BadRequest("Your request is invalid.");
        }

        //    // calendar item

        //    [HttpGet]
        //    [Route("GetAllCalendarItems")]
        //    public async Task<IActionResult> GetAllCalendarItems()
        //    {
        //        try
        //        {

        //            var results = await _repository.GetAllCalendarItemAsync();
        //            dynamic calendaritems = results.Select(p => new
        //            {
        //                p.CalendarItemId,
        //                p.CalendarId,
        //                pName = p.Calendar.CalendarItemName,
        //                p.CalendarItemDescription,
        //                p.Location,
        //                pDate = p.Calendar.Date,
        //                //p.StartTime,
        //                //p.EndTime

        //            });
        //            return Ok(calendaritems);
        //        }
        //        catch (Exception)
        //        {
        //            return StatusCode(500, "Internal Server Error. Please contact support.");
        //        }
        //    }

        //    [HttpGet]
        //    [Route("GetCalendarItem/{itemId}")]
        //    public async Task<IActionResult> GetCalendarItemAsync(int itemId)
        //    {
        //        try
        //        {
        //            var result = await _repository.GetCalendarItemAsync(itemId);

        //            if (result == null) return NotFound("Calendar item does not exist");

        //            return Ok(result);
        //        }
        //        catch (Exception)
        //        {
        //            return StatusCode(500, "Internal Server Error. Please contact support");
        //        }
        //    }

        //    [HttpPost]
        //    [Route("AddCalendarItem")]
        //    public async Task<IActionResult> AddCalendarItem(CalendarViewModel cvm)
        //    {
        //        var item = new CalendarItem
        //        {
        //            CalendarId = cvm.CalendarID,
        //            //TaskId = cvm.TaskID,
        //            //TimeCardId = cvm.TimeCardID,
        //            CalendarItemDescription = cvm.CalendarItemDescription,
        //            Location = cvm.Location,
        //            //StartTime = cvm.StartTime,
        //            //EndTime = cvm.EndTime
        //        };

        //        try
        //        {
        //            _repository.Add(item);
        //            await _repository.SaveChangesAsync();
        //        }

        //        catch (Exception)
        //        {
        //            return BadRequest("Invalid transaction.");
        //        }

        //        return Ok(item);
        //    }

        //    [HttpPut]
        //    [Route("EditCalendarItem/{Id}")]
        //    public async Task<ActionResult<CalendarViewModel>> EditCalendarItem(int Id, CalendarViewModel cModel)
        //    {
        //        try
        //        {
        //            var existingCalendarItem = await _repository.GetCalendarItemAsync(Id);
        //            if (existingCalendarItem == null) return NotFound($"The calendar item does not exist");

        //            existingCalendarItem.CalendarId = cModel.CalendarID;
        //            //existingCalendarItem.TimeCardId = cModel.TimeCardID;
        //            //existingCalendarItem.TaskId = cModel.TaskID;
        //            existingCalendarItem.Location = cModel.Location;
        //            existingCalendarItem.CalendarItemDescription = cModel.CalendarItemDescription;
        //            //existingCalendarItem.StartTime = cModel.StartTime;
        //            //existingCalendarItem.EndTime = cModel.EndTime;

        //            if (await _repository.SaveChangesAsync())
        //            {
        //                return Ok(existingCalendarItem);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return StatusCode(500, "Internal Server Error. Please contact support.");
        //        }
        //        return BadRequest("Your request is invalid.");
        //    }

        //    [HttpDelete]
        //    [Route("DeleteCalendarItem/{Id}")]
        //    public async Task<IActionResult> DeleteCalendarItem(int Id)
        //    {
        //        try
        //        {
        //            var existing = await _repository.GetCalendarItemAsync(Id);
        //            if (existing == null) return NotFound($"The calendar item does not exist");

        //            _repository.Delete(existing);
        //            if (await _repository.SaveChangesAsync())
        //                return Ok(existing);
        //        }

        //        catch (Exception)
        //        {
        //            return StatusCode(500, "Internal Server Error. Please contact support.");
        //        }

        //        return BadRequest("Your request is invalid.");
        //    }


        // calendar

        [HttpGet]
        [Route("GetAllCalendars")]
        public async Task<IActionResult> GetAllCalendars()
        {
            try
            {
                var results = await _repository.GetAllCalendarAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetCalendar/{Id}")]
        public async Task<IActionResult> GetCalendarAsync(int Id)
        {
            try
            {
                var result = await _repository.GetCalendarAsync(Id);

                if (result == null) return NotFound("Calendar does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        [HttpPost]
        [Route("AddCalendar")]
        public async Task<IActionResult> AddCalendar(CalendarViewModel cvm)
        {
            var citem = new Models.Calendar
            {

                CalendarItemName = cvm.CalendarItemName,
                Date = cvm.Date,
                Location = cvm.Location,
                CalendarItemDescription = cvm.CalendarItemDescription,
                StartTime = cvm.StartTime,
                EndTime = cvm.EndTime
            };
            try
            {
                _repository.Add(citem);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }
            return Ok(citem);
        }


        [HttpPut]
        [Route("EditCalendar/{Id}")]
        public async Task<ActionResult<CalendarViewModel>> EditCalendar(int Id, CalendarViewModel cModel)
        {
            try
            {
                var existingCalendar = await _repository.GetCalendarAsync(Id);
                if (existingCalendar == null) return NotFound($"The calendar does not exist");


                existingCalendar.CalendarItemName = cModel.CalendarItemName;
                existingCalendar.Date = cModel.Date;
                existingCalendar.Location = cModel.Location;
                existingCalendar.CalendarItemDescription = cModel.CalendarItemDescription;
                existingCalendar.StartTime = cModel.StartTime;
                existingCalendar.EndTime = cModel.EndTime;

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingCalendar);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }

        [HttpDelete]
        [Route("DeleteCalendar/{Id}")]
        public async Task<IActionResult> DeleteCalendar(int Id)
        {
            try
            {
                var existing = await _repository.GetCalendarAsync(Id);
                if (existing == null) return NotFound($"The calendar does not exist");

                _repository.Delete(existing);
                if (await _repository.SaveChangesAsync())
                    return Ok(existing);
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }

            return BadRequest("Your request is invalid.");
        }

        //employee statuses
        [HttpGet]
        [Route("GetAllEmployeeStatuses")]
        public async Task<IActionResult> GetAllEmployeeStatuses()
        {
            try
            {
                var results = await _repository.GetAllEmployeeStatusAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        // user
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var results = await _repository.GetAllUserAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //manager type
        [HttpGet]
        [Route("GetAllManagerTypes")]
        public async Task<IActionResult> GetAllManagerTypes()
        {
            try
            {
                var results = await _repository.GetAllManagerTypeAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //titles
        [HttpGet]
        [Route("GetAllTitles")]
        public async Task<IActionResult> GetAllTitles()
        {
            try
            {
                var results = await _repository.GetAllTitleAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }




        // division

        [HttpGet]
        [Route("GetAllDivisions")]
        public async Task<IActionResult> GetAllDivisions()
        {
            try
            {
                var results = await _repository.GetAllDivisionAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetDivision/{Id}")]
        public async Task<IActionResult> GetDivisionAsync(int Id)
        {
            try
            {
                var result = await _repository.GetDivisionAsync(Id);

                if (result == null) return NotFound("Division does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        // regions

        [HttpGet]
        [Route("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            try
            {
                var results = await _repository.GetAllRegionAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetRegion/{Id}")]
        public async Task<IActionResult> GetRegionAsync(int Id)
        {
            try
            {
                var result = await _repository.GetRegionAsync(Id);

                if (result == null) return NotFound("Region does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }

        // project status

        [HttpGet]
        [Route("GetAllProjectStatus")]
        public async Task<IActionResult> GetAllProjectStatus()
        {
            try
            {
                var results = await _repository.GetAllProjectStatusAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetProjectStatus/{Id}")]
        public async Task<IActionResult> GetProjectStatusAsync(int Id)
        {
            try
            {
                var result = await _repository.GetProjectStatusAsync(Id);

                if (result == null) return NotFound("Division does not exist");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support");
            }
        }
    }

}
