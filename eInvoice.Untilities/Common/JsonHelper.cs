using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Untilities.Common
{
    public class JsonHelper
    {
        public static IList<ValidationError> validateJsonString<T>(JObject jObject)
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(T));        
          
            // validate json
            IList<ValidationError> errors;
            bool valid = jObject.IsValid(schema, out errors);

            return errors;
        }
    }
}
