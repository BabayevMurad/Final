﻿using Game.GameModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game.Server.Models
{
    internal class Client
    {

        public TcpClient TcpClient { get; set; }

        public User User { get; set; }
        public Client(TcpClient client, User user) 
        { 
            TcpClient = client;
            User = user;
        }
    }
}
