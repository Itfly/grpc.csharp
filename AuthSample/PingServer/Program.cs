namespace PingServer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Grpc.Core;
    using Ping.Proto;

    class Program
    {
        const int Port = 50051;
        const string CertPath = "../cert/";

        static void Main(string[] args)
        {
            var cacert = File.ReadAllText(CertPath + @"ca.crt");
            var cert = File.ReadAllText(CertPath + @"server.crt");
            var key = File.ReadAllText(CertPath + @"server.key");
            var keypair = new KeyCertificatePair(cert, key);
            var sslCredentials = new SslServerCredentials(new List<KeyCertificatePair>() { keypair }, cacert, false);

            var server = new Server
            {
                Services = { PingApi.BindService(new PingServiceImpl()) },
                Ports = { new ServerPort("0.0.0.0", Port, sslCredentials) }
            };
            server.Start();

            Console.WriteLine("Starting server on port " + Port);
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
