using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class UniquePropertyDetailsBuilder
    {
        public Dictionary<string, string> GetPropertyMessage(Vehicle i_Vehicle)
        {
            Dictionary<string, string> propertyNameMessageDictionary = new Dictionary<string, string>();
            Type type = i_Vehicle.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.DeclaringType == type)
                {
                    string propertyName = property.Name;
                    string inputMessage = generatePropertyPrompt(property); // Generate input messages for the property

                    propertyNameMessageDictionary[propertyName] = inputMessage;
                }
            }

            return propertyNameMessageDictionary;
        }

        private static string convertToSentenceCase(string i_Input)
        {
            StringBuilder result = new StringBuilder(i_Input.Length * 2);
            result.Append(char.ToUpper(i_Input[0]));

            for (int i = 1; i < i_Input.Length; i++)
            {
                result.Append(i_Input[i]);
            }

            return result.ToString();
        }

        private static string generatePropertyPrompt(PropertyInfo i_Property)
        {
            StringBuilder inputMessages = new StringBuilder($"Select {convertToSentenceCase(i_Property.Name)}\n");
            
            if (i_Property.PropertyType.IsEnum)
            {
                Type enumType = i_Property.PropertyType;
                foreach (var enumValue in Enum.GetValues(enumType))
                {
                    inputMessages.AppendLine($"- {(int)enumValue} {convertToSentenceCase(enumValue.ToString())}");
                }
            }
            else if(i_Property.PropertyType == typeof(bool)) 
            {
                inputMessages.AppendLine("Write 'true' or 'false':");
            }

            return inputMessages.ToString();
        }
    }
}