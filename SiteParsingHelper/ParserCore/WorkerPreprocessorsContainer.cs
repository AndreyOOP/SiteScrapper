using ParserCoreProject.Abstraction;
using System;
using System.Collections.Generic;

namespace ParserCoreProject.ParserCore
{
    public class WorkerPreprocessorsContainer : IWorkerPreprocessorsContainer
    {
        protected List<IWorkerPreprocessor> preprocessors = new List<IWorkerPreprocessor>();

        public IEnumerable<IWorkerPreprocessor> GetPreprocessors() => preprocessors;

        public void RegisterPreprocessor(IWorkerPreprocessor preprocessor)
        {
            if (preprocessor == null)
                throw new ArgumentNullException();

            preprocessors.Add(preprocessor);
        }
    }
}
