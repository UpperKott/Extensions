namespace Mapping.Interfaces
{
    public interface IMapperConfig<T> where T : ConfigItemBase
    {
        T[] Properties { get; set; }
    }
}
