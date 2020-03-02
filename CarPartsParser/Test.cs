using CarPartsParser.SiteParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsParser
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var a = new AdapterA();

            var typeOfResult = a.GetType().GetInterfaces().First(i => i.Name.Contains(nameof(IResultAdapter<IParsedResult>))).GetGenericArguments().First();

            Assert.AreEqual(typeof(ResultA), typeOfResult);
        }
    }
}
