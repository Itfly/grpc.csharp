using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Common
{
    public static class Constant
    {
        public const string RequestIdName = "x-ms-rpc-request-id";
        public const string CorrelationIdName = "x-ms-rpc-correlation-id";
        public const string VersionName = "x-ms-rpc-version";
    }
}
