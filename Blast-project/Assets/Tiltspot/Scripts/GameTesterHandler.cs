#if UNITY_WEBGL && UNITY_EDITOR
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json.Linq;

namespace Phonion.Tiltspot
{
    public class GameTesterHandler : WebSocketBehavior {

        public static JToken info = null;

        protected override void OnMessage(MessageEventArgs e)
        {

            JObject o = JObject.Parse(e.Data);
            if ((string)o["unityMsg"] == "msgToGame")
            {
                Tiltspot._.GameTesterOnMessage(o.ToString());
            }

            if ((string)o["unityMsg"] == "controllersReady")
            {
                info = o["data"];
                Tiltspot._.isReady = true;
            }

            if ((string)o["unityMsg"] == "connect")
            {
                Tiltspot._.GameTesterOnConnect((int)o["controllerId"]);
            }

            if ((string)o["unityMsg"] == "disconnect")
            {
                Tiltspot._.GameTesterOnDisconnect((int)o["controllerId"]);
            }

            if ((string)o["unityMsg"] == "reconnect")
            {
                Tiltspot._.GameTesterOnReconnect((int)o["controllerId"]);
            }

        }

    }
}
#endif