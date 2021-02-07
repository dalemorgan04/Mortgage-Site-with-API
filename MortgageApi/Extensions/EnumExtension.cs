using System;
using System.ComponentModel;
using System.Reflection;

namespace MortgageApi.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum genericEnum)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = genericEnum.GetType().GetField(genericEnum.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return genericEnum.ToString();
        }
    }
}