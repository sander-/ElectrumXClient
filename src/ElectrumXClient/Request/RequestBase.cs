using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ElectrumXClient.Request
{
    [DataContract]
    public class RequestBase
    {
        [DataMember(Name = "id")]
        public int MessageId { get; set; }
        [DataMember(Name = "method")]
        public string Method { get; set; }
        [DataMember(Name = "params")]
        public string[] Parameters { get; set; }

        public byte[] GetRequestData<T>()
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(Serialize<T>());
            return data;
        }

        protected string Serialize<T>()
        {
            using (var ms = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(ms, this);
                return Encoding.ASCII.GetString(ms.ToArray()) + "\n";
            }
        }
    }
}
