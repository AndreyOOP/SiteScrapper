namespace CarPartsParser.Abstraction.Services
{
    public interface IServiceProvider
    {
        IServiceA GetServiceA();

        IServiceB GetServiceB();
    }
}
