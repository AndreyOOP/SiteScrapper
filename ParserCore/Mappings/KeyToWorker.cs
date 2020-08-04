namespace ParserCore
{
    public class KeyToWorker
    {
        /// <summary>
        /// Key representing Worker<TIn, TOut>
        /// </summary>
        public IInOutKey Key { get; set; }

        /// <summary>
        /// Worker<TIn, TOut>
        /// </summary>
        public object Worker { get; set; }
    }
}
