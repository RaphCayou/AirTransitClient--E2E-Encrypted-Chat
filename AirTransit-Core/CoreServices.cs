﻿using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using AirTransit_Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AirTransit_Core
{
    public class CoreServices
    {
        public static string SERVER_ADDRESS = "servermartineau.com";

        public IContactRepository ContactRepository { get; set; }
        public IMessageRepository MessageRepository { get; set; }

        public IMessageService MessageService { get; set; }

        private IAuthenticationService AuthenticationService { get; set; }
        private BlockingCollection<string> BlockingCollection { get; set; }

        public CoreServices()
        {
            BlockingCollection = new BlockingCollection<string>();
        }

        public bool Init(string phoneNumber)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MessagingContext>();
            // TODO : Add connection to database
            optionsBuilder.UseSqlite("Data Source=airtransit.db");
            MessagingContext messagingContext = new MessagingContext(optionsBuilder.Options);

            // TODO : Does it really need ContactRepository?? Because it will be null here, it's going to be assigned later but it's not by reference (think not).
            AuthenticationService = new AuthenticationService(ContactRepository);

            KeySet keySet = AuthenticationService.SignUp(phoneNumber);

            if (keySet != null)
            {
                InitializeRepositories(messagingContext);
                InitializeServices(keySet);
            }

            return keySet != null;
        }

        public BlockingCollection<string> GetBlockingCollection()
        {
            return BlockingCollection;
        }

        private void InitializeRepositories(MessagingContext messagingContext)
        {
            ContactRepository = new EntityFrameworkContactRepository(messagingContext);
            MessageRepository = new EntityFrameworkMessageRepository(messagingContext);
        }

        private void InitializeServices(KeySet keySet)
        {
            MessageService = new MessageService(MessageRepository, keySet);
        }
    }
}
