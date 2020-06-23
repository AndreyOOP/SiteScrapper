using ParserCoreProject.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserCoreProject.ParserCore
{
    public class WorkerPreprocessorsContainer : IWorkerPreprocessorsContainer
    {
        protected List<IWorkerPreprocessor> preprocessors = new List<IWorkerPreprocessor>();

        public IEnumerable<IWorkerPreprocessor> GetPreprocessors() => preprocessors;

        public T GetPreprocessor<T>() where T : IWorkerPreprocessor // ToDo: tests
        {
            return preprocessors.OfType<T>().FirstOrDefault();
        }

        public void RegisterPreprocessor(IWorkerPreprocessor preprocessor)
        {
            if (preprocessor == null)
                throw new ArgumentNullException();

            preprocessors.Add(preprocessor);
        }
    }
}
