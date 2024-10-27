using Game.GameModels.Models;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using static Game.Client.Services.GameMenyu;

namespace Game.Client
{
    internal class Client
    {
        static void Main(string[] args)
        {
            var ep = new IPEndPoint(IPAddress.Loopback, 27001);

            var client = new TcpClient();

            try
            {
                client.Connect(ep);

                if (client.Connected)
                {
                    Console.WriteLine("Welcome To Game.");

                    var stream = client.GetStream();

                    var buffer = new byte[1024];

                    var len = stream.Read(buffer);

                    var message = Encoding.UTF8.GetString(buffer, 0, len);

                    if (message == "first")
                    {
                        Console.WriteLine("Pess \"I\" to invite gamer.");
                        Console.WriteLine("Pess any button to exit.");

                        var key = Console.ReadKey();

                        switch (key.Key)
                        {
                            case ConsoleKey.I:
                                stream.Write(Encoding.UTF8.GetBytes("Yes"), 0, Encoding.UTF8.GetBytes("Yes").Length);

                                var buffer1 = new byte[1024];

                                var len1 = stream.Read(buffer1);

                                var confirm = Encoding.UTF8.GetString(buffer1, 0, len1);

                                if (confirm == "No")
                                {
                                    Console.WriteLine("client don't accept your invite.");

                                    Environment.Exit(0);
                                }

                                GamePlay(client, message);

                                break;
                            default:
                                break;
                        }
                    }
                    else if (message == "second")
                    {
                        Console.WriteLine("Waiting invite.");

                        var buffer1 = new byte[1024];

                        var len1 = stream.Read(buffer1);

                        var invite = Encoding.UTF8.GetString(buffer1, 0, len1);

                        if (invite == "invite")
                        {

                            Console.Clear();

                            Console.WriteLine("You are invited to game:");
                            Console.WriteLine("Pess Y to confirm:");
                            Console.WriteLine("Press any key to dimiss:");

                            var key1 = Console.ReadKey();

                            if (key1.Key == ConsoleKey.Y)
                            {
                                stream.Write(Encoding.UTF8.GetBytes("Yes"));

                                GamePlay(client, message);
                            }
                            else
                            {
                                stream.Write(Encoding.UTF8.GetBytes("No"));

                                Environment.Exit(0);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
