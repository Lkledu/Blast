#if UNITY_WEBGL && UNITY_EDITOR
using System;
using System.Net;
using UnityEngine;
using WebSocketSharp.Server;
using Newtonsoft.Json.Linq;

namespace Phonion.Tiltspot
{

     [Serializable]
    public class WebServer
    {

        public WebSocketServer server;

        public WebServer(int port)
        {
            StartServer(port);
            
        }

        private void StartServer(int port)
        {
            server = new WebSocketServer(IPAddress.Parse("127.0.0.1"), port);
            server.AddWebSocketService<GameTesterHandler>("/GameTesterHandler");
            server.Start();
            Debug.Log("(Tiltspot) Ready to use Tiltspot Game Tester on port " + port);
        }

        public void SendMsgToGameTester(string unityMsg, int id, string msg, string data)
        {
            foreach (WebSocketServiceHost h in server.WebSocketServices.Hosts)
            {
                if (h.Path == "/GameTesterHandler")
                {
                    JObject o = new JObject
                    {
                        { "unityMsg", unityMsg },
                        { "id", id },
                        { "msg", msg },
                        { "data", data }
                    };
                    h.Sessions.Broadcast(o.ToString());
                }

            }
        }
    }
}
#endif