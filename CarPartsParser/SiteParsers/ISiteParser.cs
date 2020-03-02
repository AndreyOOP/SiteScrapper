using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsParser.SiteParsers
{
    public interface ISiteParser
    {
        IParsedResult Parse(string id);
    }

    public interface IParsedResult
    {
    }

    public class SiteParserA : ISiteParser
    {
        public IParsedResult Parse(string id)
        {
            return new ResultA();
        }
    }

    public class SiteParserB : ISiteParser
    {
        public IParsedResult Parse(string id)
        {
            return new ResultB();
        }
    }

    public class ResultA : IParsedResult
    {
    }

    public class ResultB : IParsedResult
    {
    }

    public class ResultMain : IParsedResult { }

    public interface IResultAdapter<T> where T : IParsedResult
    {
        ResultMain Convert(T res);
    }

    public class AdapterA : IResultAdapter<ResultA>
    {
        public ResultMain Convert(ResultA res)
        {
            return new ResultMain();
        }
    }

    public class AdapterB : IResultAdapter<ResultB>
    {
        public ResultMain Convert(ResultB res)
        {
            return new ResultMain();
        }
    }
}
