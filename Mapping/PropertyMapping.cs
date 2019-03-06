using System.Runtime.Serialization;

namespace Mapping
{
    public class PropertyMapping
    {
        /// <summary>
        /// Тип конвертации в маппинге
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Regex для парсинга
        /// </summary>
        [DataMember(Name = "pattern")]
        public string Pattern { get; set; }

        /// <summary>
        /// Свойство из которого происходит маппинг
        /// </summary>
        [DataMember(Name = "source")]
        public string Source { get; set; }

        /// <summary>
        /// Свойство из которого происходит маппинг
        /// </summary>
        [DataMember(Name = "target")]
        public string Target { get; set; }
    }
}
