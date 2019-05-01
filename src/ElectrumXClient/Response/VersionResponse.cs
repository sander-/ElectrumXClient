using System;
using System.Collections.Generic;
using System.Text;

namespace ElectrumXClient.Response
{
    public class VersionResponse : ResponseBase
    {
        public VersionResponse(string message): base(message)
        {
        }
    }
}
