using ParserApi.Controllers.Models;
using ParserApi.Parsers.Site911.Models;
using ParserCore;
using ParserCore.Abstraction;
using ParserCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace ParserApi.Parsers.Site911ParserCore
{
    public class Site911Parser : ParserBase<In911, Result911>
    {
        public IInMemoryWorkerLogger WorkerLogger { get; }

        public Site911Parser(
            [Dependency(nameof(Parser.Site911))]IWorkersContainer workersContainer, 
            [Dependency(nameof(Parser.Site911))]IInMemoryWorkerLogger workerLogger
            ) : base(workersContainer)
        {
            WorkerLogger = workerLogger;
        }

        protected override Result911 PrepareResult()
        {
            return new Result911
            {
                Parts = GetParts(),
                PerRequestParts = GetPerRequestParts(),
                AnalogParts = new List<AnalogPart>(),
                MatchingParts = new List<MatchingPart>(),
                RawData = new RawDataSite911
                {
                    Primary = result.Get<PrimaryResultStep2>(),
                    Secondary = result.Get<SecondaryResultStep4>()
                }
            };
        }

        private IEnumerable<Part> GetParts()
        {
            var primary = result.Get<PrimaryResultStep2>();
            var table = new List<Part>
            {
                new Part
                {
                    ParserName = nameof(Parser.Site911),
                    Name = primary?.Name,
                    Brand = "", // todo
                    Price = primary?.Price.ToString(),
                    LinkToSource = $"https://911auto.com.ua/search/" // todo
                }
            };
            return table;
        }
        
        private IEnumerable<PerRequestPart> GetPerRequestParts()
        {
            var secondary = result.Get<SecondaryResultStep4>();
            var perRequestParts = secondary.Table?.Select(r => new PerRequestPart
            {
                ParserName = nameof(Parser.Site911),
                PartId = r.PartId,
                Brand = r.Brand,
                Name = r.Name,
                Qty = r.Qty,
                PriceUah = r.PriceUah,
                LinkToSource = new Uri($"https://911auto.com.ua/search/{r.PartId}")
            });
            return perRequestParts;
        }
    }
}