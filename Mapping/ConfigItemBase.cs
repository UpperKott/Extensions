namespace Mapping
{
    public class ConfigItemBase
    {
        /// <summary>
        /// Тип (int, string, decimal)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Тми свойство которое маппится
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Regex для парсинга
        /// </summary>
        public string pattern { get; set; }
    }
}
