using System.Runtime.Serialization;

namespace Mapping
{
    public class TypeConversionConfig
    {
        /// <summary>
        /// Имя типа
        /// </summary>
        [DataMember(Name = "typeName")]
        public string TypeName { get; set; }
        public PropertyMapperConfig[] Properties { get; set; }
    }
}