using System;
using System.Reflection;

namespace Garment.Web.Common
{
    public class Utilities
    {
        public static bool IsNullOrDefault<T>(T argument)
        {
            if (argument == null) return true;
            if (argument is ValueType)
            {
                return Equals(argument, default(T));
            }
            
            foreach (PropertyInfo pi in argument.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if(pi.PropertyType == typeof(string))
                {
                    string pValue = (string)pi.GetValue(argument, null);
                    if (!IsNullOrDefault(pValue))
                        return false;
                }
                else if (pi.PropertyType == typeof(int))
                {
                    int pValue = (int)pi.GetValue(argument, null);
                    if (!IsNullOrDefault(pValue))
                        return false;
                }

                else if (pi.PropertyType == typeof(float))
                {
                    float pValue = (float)pi.GetValue(argument, null);
                    if (!IsNullOrDefault(pValue))
                        return false;
                }
                else if (pi.PropertyType == typeof(DateTime))
                {
                    DateTime pValue = (DateTime)pi.GetValue(argument, null);
                    if (!IsNullOrDefault(pValue))
                        return false;
                }
            }

            return true;
        }
    }
}