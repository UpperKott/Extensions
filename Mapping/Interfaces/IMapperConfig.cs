namespace Mapping.Interfaces
{
    public interface IMapperConfig<TType,TProp> where TType : TypeDefinition where TProp : PropertyMapping
    {
        TType[] TypeDefinitions { get; set; }
        TProp[] Properties { get; set; }
    }
}
