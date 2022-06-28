using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.DataTransferObjects.Test
{
    public class TestResponseInfo
    {
        public Guid TestId { get; set; }       
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
