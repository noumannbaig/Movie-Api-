using Backend.Application.DataTransferObjects.Test;
using System;
using System.Linq;

namespace Backend.Data.IRepositories.ITest
{
    public interface ITestRepository
    {
        TestResponseInfo CreateTest(TestRequestInfo testRequestInfo);
    }
}
