﻿using System;
using System.Collections.Generic;
using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    public interface IMessageRepository
    {
        Message GetMessage(string id);
        IEnumerable<Message> GetMessages();
        IEnumerable<Message> GetMessages(DateTime since);
        IEnumerable<Message> GetMessages(int maximumNumberOfMessages);
        IEnumerable<Message> GetMessages(Contact contact);
        IEnumerable<Message> GetMessages(Contact contact, DateTime since);
        IEnumerable<Message> GetMessages(Contact contact, int maximumNumberOfMessages);
        Message GetLastMessage(Contact contact);
        
        void DeleteMessages(IEnumerable<Message> message);

        void AddMessage(Message message);
    }
}