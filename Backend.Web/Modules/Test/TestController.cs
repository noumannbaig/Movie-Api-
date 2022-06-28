using Backend.Application.DataTransferObjects.Test;
using HRM.Web.Controllers.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Web.Modules.Test
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        private readonly TestService testService;
        public TestController(TestService _testService)
        {
            testService = _testService;
        }
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Hurrey!.... ILI Digital's Server is running properly");
        }

        [HttpPost]
        public IActionResult TestCreate(TestRequestInfo testRequestInfo)
        {
            return Ok(testService.CreateMaterial(testRequestInfo));
        }
    }
}
