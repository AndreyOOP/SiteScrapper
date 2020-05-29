using CarPartsParser.Abstraction.Services;
using System;

namespace CarPartsParser.Services
{
    public class ServiceA : IServiceA
    {
        public string ExecuteServiceA()
        {
            return "Execute Service A";
        }
    }
}
