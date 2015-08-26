using NativeWebView.HTML.DOM.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace NativeWebView.JSON
{
    public class JsonHelper
    {
        private static DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JsonData));
        private JsonData _data;

        public JsonHelper(String jsonText)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText)))
            {
                _data = (JsonData)serializer.ReadObject(stream);
            }
        }

        public JsonHelper(int command, string id, params string[] data)
        {
            _data = new JsonData(command, id);
            foreach (var d in data)
            {
                AddData(d);
            }
        }

        public void AddData(bool data)
        {
            _data.DATA.Add(data.ToString());
        }

        public void AddData(int data)
        {
            _data.DATA.Add(data.ToString());
        }
        public void AddData(double data)
        {
            _data.DATA.Add(data.ToString());
        }
        public void AddData(string data)
        {
            _data.DATA.Add(data);
        }

        public int Command
        {
            get
            {
                return _data.COMMAND;
            }
        }

        public String Id
        {
            get
            {
                return _data.ID;
            }
        }

        public List<String> Data
        {
            get
            {
                return _data.DATA;
            }
        }
        static object streamLocker = new object();
        public override String ToString()
        {
            lock (streamLocker)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    String reply = null;
                    serializer.WriteObject(stream, _data);
                    reply = Encoding.UTF8.GetString(stream.ToArray(), 0, (int)stream.Length);
                    if (String.IsNullOrWhiteSpace(reply))
                        throw new NullReferenceException("Unable to determine json data");
                    return reply;
                }
            }
        }
    }
}
