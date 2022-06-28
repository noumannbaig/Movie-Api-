using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.DataTransferObjects.Shared
{
    public class GeneralResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class GeneralResponseWithCode
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class OrderByResponseModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
