using Dummy;
using Greet;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        const string target = "127.0.0.1:50051";
        static void Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);
            channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("Client Connected Successfully");
                }

            });

            //var client = new DummyService.DummyServiceClient(channel);
            var client = new GreetingService.GreetingServiceClient(channel);

            var greeting = new Greeting()
            {
                FirstName = "Mahfuz",
                LastName = "Shazol"
            };

            var request = new GreetingRequest() { Greeting = greeting };
            var resonse = client.Greet(request);
            Console.WriteLine(resonse.Result);

            channel.ShutdownAsync().Wait();
            Console.ReadLine();
        }
    }
}
