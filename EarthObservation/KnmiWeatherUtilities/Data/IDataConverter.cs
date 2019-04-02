namespace KnmiWeatherUtilities.Data
{
    public interface IDataConverter<T, R>
    {
        R Convert(T input);
    }
}
