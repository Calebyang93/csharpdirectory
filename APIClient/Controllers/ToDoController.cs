
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using TD.Data;
using TD.Model;

namespace APIHost.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class ToDoController : ControllerBase
    {
        private static readonly string[] Categories = new[]
        {
           "Art", "Professional", "Sport", "Family", "Culture"
        };

        private readonly ILogger<ToDoController> _logger;

        public ToDoController(ILogger<ToDoController> logger)
        {
            _logger = logger;
        }
        // Example Postman URL: https://localhost:44328/todo/api/getbytest/123
        [HttpGet]
        [Route("api/getbytest/{status}")]
        public IEnumerable<string> GetByTest(int status)
        {
            return new List<string>(new string[] { "abc", "def" });
        }

        // Example Postman URL: https://localhost:44328/todo/api/gettest_allok/xyz
        [HttpGet]
        [Route("api/gettest_allok/{s}")]
        public ActionResult<List<string>> GetTest_AllOk(string s)
        {
            List<string> dataRequested = new List<string>(new string[] { "abc", "def" });
            return base.Ok(dataRequested);
        }

        // Example Postman URL: https://localhost:44328/todo/api/gettest_error/xyz
        [HttpGet]
        [Route("api/gettest_error/{s}")]
        public ActionResult<List<string>> GetTest_Error(string s)
        {
            string msg = "This is a custom message from the controller [GetTest_Error]";
            return base.Problem(msg);
        }

        // Example Postman URL: https://localhost:44328/todo/api/gettodoitem
        [HttpGet]
        [Route("api/gettodoitem")]
        public ToDoItem GetToDoItem()
        {
            return new ToDoItem()
            {
                ID = Guid.NewGuid(),
                Category = "Cat1",
                CreatedOn = DateTime.Now,
                Deadline = DateTime.Now.AddDays(10),
                Description = "Some description",
                Status = ToDoStatus.InProgress,
                UserName = Environment.UserName
            };
        }

        // Example Postman URL: https://localhost:44328/todo/api/getbystatus/123
        [HttpGet]
        [Route("api/getbystatus_original/{status}")]
        public IEnumerable<ToDoItem> GetByStatus_Original(ToDoStatus status)
        {
            try
            {
                // return new List<string>(new string[] { status.ToString() });
                ToDoRepository tdr = new ToDoRepository();
                var lst = tdr.GetByStatus(status);
                return lst;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetByStatus: " + ex.Message);
                return new List<ToDoItem>();
            }
        }
        // Example Postman URL: https://localhost:44328/todo/api/getbystatus/123
        [HttpGet]
        [Route("api/getbystatus/{status}")]
        public IEnumerable<ToDoItem> GetByStatus(int status)
        {
            try
            {
                _logger.LogInformation("Hello");
                if (Enum.IsDefined(typeof(ToDoStatus), status))
                {
                    ToDoStatus s = (ToDoStatus)status;
                    // return new List<string>(new string[] { status.ToString() });
                    ToDoRepository tdr = new ToDoRepository();
                    var lst = tdr.GetByStatus(s);
                    return lst;
                }
                else
                    return new List<ToDoItem>();
            }
            catch (Exception ex)
            {
                _logger.LogError("GetByStatus: " + ex.Message);
                return new List<ToDoItem>();
            }
        }

        //// Example Postman URL: https://localhost:44328/todo/api/getbydescription/123
        [HttpGet]
        [Route("api/getbydescription/{description}")]
        public List<ToDoItem> GetByDescription(string description)
        {
            try
            {
                //List<ToDoItem> lst = new List<ToDoItem>();
                //ToDoItem x = new ToDoItem();
                //ToDoItem y = new ToDoItem();
                //if (lst.Contains(x))
                //    return null;

                _logger.LogInformation("Description Error");
                ToDoRepository tdr = new ToDoRepository();
                var tdd = tdr.GetByDescription(description);
                return tdd;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetByDescription: " + ex.Message);
                return new List<ToDoItem>();
            }
        }

        // Example Postman URL: https://localhost:44328/todo/api/itemadd
        [HttpPost]
        [Route("api/itemadd")]
        public ActionResult ItemAdd([FromBody] ToDoItem td)
        {
            try
            {
                if (td.Status == ToDoStatus.Unknown)
                    return Problem("Can not add item with status Unknown");
                else
                {
                    // Force an exception
                    //int i = 0;
                    //int j = 1 / i;

                    ToDoRepository tdr = new ToDoRepository();
                    tdr.Add(td);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return Problem($"ItemAdd error: {ex.Message}");
            }
        }

        // Example Postman URL: https://localhost:44328/todo/api/itemupdate
        [HttpPut]
        [Route("api/itemupdate")]
        public ActionResult ItemUpdate([FromBody] ToDoItem td)
        {
            try
            {
                if (td.Status == ToDoStatus.Completed && td.CreatedOn == td.Deadline)
                {
                    return Problem("The Todo item has been completed. No need for update");
                }
                else
                {
                    ToDoRepository tdr = new ToDoRepository();
                    tdr.Update(td);
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return Problem($"Item update error: {e.Message}");
            }
        }

    }
}
/*
 Example serialised ToDoItem:

 {
    "id": "ee9a82a4-738f-4db3-b818-6c4404ae5c8d",
    "description": "Some description",
    "createdOn": "2020-09-10T23:25:26.0184413+08:00",
    "category": "Cat1",
    "userName": "James",
    "status": 2,
    "deadline": "2020-09-20T23:25:26.0199838+08:00"
}

 */
