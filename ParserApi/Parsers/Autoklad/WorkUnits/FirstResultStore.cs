using ParserApi.Parsers.Autoklad.Models;

namespace ParserApi.Parsers.Autoklad.WorkUnits
{
    public class FirstResultStore : AutokladWorkerBase<FirstResultAK, FirstResultStoreAK>
    {
        public override FirstResultStoreAK Parse(FirstResultAK model)
        {
            return new FirstResultStoreAK
            {
                FirstResult = model.FirstResult
            };
        }
    }
}