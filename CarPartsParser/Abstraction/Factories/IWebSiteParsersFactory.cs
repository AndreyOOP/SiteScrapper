﻿using CarPartsParser.Models;
using CarPartsParser.SiteParsers.Abstraction;
using System.Collections.Generic;

namespace CarPartsParser.Abstraction.Factories
{
    public interface IWebSiteParsersFactory
    {
        IEnumerable<IWebSiteParser<ParserExecutorResultBase>> GetAll();
    }
}
