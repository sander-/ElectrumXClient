using System;
using System.Collections.Generic;
using System.Text;

namespace ElectrumXClient.Request
{
    public class VersionRequest : RequestBase
    {
        public VersionRequest() : base()
        {
            base.Method = "server.version";
            base.Parameters = new string[0];
        }
    }
}
