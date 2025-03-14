using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.Model;
using System.Threading.Tasks;
using RepositoryLayer.Service;
using BusinessLayer.Service;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloGreetingController : ControllerBase
    {
        private readonly ILogger<HelloGreetingController> _logger;
        private readonly IGreetingBL _greetingBL;
        public HelloGreetingController(IGreetingBL greetingBL, ILogger<HelloGreetingController> logger)
        {
            _greetingBL = greetingBL;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a welcome greeting from the API.
        /// </summary>

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("GET request received for greeting");

            var greetingResult = _greetingBL.GetGreeting();
            var data = new
            {
                Greeting = greetingResult,
                Date = DateTime.Now
            };

            var response = new ResponseModel<object>
            {
                Success = true,
                Message = "Request successful",
                Data = data
            };
            _logger.LogInformation("GET request successful, returning response.");
            return Ok(response);
        }

        /// <summary>
        /// Creates a personalized greeting based on the provided request data.
        /// </summary>
        /// <param name="request">The RequestModel containing first name, last name, and email.</param>
        /// <returns>A ResponseModel with a personalized greeting, email, and creation timestamp.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] RequestModel request)
        {
            _logger.LogInformation("POST request received with FirstName: {FirstName}, LastName: {LastName}}", request.FirstName, request.LastName);

            var greetingResult = _greetingBL.GetGreeting(request.FirstName, request.LastName);
            var data = new
            {
                Greeting = greetingResult,
                ReceivedAt = DateTime.Now
            };

            var response = new ResponseModel<object>
            {
                Success = true,
                Message = "Greeting created",
                Data = data
            };
            return Ok(response);
        }

        /// <summary>
        /// Updates a greeting with new user information.
        /// </summary>
        /// <param name="request">The RequestModel containing updated first name, last name, and email.</param>
        /// <returns>A ResponseModel with the updated full name, email, and update timestamp.</returns>
        [HttpPut]
        public IActionResult Put([FromBody] RequestModel request)
        {
            _logger.LogInformation("PUT request received with FirstName: {FirstName}, LastName: {LastName}}", request.FirstName, request.LastName);

            var data = new
            {
                FullName = $"{request.FirstName} {request.LastName}",
                UpdatedAt = DateTime.Now
            };

            var response = new ResponseModel<object>
            {
                Success = true,
                Message = "Greeting updated",
                Data = data
            };

            return Ok(response);
        }

        /// <summary>
        /// Partially updates a greeting with the provided fields.
        /// </summary>
        /// <param name="request">The RequestModel with optional fields to update (first name, last name, or email).</param>
        /// <returns>A ResponseModel showing which fields were updated and the update timestamp.</returns>
        [HttpPatch]
        public IActionResult Patch([FromBody] RequestModel request)
        {
            _logger.LogInformation("PATCH request received with FirstName: {FirstName}, LastName: {LastName}", request.FirstName, request.LastName);

            var data = new
            {
                UpdatedFields = new
                {
                    FirstName = string.IsNullOrEmpty(request.FirstName) ? "Not updated" : request.FirstName,
                    LastName = string.IsNullOrEmpty(request.LastName) ? "Not updated" : request.LastName,
                },
                UpdatedAt = DateTime.Now
            };

            var response = new ResponseModel<object>
            {
                Success = true,
                Message = "Greeting partially updated",
                Data = data
            };

            return Ok(response);
        }

        /// <summary>
        /// Deletes a greeting.
        /// </summary>
        /// <returns>A ResponseModel confirming deletion with a timestamp.</returns>
        [HttpDelete]
        public IActionResult Delete()
        {
            _logger.LogInformation("DELETE request received for greeting");

            var data = new
            {
                DeletedAt = DateTime.Now
            };

            var response = new ResponseModel<object>
            {
                Success = true,
                Message = "Greeting deleted",
                Data = data
            };

            return Ok(response);
        }

        /// <summary>
        ///     Add Greeting
        /// </summary>
        /// <param name="greeting"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddGreeting")]
        public IActionResult AddGreeting(SaveGreetingModel greeting)
        {
            _logger.LogInformation("POST request received.");
            var response = _greetingBL.AddGreeting(greeting);
            _logger.LogInformation("POST response: {@Response}", response);
            return Ok(response);
        }

        /// <summary>
        /// Get Greeting by Id    
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetGreetingById")]
        public IActionResult GetGreetingById(int id)
        {
            _logger.LogInformation("GET request received.");
            var response = _greetingBL.GetGreetingById(id);
            if (response == null)
            {
                return NotFound();
            }
            _logger.LogInformation("GET response: {@Response}", response);
            return Ok(response);
        }

        /// <summary>
        /// Get All Greeting Messages
        /// </summary>
        [HttpGet]
        [Route("GetAllGreetingMessage")]
        public IActionResult GetAllGreetingMessage()
        {
            _logger.LogInformation("GET request received.");
            var response = _greetingBL.GetAllGreetingMessage();
            _logger.LogInformation("GET response: {@Response}", response);
            return Ok(response);
        }

        /// <summary>
        /// Update Greeting
        /// </summary>
        /// <param name="id"></param>
        /// <param string="message"> </param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateGreeting")]
        public IActionResult UpdateGreeting(int id, string message)
        {
            _logger.LogInformation("PUT request received.");
            var response = _greetingBL.UpdateGreeting(id, message);
            if (response == null)
            {
                return NotFound();
            }
            _logger.LogInformation("PUT response: {@Response}", response);
            return Ok(response);
        }


        /// <summary>
        /// Delete Greeting Message
        /// </summary>
        /// <param name="id"></param>
        /// <return></return>
        [HttpDelete]
        [Route("DeleteGreeting")]
        public IActionResult DeleteGreeting(int id)
        {
            _logger.LogInformation("DELETE request received.");
            var response = _greetingBL.DeleteGreeting(id);
            if (response == false)
            {
                string error = "Greeting not found";
                return Ok(error);
            }
            else
            {
                _logger.LogInformation("DELETE response: {@Response}", response);
                string msg = "Greeting Deleted";
                return Ok(msg);
            }
        }
    }
}
