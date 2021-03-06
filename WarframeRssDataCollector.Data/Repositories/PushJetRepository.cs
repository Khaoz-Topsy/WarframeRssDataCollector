﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WarframeRssDataCollector.Domain;

namespace WarframeRssDataCollector.Data.Repositories
{
    public class PushJetRepository
    {
        public async void SendMessage(PushJetMessageContent messageData)
        {
            await Task.Run(async () =>
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.pushjet.io/message");
                    
                    string stringData =
                        "secret=" + messageData.secret + "&" +
                        "message=" + messageData.message + "&" +
                        "title=" + messageData.title + "&" +
                        "level=" + messageData.level + "&" +
                        "link=" + messageData.link;

                    //TODO try dis
                    //string stringData = String.Format("secret={0}&message={1}&title={3}&level={4}&link={5}",
                    //    messageData.secret, messageData.message, messageData.title, messageData.level, messageData.link);

                    var data = Encoding.ASCII.GetBytes(stringData);

                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    httpWebRequest.ContentLength = data.Length;
                    
                    Stream newStream = await httpWebRequest.GetRequestStreamAsync();
                    newStream.Write(data, 0, data.Length);
                    newStream.Close();

                    WebResponse webResponse = await httpWebRequest.GetResponseAsync();
                }
                catch (Exception ex)
                { }
            });            
        }
    }
}
