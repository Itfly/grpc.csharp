namespace PingClient
{
    using System;
    using System.IO;
    using Grpc.Core;
    using Ping.Proto;

    class Program
    {
        const int Port = 50051;
        const string CertPath = "../cert/";

        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            var cacert = File.ReadAllText(CertPath + @"ca.crt");
            var cert = File.ReadAllText(CertPath + @"client.crt");
            var key = File.ReadAllText(CertPath + @"client.key");
            var keypair = new KeyCertificatePair(cert, key);
            var creds = new SslCredentials(cacert, keypair);

            // The host value should be same with COMPUTERNAME in generate_crt_key.bat
            var channel = new Channel("localhost", Port, creds);
            var client = new PingClient(new PingApi.PingApiClient(channel));

            // Test echo
            client.Ping("sender1", 10L);
            client.Ping("sender2", 100L);
            client.Ping("sender3", 1000L);
            client.Ping("sender4", 10000L);
            client.Ping("sender5", 100000L);
            client.Ping("sender6", 1000000L);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
