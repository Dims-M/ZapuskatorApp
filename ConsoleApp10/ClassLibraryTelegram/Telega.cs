using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core;

namespace ClassLibraryTelegram
{
  public  class Telega
    {
        /// <summary>
        /// ID нужного чата
        /// </summary>
        int VKFID = 1175259547; //
        int apiId = 0;
        string apiHash = "";
        int offset = 0;
        int n = 1; //начало сообщений 
        int countMessa = 0 ; // Нужное количество сообщений

        StringBuilder sb = new StringBuilder();
        //TelegramClient client = new TelegramClient(<key> <hash>);
      //  TelegramClient client = new TelegramClient(apiId, apiHash);
        TLUser user;

        private DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public async Task<StringBuilder> GetChatTelegram()
        {
            TelegramClient client = new TelegramClient(apiId, apiHash);

            sb.Append("#\tDate\tTime\tMID\tTUID\tText" + Environment.NewLine);

            TLDialogsSlice dialogs = (TLDialogsSlice)await client.GetUserDialogsAsync(); // Получение списка чатов

            TLChannel chat = dialogs.Chats.Where(c => c.GetType() == typeof(TLChannel)).Cast<TLChannel>().FirstOrDefault(c => c.Id == VKFID);

            TLInputPeerChannel inputPeer = new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = (long)chat.AccessHash };


            while (countMessa == 100)
            {
                try
                {
                    TLChannelMessages res = await client.SendRequestAsync<TLChannelMessages>
                    (new TLRequestGetHistory() { Peer = inputPeer, Limit = 1000, AddOffset = offset, OffsetId = 0 });
                    var msgs = res.Messages;
                    if (res.Count > offset)
                    {
                        offset += msgs.Count;
                        foreach (TLAbsMessage msg in msgs)
                        {
                            if (msg is TLMessage)
                            {
                                TLMessage message = msg as TLMessage;
                                sb.Append(n.ToString() + "\t" +
                                    ConvertFromUnixTimestamp(message.Date).ToLocalTime().ToString("dd'.'MM'.'yyyy") + "\t" +
                                    ConvertFromUnixTimestamp(message.Date).ToLocalTime().ToString("HH':'mm':'ss") + "\t" +
                                    message.Id + "\t" + message.FromId + "\t" + message.Message + Environment.NewLine);
                            }
                            if (msg is TLMessageService)
                                continue;
                            n++;
                            countMessa++;
                        }
                        Thread.Sleep(22000); //to avoid TelegramFloodException
                    }
                    else
                        break;
                }
                catch (Exception ex)
                {
                    string tempEx = $"Ошибка при получении списка сособщения из чата {VKFID}"+ Environment.NewLine;
                    //MessageBox.Show(ex.Message);
                    break;
                }
                finally
                {
                    await Task.Delay(22000); //чтобы обойти TelegramFloodException
                }
            }
            return sb;
          //  textBox2.Text = sb.ToString();
           // MessageBox.Show("Done");
        }




    }
}
