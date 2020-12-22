using AcademyBank.Models.Enums;
using AcademyBank.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AcademyBank.BLL.Helpers.Implementations
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(string value, Type type)
        {
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

        public static List<string> GetValues(this Type type)
        {
            List<string> descriptions = new List<string>();
            string description = "";
            var arr = type.GetEnumNames();

            foreach(var el in arr)
            {
                description = GetEnumDescription(el, type);
                descriptions.Add(description);
            }

            return descriptions;
        }

        public static List<string> GetAllEnumNames(this Type type)
        {
            List<string> list = new List<string>();

            var arr = type.GetEnumNames();

            foreach(var el in arr)
            {
                list.Add(el);
            }

            return list;
        }

        public static List<int> GetAllEnumValues(this Type type)
        {
            List<int> list = new List<int>();

            var arr = (int [])type.GetEnumValues();

            foreach (var el in arr)
            {
                list.Add(el);
            }

            return list;
        }
    }
}
