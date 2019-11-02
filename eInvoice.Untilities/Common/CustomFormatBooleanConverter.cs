using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Untilities.Common
{
    public class CustomFormatBooleanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes value to JSON
        /// </summary>
        /// <param name="writer">JSON writer</param>
        /// <param name="value">Value to be written</param>
        /// <param name="serializer">JSON serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Boolean)value));
        }

        /// <summary>
        /// Reads value from JSON
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="objectType">Target type</param>
        /// <param name="existingValue">Existing value</param>
        /// <param name="serializer">JSON serialized</param>
        /// <returns>Deserialized DateTime</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value.ToString() == "0")
            {
                return false;
            }
            else if (reader.Value.ToString() == "1")
            {
                return true;
            }
            var s = reader.Value.ToString();
            Boolean result;
            if (Boolean.TryParse(s, out result))
            {
                return result;
            }
            return null;
        }
    }
}
