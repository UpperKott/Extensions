namespace Mapping.Interfaces
{
    public interface IMapperConfig<TType,TProp> where TType : TypeConversionConfig where TProp : PropertyMapperConfig
    {
        TType[] Types { get; set; }
        TProp[] Properties { get; set; }
    }
}
