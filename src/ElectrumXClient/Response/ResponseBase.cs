using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ElectrumXClient.Response
{
    [DataContract]
    public class ResponseBase
    {
        [DataMember(Name = "jsonrpc")]
        protected string JsonRpcVersion { get; set; }
        [DataMember(Name = "id")]
        protected int MessageId { get; set; }
        [DataMember(Name = "result")]
        protected string[] Result { get; set; }

        protected ResponseBase(string message)
        {
            var response = Deserialize<ResponseBase>(message);
            this.JsonRpcVersion = response.JsonRpcVersion;
            this.MessageId = response.MessageId;
            this.Result = response.Result;
        }

        protected static T Deserialize<T>(string json)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
