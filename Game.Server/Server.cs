using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text;
using Game.Server.Models;
using Game.GameModels.Models;

namespace Game.Server
{
    internal class Server
    {
        static TcpClient firstClient;

        static TcpClient secondClient;

        static void Main(string[] args)
        {
            var ep = new IPEndPoint(IPAddress.Loopback, 27001);

            var tcpListener = new TcpListener(ep);


            tcpListener.Start();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Server.");
            Console.ResetColor();

            try
            {

                var clients = new List<TcpClient>();

                while (true)
                {
                    var client = tcpListener.AcceptTcpClient();

                    clients.Add(client);

                    if (clients.Count == 1)
                    {
                        firstClient = client;
                    }
                    else
                    {
                        secondClient = client;

                        firstClient.GetStream().Write(Encoding.UTF8.GetBytes("first"));

                        secondClient.GetStream().Write(Encoding.UTF8.GetBytes("second"));
                    }

                    _ = Task.Run(() =>
                    {
                        if (client == firstClient && clients.Count == 2)
                        {
                            var buffer = new byte[1024];

                            var firstClientStream = firstClient.GetStream();

                            firstClientStream.Read(buffer, 0, buffer.Length);

                            var invite = Encoding.UTF8.GetString(buffer);

                            var invite1 = invite.Split('\0')[0];

                            if (invite1 == "Yes")
                            {
                                var secondClientStream = secondClient.GetStream();

                                secondClientStream.Write(Encoding.UTF8.GetBytes("invite"));

                                var buffer1 = new byte[1024];

                                var len1 = secondClientStream.Read(buffer1);

                                var confirmSecondClient = Encoding.UTF8.GetString(buffer1, 0, len1);

                                if (confirmSecondClient == "Yes")
                                {
                                    firstClientStream.Write(Encoding.UTF8.GetBytes("Yes"));

                                    while (true)
                                    {
                                        var buffer2 = new byte[1024];

                                        var len2 = firstClientStream.Read(buffer2);

                                        if (Encoding.UTF8.GetString(buffer2, 0, len2) == "win")
                                        {
                                            secondClientStream.Write(Encoding.UTF8.GetBytes("lose"));

                                            Environment.Exit(0);
                                        }

                                        secondClientStream.Write(buffer2, 0, len2);

                                        var buffer3 = new byte[1024];

                                        var len3 = secondClientStream.Read(buffer3);

                                        if (Encoding.UTF8.GetString(buffer3, 0, len3) == "win")
                                        {
                                            firstClientStream.Write(Encoding.UTF8.GetBytes("lose"));

                                            Environment.Exit(0);
                                        }

                                        firstClientStream.Write(buffer3, 0, len3);
                                    }

                                }
                                else
                                {
                                    firstClientStream.Write(Encoding.UTF8.GetBytes("No"));

                                    Environment.Exit(0);
                                }

                            }
                        }
                        else if (client == secondClient && clients.Count == 2)
                        {
                            var buffer = new byte[1024];

                            var firstClientStream = firstClient.GetStream();

                            firstClientStream.Read(buffer);

                            var invite = Encoding.UTF8.GetString(buffer);

                            var invite1 = invite.Split('\0')[0];

                            if (invite1 == "Yes")
                            {
                                var secondClientStream = secondClient.GetStream();

                                secondClientStream.Write(Encoding.UTF8.GetBytes("invite"));

                                var buffer1 = new byte[1024];

                                var len1 = secondClientStream.Read(buffer1);

                                var confirmSecondClient = Encoding.UTF8.GetString(buffer1, 0, len1);

                                if (confirmSecondClient == "Yes")
                                {
                                    firstClientStream.Write(Encoding.UTF8.GetBytes("Yes"));

                                    while (true)
                                    {
                                        var buffer2 = new byte[1024];

                                        var len2 = firstClientStream.Read(buffer2);

                                        if (Encoding.UTF8.GetString(buffer2, 0, len2) == "win")
                                        {
                                            secondClientStream.Write(Encoding.UTF8.GetBytes("lose"));

                                            Environment.Exit(0);
                                        }
                                        else if (Encoding.UTF8.GetString(buffer2, 0, len2) == "equal")
                                        {
                                            secondClientStream.Write(Encoding.UTF8.GetBytes("equal"));

                                            Environment.Exit(0);
                                        }

                                        secondClientStream.Write(buffer2, 0, len2);

                                        var buffer3 = new byte[1024];

                                        var len3 = secondClientStream.Read(buffer3);

                                        if (Encoding.UTF8.GetString(buffer3, 0, len3) == "win")
                                        {
                                            firstClientStream.Write(Encoding.UTF8.GetBytes("lose"));

                                            Environment.Exit(0);
                                        }
                                        else if (Encoding.UTF8.GetString(buffer3, 0, len3) == "equal")
                                        {
                                            firstClientStream.Write(Encoding.UTF8.GetBytes("equal"));

                                            Environment.Exit(0);
                                        }

                                        firstClientStream.Write(buffer3, 0, len3);
                                    }
                                }
                                else
                                {
                                    firstClientStream.Write(Encoding.UTF8.GetBytes("No"));
                                }

                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
