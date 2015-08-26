using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NativeWebView.JSON
{
    [DataContract]
    internal class JsonData
    {
        public const String JSON_COMMAND_KEY = "COMMAND";
        public const String JSON_ID_KEY = "ID";
        public const String JSON_DATA_KEY = "DATA";

        public JsonData()
        {
            COMMAND = 0;
            ID = String.Empty;
            DATA = new List<String>();
        }
        public JsonData(int command, string id)
            : this()
        {
            COMMAND = command;
            ID = id;
        }
        
        [DataMember(Name = "COMMAND", IsRequired = true)]
        public int COMMAND { get; set; }
        [DataMember(Name = "ID", IsRequired = true)]
        public String ID { get; set; }
        [DataMember(Name = "DATA", IsRequired = true)]
        public List<String> DATA { get; set; }
    }
}
