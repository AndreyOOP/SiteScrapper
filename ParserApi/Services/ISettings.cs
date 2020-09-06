namespace ParserApi.Services
{
    public interface ISettings<TSettings>
    {
        /// <summary>
        /// Current settings
        /// </summary>
        TSettings Settings { get; }

        /// <summary>
        /// Update setting object to new one
        /// </summary>
        /// <param name="settings">New settings</param>
        void Update(TSettings settings);
    }
}
