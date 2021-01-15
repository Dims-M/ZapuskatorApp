using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core;
using System.IO;
using TLSharp.Core.Exceptions;

namespace ClassLibraryTelegram
{
  public  class Telega
    {
        /// <summary>
        /// ID нужного чата
        /// </summary>
        int VKFID = XXX; //
        int apiId = XXX;

        string apiHash = "XXXXXX";
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
            try
            {
                TelegramClient client = new TelegramClient(apiId, apiHash);
                await client.ConnectAsync();

                //var hash = await client.SendCodeRequestAsync("+XXXX);

                //string stop = "";
                //var code = "71486";

                //var user = await client.MakeAuthAsync("+XXXX", hash, code);

                // var temp =  ConfigurationManager.AppSettings[nameof(ApiHash)];

                sb.Append("#\tDate\tTime\tMID\tTUID\tText" + Environment.NewLine);

                TLDialogsSlice dialogs = (TLDialogsSlice)await client.GetUserDialogsAsync(); // Получение списка чатов

                //string temp = "";
                //for (int i = 0; i < dialogs.Count; i++)
                //{
                //    temp += dialogs.Chats.list.ToString() + Environment.NewLine;
                //}
                //SaveTextFile(temp);

                TLChannel chat = dialogs.Chats.Where(c => c.GetType() == typeof(TLChannel)).Cast<TLChannel>().FirstOrDefault(c => c.Id == VKFID);

                // TLInputPeerChannel inputPeer = new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = (long)chat.AccessHash };


                while (countMessa != 100)
                {
                    try
                    {
                        TLChannelMessages res = await client.SendRequestAsync<TLChannelMessages>
                        (new TLRequestGetHistory() { /*Peer = inputPeer,*/ Limit = 1000, AddOffset = offset, OffsetId = 0 });

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
                            SaveTextFileBuilder(sb, "Messages.txt");
                        }
                        else
                            break;
                    }
                    catch (Exception ex)
                    {
                        string tempEx = $"Ошибка при получении списка сособщения из чата {VKFID}" + Environment.NewLine + ex;
                        SaveTextFile(tempEx);
                        break;
                    }
                    finally
                    {
                      //  await Task.Delay(22000); //чтобы обойти TelegramFloodException
                    }
                }
            }
            //  textBox2.Text = sb.ToString();
            // MessageBox.Show("Done");

            catch (Exception ex)
            {
                string tempEx = $"Ошибка при получении списка сособщения из чата {VKFID}" + Environment.NewLine + ex;
                SaveTextFile(tempEx);
            }

            finally
            {

               // SaveTextFileBuilder(sb, "Messages.txt");
               // return sb;
            }
            return sb;

        }


        //public virtual async Task AuthUser()
        //{
        //    var client = NewClient();

        //    await client.ConnectAsync();

        //    var hash = await client.SendCodeRequestAsync(NumberToAuthenticate);
        //    var code = CodeToAuthenticate; // you can change code in debugger too

        //    if (String.IsNullOrWhiteSpace(code))
        //    {
        //        throw new Exception("CodeToAuthenticate is empty in the app.config file, fill it with the code you just got now by SMS/Telegram");
        //    }

        //    TLUser user = null;
        //    try
        //    {
        //        user = await client.MakeAuthAsync(NumberToAuthenticate, hash, code);
        //    }
        //    catch (CloudPasswordNeededException ex)
        //    {
        //        var passwordSetting = await client.GetPasswordSetting();
        //        var password = PasswordToAuthenticate;

        //        user = await client.MakeAuthWithPasswordAsync(passwordSetting, password);
        //    }
        //    catch (InvalidPhoneCodeException ex)
        //    {
        //        throw new Exception("CodeToAuthenticate is wrong in the app.config file, fill it with the code you just got now by SMS/Telegram",
        //                            ex);
        //    }
        //    Assert.IsNotNull(user);
        //    Assert.IsTrue(client.IsUserAuthorized());
        //}


        public void SaveTextFile(string text, string path= @"LogError.txt")
        {
            string writePath = path;
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }   
            }
            catch (Exception e)
            {
              //  Console.WriteLine(e.Message);
            }
        }

        public void SaveTextFileBuilder(StringBuilder text, string path = @"LogTelegram.txt")
        {
            string writePath = path;
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
            catch (Exception e)
            {
                //  Console.WriteLine(e.Message);
            }
        }

    }

    //class Assert
    //{
    //    static internal void IsNotNull(object obj)
    //    {
    //        IsNotNullHanlder(obj);
    //    }

    //    static internal void IsTrue(bool cond)
    //    {
    //        IsTrueHandler(cond);
    //    }
    //}
}
