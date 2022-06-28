using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Helpers
{
    public static class RandomNumber
    {
        public static string Create()
        {
            var randomGenerator = RandomNumberGenerator.Create(); // Compliant for security-sensitive use cases
            byte[] data = new byte[2];
            randomGenerator.GetBytes(data);
            return BitConverter.ToString(data);
        }
    }
}
