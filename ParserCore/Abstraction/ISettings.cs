namespace ParserCore.Abstraction
{
    public interface ISettings<TSettings>
    {
        /// <summary>
        /// Update setting object to new one
        /// </summary>
        /// <param name="settings">New settings</param>
        void UpdateSettings(TSettings settings);
    }
}
