using System.Runtime.Serialization;

namespace Mapping
{
    public class TypeDefinition
    {
        /// <summary>
        /// Имя типа
        /// </summary>
        [DataMember(Name = "typeName")]
        public string TypeName { get; set; }
        public PropertyMapping[] TypeMapping { get; set; }
    }
}