﻿using clientXamarin.Models;
using clientXamarin.Services;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace clientXamarin.ViewModels.MainContentViewModel
{
    public class ChatRoomViewModel : BaseViewModel, IDisposable
    {
        private string _message = string.Empty;

        public string Message { get => _message; set => SetProperty(ref _message,value); }

        private ObservableCollection<ChatMessage> _messages;
        public ObservableCollection<ChatMessage> Messages { get=> _messages; set => SetProperty(ref _messages,value); }

        private IDisposable _messageSubscription;

        private string _otherUsername;

        public string OtherUsername { get => _otherUsername; set => SetProperty(ref _otherUsername, value); }

        public string Title { get => "Chatting with " + OtherUsername; }

        public ChatRoomViewModel(string otherUsername)
        {
            _otherUsername = otherUsername;
            Connect();
            InitializeCommand();
            Messages = new ObservableCollection<ChatMessage>();
        }

        public ICommand SendMessageCommand { get; private set; }

        private void InitializeCommand()
        {
            SendMessageCommand = new Command(() => SendMessage());
        }

        public void Init()
        {
            _messageSubscription = ChatService.NewMeussageReceived.Subscribe(GetMessages);
        }

        private void GetMessages(IEnumerable<ChatMessage> messages)
        {

            foreach (var message in messages)
            {
                Messages.Add(message);

            }
            foreach (var item in Messages)
            {
                Debug.WriteLine(item.Content);

            }
        }

        private async void Connect()
        {
            await ChatService.Connect(_otherUsername);
            Messages = ChatService._chats;

     
        }

        private async void SendMessage()
        {
            await ChatService.SendMessage(_otherUsername, _message);
            //set message to empty after send
            Message = "";
        }

        public void Dispose()
        {
            _messageSubscription.Dispose();
        }
    }
}
