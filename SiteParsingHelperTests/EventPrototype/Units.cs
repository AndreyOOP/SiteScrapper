using SiteParsingHelper.Event.Abstraction;

namespace SiteParsingHelperTests.EventPrototype
{
    class UnitA : WorkUnitBase<ModelInA, ModelInB, ParsingResult>
    {
        public UnitA(IWebParser<ParsingResult> eventBus) : base(eventBus)
        {
        }

        protected override ModelInB ParseUnit(ModelInA model)
        {
            webParser.Result.A = "a";
            return new ModelInB();
        }

        protected override void SelectWorkUnit(ModelInB model)
        {
            webParser.ExecuteUnit<ModelInB, ModelInC>(model);
        }
    }

    class UnitB : WorkUnitBase<ModelInB, ModelInC, ParsingResult>
    {
        public UnitB(IWebParser<ParsingResult> eventBus) : base(eventBus)
        {
        }

        protected override ModelInC ParseUnit(ModelInB model)
        {
            webParser.Result.B = "b";
            return new ModelInC();
        }

        protected override void SelectWorkUnit(ModelInC model)
        {
            return;
        }
    }
}
