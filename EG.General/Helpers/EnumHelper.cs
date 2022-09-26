using EG.General.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG.General.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            
            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }

        public static int GetEnumValue<T>(this T enumValue) where T : struct,IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new Exception("value is not enum Type");

            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            return (int)fieldInfo.GetValue(null);
        }

        public static Enum GetEnumByName(this string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("name is null");

            switch (name)
            {
                case "USDT":
                    return TokenTypeEnum.USDT;
                case "BitCoin":
                    return CoinTypeEnum.BitCoin;
                case "Tron":
                    return CoinTypeEnum.Tron;
                default:
                    throw new Exception("notfound enum");
            }
        }
    }
}
