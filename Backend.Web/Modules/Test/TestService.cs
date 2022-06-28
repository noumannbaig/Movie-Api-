using Backend.Application.DataTransferObjects.Test;
using Backend.Application.DataTransferObjects.Shared;
using Backend.Application.Enums;
using Backend.Application.Extentions;
using Backend.Application.Wrappers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.IRepositories.ITest;

namespace Backend.Web.Modules.Test
{
    public class TestService
    {
        private readonly ITestRepository testRepository;
        public TestService(ITestRepository _testRepository)
         {
            testRepository = _testRepository;
         }

        public Response<dynamic> CreateMaterial(TestRequestInfo input)
        {
            TestResponseInfo GetMaterial = testRepository.CreateTest(input);
            return new Response<dynamic>(true, GetMaterial, GeneralMessage.RecordAdded);
        }

    }
}
