﻿using ParserApi.Parsers.Autoklad.Models;
using ParserApi.Parsers.Site911.Models;

namespace ParserApi.Controllers.Models
{
    public class ParsersResult
    {
        public Result911 Site911 { get; set; }
        public ResultAK Autoklad { get; set; }
    }
}