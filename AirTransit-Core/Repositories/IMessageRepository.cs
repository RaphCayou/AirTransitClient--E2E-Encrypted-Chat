﻿using System;
using System.Collections.Generic;
using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    public interface IMessageRepository
    {
        Message SendMessage(String phoneNumber, String message);
        
        IEnumerable<Message> GetMessages();
        IEnumerable<Message> GetMessages(DateTime since);
        IEnumerable<Message> GetMessages(int maximumNumberOfMessages);
        IEnumerable<Message> GetMessages(Contact contact);
        IEnumerable<Message> GetMessages(Contact contact, DateTime since);
        IEnumerable<Message> GetMessages(Contact contact, int maximumNumberOfMessages);
        IEnumerable<Message> GetLastMessagesOfContacts(IEnumerable<Contact> contacts);
        
        bool DeleteMessages(List<String> messageIDs);
    }
}