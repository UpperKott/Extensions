using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Mapping.Constants;
using Mapping.Interfaces;

namespace Mapping
{
    public abstract class ConfigurableMapperBase<TIn, TType, TProp, TOut>
        where TType : TypeConversionConfig
        where TProp : PropertyMapperConfig
        where TOut : new()
    {
        protected IMapperConfig<TType, TProp> MapperConfig;

        public ConfigurableMapperBase(IMapperConfig<TType, TProp> mapperConfig)
        {
            MapperConfig = mapperConfig;
        }

        public TOut Map(TIn input)
        {
            var mapped = new TOut();

            foreach (var itemConfig in MapperConfig.Properties)
            {
                var targetProperty = mapped.GetType().GetProperty(itemConfig.Source);
                if (targetProperty == null) continue;

                targetProperty.SetValue(mapped, GetValue(targetProperty, itemConfig, input));
            }

            return mapped;
        }

        private object GetValue(PropertyInfo targetProperty, TProp config, TIn input)
        {
            var value = GetValueImpl(config, input);

            if (value != null && config.Pattern != null)
            {
                var regex = new Regex(config.Pattern, RegexOptions.Multiline);
                value = regex.Match(value.Replace("\n", "")).Value;
            }

            if (string.IsNullOrEmpty(config.Type))
            {
                return ParseByTargetPropertyType(targetProperty, value);
            }
            else
            {
                return ParseByConfigType(targetProperty.PropertyType, config, value);
            }
            
        }

        private static object ParseByTargetPropertyType(PropertyInfo targetProperty, string value)
        {
            if (targetProperty.PropertyType == typeof(decimal) || targetProperty.PropertyType == typeof(decimal?))
            {
                return decimal.TryParse(value, out var decimalValue)
                    ? Math.Round(decimalValue, 2)
                    : GetDefaultValue(targetProperty.PropertyType);
            }

            if (targetProperty.PropertyType == typeof(int) || targetProperty.PropertyType == typeof(int?))
            {
                return int.TryParse(value, out var intValue)
                    ? intValue
                    : GetDefaultValue(targetProperty.PropertyType);
            }

            if (targetProperty.PropertyType == typeof(bool) || targetProperty.PropertyType == typeof(bool?))
            {
                return bool.TryParse(value, out var intValue)
                    ? intValue
                    : GetDefaultValue(targetProperty.PropertyType);
            }

            if (targetProperty.PropertyType == typeof(string))
            {
                return value;
            }

            return GetDefaultValue(targetProperty.PropertyType);
        }

        private static object ParseByConfigType(Type targetPropertyType, TProp config, string value)
        {
            switch (config.Type)
            {
                case StandartTypes.DECIMAL_S:
                    return string.IsNullOrWhiteSpace(value) ? GetDefaultValue(targetPropertyType) : decimal.Parse(value);
                case StandartTypes.INT_S:
                    return string.IsNullOrWhiteSpace(value) ? GetDefaultValue(targetPropertyType) : int.Parse(value);
                case StandartTypes.BOOL_S:
                    return string.IsNullOrWhiteSpace(value) ? GetDefaultValue(targetPropertyType) : bool.Parse(value);
                case StandartTypes.STRING_S:
                    return value;
                default:
                    return null;
            }
        }

        private static object GetDefaultValue(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        protected abstract string GetValueImpl(TProp config, TIn input);
    }
}
