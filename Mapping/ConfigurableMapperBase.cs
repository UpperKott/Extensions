using System;
using System.Text.RegularExpressions;
using Mapping.Interfaces;

namespace Mapping
{
    public abstract class ConfigurableMapperBase<TIn, TConfig, TOut>
        where TConfig : ConfigItemBase
        where TOut : new()
    {
        public const string DECIMAL_S = "decimal";
        public const string STRING_S = "string";
        public const string INT_S = "int";

        protected IMapperConfig<TConfig> MapperConfig;

        public ConfigurableMapperBase(IMapperConfig<TConfig> mapperConfig)
        {
            MapperConfig = mapperConfig;
        }

        public TOut Map(TIn input)
        {
            var mapped = new TOut();

            foreach (var itemConfig in MapperConfig.Properties)
            {
                var propertyToMap = mapped.GetType().GetProperty(itemConfig.name);
                if (propertyToMap == null) continue;

                propertyToMap.SetValue(mapped, GetValue(itemConfig, input));
            }

            return mapped;
        }

        private object GetValue(TConfig config, TIn input)
        {
            var value = GetValueImpl(config, input);

            if (config.pattern != null)
            {
                var regex = new Regex(config.pattern, RegexOptions.Multiline);
                value = regex.Match(value.Replace("\n", "")).Value;
            }

            switch (config.type)
            {
                case DECIMAL_S:
                    var decimalValue = string.IsNullOrWhiteSpace(value) ? (decimal?)null : decimal.Parse(value);
                    return decimalValue.HasValue ? (decimal?)Math.Round(decimalValue.Value, 2) : null;
                case STRING_S:
                    return value;
                case INT_S:
                    return string.IsNullOrWhiteSpace(value) ? (int?)null : int.Parse(value);
                default:
                    return null;
            }
        }

        protected abstract string GetValueImpl(TConfig config, TIn input);
    }
}
