using CarPartsParser.Abstraction.Models;

namespace CarPartsParser.SiteParsers.Abstraction.WorkUnits
{
    public interface IWorkUnit 
    {
        /// <summary>
        /// Base step of data handling - it receives some input & proceeds output, input received from previous step & output can be used in the next step
        /// </summary>
        /// <param name="input">input model</param>
        /// <param name="siteParserResult">final output model, it could be used in case some result could be get from the middle step. E.g error can be at any step</param>
        /// <returns></returns>
        IWorkUnitModel Execute(IWorkUnitModel input, ref IWorkUnitModel siteParserResult);
    }
}
