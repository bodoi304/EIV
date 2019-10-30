using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Untilities.Common
{
   public class CustomFormatDateTimeConverter : DateTimeConverterBase
    {
        //public CustomFormatDateTimeConverter()
        //{
        //    if (base.ReadJson.ToString())
        //    {

        //    }
        //    base.DateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        //}
        /// <summary>
        /// DateTime format
        /// </summary>
        private const string FormatDate = "dd/MM/yyyy";
        private const string FormatDateTime = "dd/MM/yyyy HH:mm:ss";
        /// <summary>
        /// Writes value to JSON
        /// </summary>
        /// <param name="writer">JSON writer</param>
        /// <param name="value">Value to be written</param>
        /// <param name="serializer">JSON serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString(FormatDate));
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
            if (reader.Value == null)
            {
                return null;
            }

            var s = reader.Value.ToString();
            DateTime result;
            if (DateTime.TryParseExact(s, FormatDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            if (DateTime.TryParseExact(s, FormatDateTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            return null ;
        }
    }
}
