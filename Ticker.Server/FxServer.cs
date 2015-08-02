using System;
using System.Threading;
using System.Threading.Tasks;
using QuickFix;
using QuickFix.Fields;

namespace Ticker.Server
{
    internal class FxServer : IApplication
    {
        public static SessionID ActiveSessionID;

        public void ToAdmin(Message message, SessionID sessionID)
        {
            Console.WriteLine("ToAdmin: " + message);
        }

        public void FromAdmin(Message message, SessionID sessionID)
        {
            Console.WriteLine("FromAdmin: " + message);
        }

        public void ToApp(Message message, SessionID sessionId)
        {
            Console.WriteLine("OUT: " + message);
        }

        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("IN:  " + message);
        }

        public void OnCreate(SessionID sessionID) { }

        public void OnLogout(SessionID sessionID)
        {
            ActiveSessionID = null;
        }

        public void OnLogon(SessionID sessionID)
        {
            ActiveSessionID = sessionID;

            Task.Factory.StartNew(Send);
        }

        public static void Send()
        {
            var random = new Random();

            while (true)
            {
                Thread.Sleep(3000);

                if (ActiveSessionID != null)
                {
                    var message = new Message();
                    message.SetField(new DecimalField(1, (decimal) random.NextDouble()));
                    message.SetField(new DecimalField(2, (decimal)random.NextDouble()));
                    message.SetField(new StringField(55, "EURUSD"));
                    message.Header.SetField(new MsgType("S"));
                    try
                    {
                        Session.SendToTarget(message, ActiveSessionID); 
                    }
                    catch (Exception)
                    { }
                    
                }
            }

        }
    }
}