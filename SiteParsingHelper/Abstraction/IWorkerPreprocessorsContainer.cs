using System.Collections.Generic;

namespace ParserCoreProject.Abstraction
{
    public interface IWorkerPreprocessorsContainer
    {
        void RegisterPreprocessor(IWorkerPreprocessor preprocessor);

        IEnumerable<IWorkerPreprocessor> GetPreprocessors();

        T GetPreprocessor<T>() where T : IWorkerPreprocessor;
    }
}
