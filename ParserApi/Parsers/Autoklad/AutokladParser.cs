using ParserApi.Controllers.Models;
using ParserApi.Parsers.Autoklad.Models;
using ParserCore;
using ParserCore.Abstraction;
using ParserCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace ParserApi.Parsers.Autoklad
{
    public class AutokladParser : ParserBase<InAK, ResultAK>
    {
        public IInMemoryWorkerLogger WorkerLogger { get; }

        public AutokladParser(
            [Dependency(nameof(Parser.Autoklad))]IWorkersContainer workersContainer,
            [Dependency(nameof(Parser.Autoklad))]IInMemoryWorkerLogger workerLogger
            ) : base(workersContainer)
        {
            WorkerLogger = workerLogger;
        }

        protected override ResultAK PrepareResult()
        {
            return new ResultAK
            {
                Parts = GetParts(),
                PerRequestParts = new List<PerRequestPart>(),
                AnalogParts = GetAnalogParts(),
                MatchingParts = GetMatchingParts(),
                RawData = new RawDataAK
                {
                     FirstResult = result.Get<FirstResultStoreAK>()?.FirstResult,
                     Analogs = result.Get<SecondResultAK>()?.Analogs,
                     Same = result.Get<SecondResultAK>()?.Same
                }
            };
        }

        private IEnumerable<Part> GetParts()
        {
            var firstResult = result.Get<FirstResultStoreAK>().FirstResult;

            var parts = firstResult?.Select(p => new Part
            {
                ParserName = nameof(Parser.Autoklad),
                Name = p.Name,
                Brand = p.Brand,
                Price = p.Price,
                LinkToSource = $"https://www.autoklad.ua/{p.PartBrandLink}"
            });
            return parts;
        }

        private IEnumerable<AnalogPart> GetAnalogParts()
        {
            var analogs = result.Get<SecondResultAK>()?.Analogs;

            var parts = analogs?.SelectMany(p => p.Value.Select(x => new AnalogPart
            {
                ParserName = nameof(Parser.Autoklad),
                Name = x.Name,
                Price = x.Price,
                LinkToSource = new Uri($"https://www.autoklad.ua/{p.Key}")
            }));
            return parts;
        }

        private IEnumerable<MatchingPart> GetMatchingParts()
        {
            var matches = result?.Get<SecondResultAK>()?.Same;

            var parts = matches?.SelectMany(p => p.Value.Select(x => new MatchingPart
            {
                ParserName = nameof(Parser.Autoklad),
                Name = x.Name,
                Price = x.Price,
                LinkToSource = new Uri($"https://www.autoklad.ua/{p.Key}")
            }));
            return parts;
        }
    }
}