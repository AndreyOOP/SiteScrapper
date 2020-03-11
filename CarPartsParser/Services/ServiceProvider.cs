using CarPartsParser.Abstraction.Services;

namespace CarPartsParser.Services
{
    // not sure if it is really needed
    // WorkUnit created to make each step simple like get data | parse html | parse json etc. probably normally only 1 service per WorkUnit required
    // from another hand if few services needed | their qty will change it is easier to inject single service provider
    public class ServiceProvider : IServiceProvider
    {
        private readonly IServiceA serviceA;
        private readonly IServiceB serviceB;

        public ServiceProvider(IServiceA serviceA, IServiceB serviceB)
        {
            this.serviceA = serviceA;
            this.serviceB = serviceB;
        }

        public IServiceA GetServiceA() => serviceA;
        public IServiceB GetServiceB() => serviceB;
    }
}
