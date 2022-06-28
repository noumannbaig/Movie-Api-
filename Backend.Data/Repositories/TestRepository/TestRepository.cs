using Backend.Data.IRepositories.ITest;
using System;
using Backend.Application.DataTransferObjects.Test;
using Backend.Data.Entities.Test;
using System.Transactions;
using System.Linq;

namespace Backend.Data.Repositories.TestRepository
{
    public class TestRepository : ITestRepository, IDisposable
    {
        private readonly EFDataContext _context;

        public TestRepository(EFDataContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }

        public TestResponseInfo CreateTest(TestRequestInfo testRequestInfo)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Test test = new Test
                {
                    Key = testRequestInfo.Key,
                    Value = testRequestInfo.Value
                };
                _context.Test.Add(test);
                _context.SaveChanges();
                scope.Complete();
                TestResponseInfo output = new TestResponseInfo
                {
                    TestId = test.TestId,
                    Key = test.Key,
                    Value = test.Value
                };
                return output;
            }
        }

    }
}
