using Game.GameModels.Models;
using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace Game.Client.Services
{
    internal static class GameMenyu
    {
        static void GameDataSender(TcpClient tcpClient, SignIndex index, int isWin)
        {
            var stream = tcpClient.GetStream();

            if (isWin == 0)
            {
                stream.Write(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(index)));
            }
            else if (isWin == 1)
            {
                stream.Write(Encoding.UTF8.GetBytes("equal"));

                Console.Clear();

                Console.WriteLine("Game Ended Equal:");

                Environment.Exit(0);
            }
            else if (isWin == 2)
            {
                stream.Write(Encoding.UTF8.GetBytes("win"));

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("You Win Game:");

                Environment.Exit(0);
            }
        }

        static SignIndex ReadData(TcpClient tcpClient)
        {
            var stream = tcpClient.GetStream();

            var buffer = new byte[1024];

            var len = stream.Read(buffer, 0, buffer.Length);

            var json = Encoding.UTF8.GetString(buffer, 0, len);

            if (json == "lose")
            {
                Console.Clear();

                Console.WriteLine("You Lose Game");

                Environment.Exit(0);
            }
            else if (json == "equal")
            {
                Console.Clear();

                Console.WriteLine("Game Ended Equal:");

                Environment.Exit(0);
            }

            var index = JsonSerializer.Deserialize<SignIndex>(json);

            return index;
        }

        static int Diagram(Sign[,] matix, int lenth)
        {
            var number = 0;

            var numberNull = 0;

            for (int i = 0; i < lenth; i++)
            {
                Console.Write("\t\t\t");

                for (int j = 0; j < lenth; j++)
                {
                    if (matix[i, j] is null)
                    {
                        Console.Write(++number);

                        numberNull++;

                        if (j != lenth - 1)
                        {
                            Console.Write("|");
                        }

                    }
                    else
                    {
                        ++number;

                        if (matix[i, j].SignEnum == SignEnum.X)
                        {
                            Console.Write("X");
                        }
                        else
                        {
                            Console.Write("O");
                        }

                        if (j != lenth - 1)
                        {
                            Console.Write("|");
                        }
                    }
                }

                Console.WriteLine();

            }

            return numberNull;
        }

        public static void GamePlay(TcpClient tcpClient, string message)
        {
            Console.Clear();

            Sign sign;

            if (message == "first")
            {
                sign = new Sign(SignEnum.X, 0);
            }
            else
            {
                sign = new Sign(SignEnum.O, 1);
            }

            var lenth = 3;

            Sign[,] matix = new Sign[lenth, lenth];


            bool IsFirst = true;

            bool back = false;

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Game:");

                SignIndex indexGet;

                if (message == "first")
                {
                    Console.WriteLine("You play X:");

                    if (!IsFirst && !back)
                    {
                        Diagram(matix, lenth);

                        Console.WriteLine("Pleace Wait Player.");

                        indexGet = ReadData(tcpClient);

                        matix[indexGet.i, indexGet.j] = new Sign(SignEnum.O, 1);

                        Console.Clear();

                        Console.WriteLine("You play X:");
                    }
                    else if (back)
                    {
                        Console.Clear();

                        Console.WriteLine("You play X:");
                    }

                }
                else
                {

                    if (!back)
                    {

                        Console.WriteLine("You play O:");

                        Diagram(matix, lenth);

                        Console.WriteLine("Pleace Wait Player.");

                        indexGet = ReadData(tcpClient);

                        matix[indexGet.i, indexGet.j] = new Sign(SignEnum.X, 0);

                        Console.Clear();

                        Console.WriteLine("You play O:");
                    }
                    else if (back)
                    {
                        Console.Clear();

                        Console.WriteLine("You play O:");
                    }
                }

                Diagram(matix, lenth);

                Console.WriteLine("Input number witch you see in diagram where you put sign:");

                var key = Console.ReadKey();

                SignIndex? index = null;

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        if (matix[0, 0] is null)
                        {
                            matix[0, 0] = sign;

                            index = new SignIndex(0, 0, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.D2:
                        if (matix[0, 1] is null)
                        {
                            matix[0, 1] = sign;

                            index = new SignIndex(0, 1, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.D3:
                        if (matix[0, 2] is null)
                        {
                            matix[0, 2] = sign;

                            index = new SignIndex(0, 2, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.D4:
                        if (matix[1, 0] is null)
                        {
                            matix[1, 0] = sign;

                            index = new SignIndex(1, 0, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.D5:
                        if (matix[1, 1] is null)
                        {
                            matix[1, 1] = sign;

                            index = new SignIndex(1, 1, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.D6:
                        if (matix[1, 2] is null)
                        {
                            matix[1, 2] = sign;

                            index = new SignIndex(1, 2, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.D7:
                        if (matix[2, 0] is null)
                        {
                            matix[2, 0] = sign;

                            index = new SignIndex(2, 0, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.D8:
                        if (matix[2, 1] is null)
                        {
                            matix[2, 1] = sign;

                            index = new SignIndex(2, 1, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.D9:
                        if (matix[2, 2] is null)
                        {
                            matix[2, 2] = sign;

                            index = new SignIndex(2, 2, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.NumPad1:
                        if (matix[0, 0] is null)
                        {
                            matix[0, 0] = sign;

                            index = new SignIndex(0, 0, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.NumPad2:
                        if (matix[0, 1] is null)
                        {
                            matix[0, 1] = sign;

                            index = new SignIndex(0, 1, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.NumPad3:
                        if (matix[0, 2] is null)
                        {
                            matix[0, 2] = sign;

                            index = new SignIndex(0, 2, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.NumPad4:
                        if (matix[1, 0] is null)
                        {
                            matix[1, 0] = sign;

                            index = new SignIndex(1, 0, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.NumPad5:
                        if (matix[1, 1] is null)
                        {
                            matix[1, 1] = sign;

                            index = new SignIndex(1, 1, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.NumPad6:
                        if (matix[1, 2] is null)
                        {
                            matix[1, 2] = sign;

                            index = new SignIndex(1, 2, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.NumPad7:
                        if (matix[2, 0] is null)
                        {
                            matix[2, 0] = sign;

                            index = new SignIndex(2, 0, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.NumPad8:
                        if (matix[2, 1] is null)
                        {
                            matix[2, 1] = sign;

                            index = new SignIndex(2, 1, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    case ConsoleKey.NumPad9:
                        if (matix[2, 2] is null)
                        {
                            matix[2, 2] = sign;

                            index = new SignIndex(2, 2, sign.SignEnum);

                            if (IsFirst)
                                IsFirst = false;
                        }
                        else
                        {
                            Console.WriteLine("There is a sign you can't change them.");

                            back = true;

                            continue;
                        }
                        break;
                    default:
                        Console.WriteLine("Pleace input correctly.");

                        back = true;

                        Thread.Sleep(1000);
                        continue;
                }

                try
                {
                    if (matix[0, 0].SignEnum == sign.SignEnum && matix[0, 1].SignEnum == sign.SignEnum && matix[0, 2].SignEnum == sign.SignEnum)
                    {
                        GameDataSender(tcpClient, index!, 2);
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (matix[1, 0].SignEnum == sign.SignEnum && matix[1, 1].SignEnum == sign.SignEnum && matix[1, 2].SignEnum == sign.SignEnum)
                    {
                        GameDataSender(tcpClient, index!, 2);
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (matix[2, 0].SignEnum == sign.SignEnum && matix[2, 1].SignEnum == sign.SignEnum && matix[2, 2].SignEnum == sign.SignEnum)
                    {
                        GameDataSender(tcpClient, index!, 2);
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (matix[0, 0].SignEnum == sign.SignEnum && matix[1, 0].SignEnum == sign.SignEnum && matix[2, 0].SignEnum == sign.SignEnum)
                    {
                        GameDataSender(tcpClient, index!, 2);
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (matix[2, 0].SignEnum == sign.SignEnum && matix[1, 1].SignEnum == sign.SignEnum && matix[0, 2].SignEnum == sign.SignEnum)
                    {
                        GameDataSender(tcpClient, index!, 2);
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (matix[0, 1].SignEnum == sign.SignEnum && matix[1, 1].SignEnum == sign.SignEnum && matix[2, 1].SignEnum == sign.SignEnum)
                    {
                        GameDataSender(tcpClient, index!, 2);
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (matix[0, 2].SignEnum == sign.SignEnum && matix[1, 2].SignEnum == sign.SignEnum && matix[2, 2].SignEnum == sign.SignEnum)
                    {
                        GameDataSender(tcpClient, index!, 2);
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (matix[0, 0].SignEnum == sign.SignEnum && matix[1, 1].SignEnum == sign.SignEnum && matix[2, 2].SignEnum == sign.SignEnum)
                    {
                        GameDataSender(tcpClient, index!, 2);
                    }
                }
                catch (Exception)
                {
                }

                var free = 0;

                for (int i = 0; i < lenth; i++)
                {
                    for (int j = 0; j < lenth; j++)
                    {
                        if (matix[i, j] is null)
                        {
                            free++;
                        }
                    }
                }

                if (free == 0)
                {
                    GameDataSender(tcpClient, index!, 1);
                }

                if (index is not null)
                {
                    GameDataSender(tcpClient, index!, 0);
                }

            }
        }
    }
}
