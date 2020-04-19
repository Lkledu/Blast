using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace Phonion.Tiltspot
{
    public delegate void OnReady();
    public delegate void OnMessage(int controllerId, string msg, JToken data);
    public delegate void OnDisconnect(int controllerId);
    public delegate void OnReconnect(int controllerId);
    public delegate void OnConnect(int controllerId);

    public class Tiltspot : MonoBehaviour
    {
#if UNITY_WEBGL && !UNITY_EDITOR
[DllImport("__Internal")] public static extern void Ext_GameStarted();

[DllImport("__Internal")] public static extern void Ext_SendMsg(int controllerId, string msg, string data);

[DllImport("__Internal")] public static extern void Ext_SendSetOrientation(int controllerId, int orientation);

[DllImport("__Internal")] public static extern void Ext_SendVibrate(int controllerId, int duration);

[DllImport("__Internal")] public static extern void Ext_BroadcastMsg(string msg, string data);

[DllImport("__Internal")] public static extern void Ext_BroadcastSetOrientation(int orientation);

[DllImport("__Internal")] public static extern void Ext_BroadcastVibrate(int duration);

[DllImport("__Internal")] public static extern string Ext_GetAssetUrl(string filename);

[DllImport("__Internal")] public static extern string Ext_GetIsControllerConnected(int controllerId);

[DllImport("__Internal")] public static extern string Ext_GetControllerLatency(int controllerId);

[DllImport("__Internal")] public static extern string Ext_GetUsers();

[DllImport("__Internal")] public static extern string Ext_GetNumberOfUsers();

[DllImport("__Internal")] public static extern string Ext_GetUser(int controllerId);

[DllImport("__Internal")] public static extern string Ext_GetUserId(int controllerId);

[DllImport("__Internal")] public static extern string Ext_GetUserProfilePicture(int controllerId);

[DllImport("__Internal")] public static extern string Ext_GetUserNickname(int controllerId);

[DllImport("__Internal")] public static extern string Ext_GetHostId();

[DllImport("__Internal")] public static extern string Ext_GetHost();

[DllImport("__Internal")] public static extern string Ext_GetIsUserHost(int controllerId);

[DllImport("__Internal")] public static extern string Ext_GetEntryCode();

[DllImport("__Internal")] public static extern string Ext_GetIsGameOwnedByUser(int userId);

[DllImport("__Internal")] public static extern string Ext_GetBrowserLanguage();

[DllImport("__Internal")] public static extern string Ext_GetStartTime();
#endif
        public OnReady OnReadyDelegate;
        public OnMessage OnMessageDelegate;
        public OnDisconnect OnDisconnectDelegate;
        public OnReconnect OnReconnectDelegate;
        public OnConnect OnConnectDelegate;

        public enum Orientation { PORTRAIT, LANDSCAPE }
        public bool isReady = false;


        private static Tiltspot _instance;
        private readonly TiltspotAPIGet _APIGet = new TiltspotAPIGet();
        private readonly TiltspotAPIOn _APIOn = new TiltspotAPIOn();
        private readonly TiltspotAPISend _APISend = new TiltspotAPISend();
        private readonly TiltspotAPIBroadcast _APIBroadcast = new TiltspotAPIBroadcast();
        private TiltspotLoad _Load;

#if UNITY_WEBGL && UNITY_EDITOR
        public int tiltspotGameTesterPort = 8081;
        public bool useTiltspotGameTester = false;
        public WebServer _webServer;
        public List<string> webServer_msgQueue;
#endif

        void Awake()
        {
            _Load = gameObject.AddComponent<TiltspotLoad>();
            if (_ != this)
            {
                Destroy(this.gameObject);
                return;
            }
            gameObject.name = "Tiltspot";
            InvokeRepeating("CheckForReady", 0f, 0.3f);
#if UNITY_WEBGL && UNITY_EDITOR
            if (useTiltspotGameTester)
            {
                Application.runInBackground = true;
                webServer_msgQueue = new List<string>();
                _webServer = new WebServer(tiltspotGameTesterPort);
            }
            else
            {
                isReady = true;
            }
#endif
#if UNITY_WEBGL && !UNITY_EDITOR
            Ext_GameStarted();
#endif
        }

#if UNITY_WEBGL && UNITY_EDITOR
        void CheckForMessages()
        {
            for (int i = 0; i < webServer_msgQueue.Count; i++)
            {
                OnMessage(webServer_msgQueue[i]);
                webServer_msgQueue.RemoveAt(i);
                i--;
            }
        }

        public void GameTesterOnMessage(string s)
        {
            webServer_msgQueue.Add(s);
        }

        public void GameTesterOnConnect(int controllerId)
        {
            OnConnectDelegate?.Invoke(controllerId);
        }

        public void GameTesterOnDisconnect(int controllerId)
        {
            OnDisconnectDelegate?.Invoke(controllerId);
        }

        public void GameTesterOnReconnect(int controllerId)
        {
            OnReconnectDelegate?.Invoke(controllerId);
        }

        void OnApplicationQuit()
        {
            if (useTiltspotGameTester)
            {
                _webServer.server.Stop();
            }
        }
#endif

        void CheckForReady()
        {
            if (isReady)
            {
                CancelInvoke();
#if UNITY_WEBGL && UNITY_EDITOR
                if (useTiltspotGameTester) InvokeRepeating("CheckForMessages", 0f, 0.1f);
#endif
                OnReadyDelegate();
            }
        }

        public IEnumerator LoadImage(SpriteRenderer sr, string url, bool ownSize, float units)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable) yield return null;
#pragma warning disable CS0618 // Type or member is obsolete
            var www = new WWW(url);
#pragma warning restore CS0618 // Type or member is obsolete
            yield return www;

            if (string.IsNullOrEmpty(www.text)) Debug.Log("Download Failed");
            else
            {
                Texture2D tex = new Texture2D(1, 1);
                www.LoadImageIntoTexture(tex);
                Sprite sprite;
                if (!ownSize)
                {
                    sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one / 2);
                }
                else
                {
                    sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one / 2, tex.width / units);
                }
                sr.sprite = sprite;
            }
        }

        public bool JTokenIsNullOrEmpty(JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }

        public static Tiltspot _
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<Tiltspot>();
                    if (_instance != null)
                    {
                        if(Application.isPlaying) DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }
        }

        public static TiltspotAPIGet Get{get{return Tiltspot._._APIGet;}}

        public static TiltspotAPIOn On{get{return Tiltspot._._APIOn;}}

        public static TiltspotAPISend Send{get{return Tiltspot._._APISend;}}

        public static TiltspotAPIBroadcast Broadcast{get{return Tiltspot._._APIBroadcast;}}

        public static TiltspotLoad Load{get{return Tiltspot._._Load;}}

        public void OnReady()
        {
            isReady = true;
        }

        private void OnMessage(string json)
        {
            JObject o = JObject.Parse(json);
            OnMessageDelegate?.Invoke((int)o["controllerId"], (string)o["msg"], o["data"]);
        }

        private void OnDisconnect(string json)
        {
            JObject o = JObject.Parse(json);
            OnConnectDelegate?.Invoke((int)o["controllerId"]);
        }

        private void OnReconnect(string json)
        {
            JObject o = JObject.Parse(json);
            OnReconnectDelegate?.Invoke((int)o["controllerId"]);
        }
       
    }

    public class TiltspotAPISend
    {
        public void Msg(int controllerId, string msg)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester)
            {
                Tiltspot._._webServer.SendMsgToGameTester("msgToController", controllerId, msg, "");
            }
#else
            Tiltspot.Ext_SendMsg(controllerId, msg, "");
#endif
        }

        public void Msg(int controllerId, string msg, JToken data)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester)
            {
                Tiltspot._._webServer.SendMsgToGameTester("msgToController", controllerId, msg, data.ToString());
            }
#else
            Tiltspot.Ext_SendMsg(controllerId, msg, data.ToString());
#endif
        }

        public void SetOrientation(int controllerId, Tiltspot.Orientation orientation)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester)
            {
                Tiltspot._._webServer.SendMsgToGameTester("setOrientation", controllerId, null, orientation.ToString());
            }
#else
            Tiltspot.Ext_SendSetOrientation(controllerId, (int)orientation);
#endif
        }

        public void Vibrate(int controllerId, int duration)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester)
            {
                Tiltspot._._webServer.SendMsgToGameTester("vibrate", controllerId, "", duration.ToString());
            }
#else
            Tiltspot.Ext_SendVibrate(controllerId, duration);
#endif
        }
    }

    public class TiltspotAPIBroadcast
    {
        public void Msg(string msg)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester)
            {
                Tiltspot._._webServer.SendMsgToGameTester("broadcast", -1, msg, "");
            }
#else
            Tiltspot.Ext_BroadcastMsg(msg, "");
#endif
        }

        public void Msg(string msg, JToken data)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester)
            {
                Tiltspot._._webServer.SendMsgToGameTester("broadcast", -1, msg, data.ToString());
            }
#else
            Tiltspot.Ext_BroadcastMsg(msg, data.ToString());
#endif
        }

        public void SetOrientation(Tiltspot.Orientation orientation)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester)
            {
                Tiltspot._._webServer.SendMsgToGameTester("setOrientation", -1, null, orientation.ToString());
            }
#else
            Tiltspot.Ext_BroadcastSetOrientation((int)orientation);
#endif
        }

        public void Vibrate(int duration)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester)
            {
                Tiltspot._._webServer.SendMsgToGameTester("vibrate",-1, "", duration.ToString());
            }
#else
            Tiltspot.Ext_BroadcastVibrate(duration);
#endif
        }
    }

    public class TiltspotAPIGet
    {
        public string AssetUrl(string filename)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester) { return GameTesterHandler.info["assetsPath"].ToString()+"/"+filename; }
            else { return filename; }
#else
            return Tiltspot.Ext_GetAssetUrl(filename);
#endif
        }

        public bool IsControllerConnected(int controllerId)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            return true;
#else
            return Tiltspot.Ext_GetIsControllerConnected(controllerId) == "true";
#endif
        }

        public int ControllerLatency(int controllerId)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            return 0;
#else
            return Int32.Parse(Tiltspot.Ext_GetControllerLatency(controllerId));
#endif
        }

        public List<TiltspotUser> Users()
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester){return JsonConvert.DeserializeObject<List<TiltspotUser>>(GameTesterHandler.info["room"]["users"].ToString());}
            else{return new List<TiltspotUser>();}
#else
            return JsonConvert.DeserializeObject<List<TiltspotUser>>(Tiltspot.Ext_GetUsers());
#endif
        }

        public int NumberOfUsers()
        {
#if UNITY_WEBGL && UNITY_EDITOR
            return Tiltspot.Get.Users().Count;
#else
            return Int32.Parse(Tiltspot.Ext_GetNumberOfUsers());
#endif
        }

        public TiltspotUser User(int controllerId)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester) {
                List<TiltspotUser> l = JsonConvert.DeserializeObject<List<TiltspotUser>>(GameTesterHandler.info["room"]["users"].ToString());
                foreach(TiltspotUser u in l)
                {
                    if (u.controllerId == controllerId) return u;
                }
                return null;
            }
            else{return new TiltspotUser();}
#else
            string s = Tiltspot.Ext_GetUser(controllerId);
            if(s == ""){return new TiltspotUser();}
            else{return JsonConvert.DeserializeObject<TiltspotUser>(s);}
#endif
        }

        public int UserId(int controllerId)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester)
            {
                List<TiltspotUser> l = JsonConvert.DeserializeObject<List<TiltspotUser>>(GameTesterHandler.info["room"]["users"].ToString());
                foreach (TiltspotUser u in l)
                {
                    if (u.controllerId == controllerId) return u.userId;
                }
                return -1;
            }
            else { return -1; }
#else
            return Int32.Parse(Tiltspot.Ext_GetUserId(controllerId));
#endif
        }

        public string UserProfilePicture(int controllerId)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester)
            {
                List<TiltspotUser> l = JsonConvert.DeserializeObject<List<TiltspotUser>>(GameTesterHandler.info["room"]["users"].ToString());
                foreach (TiltspotUser u in l)
                {
                    if (u.controllerId == controllerId) return u.profilePicture;
                }
                return null;
            }
            else { return "https://tiltspot.tv/assets/profile_pictures/default.png"; }
#else
            return Tiltspot.Ext_GetUserProfilePicture(controllerId);
#endif
        }

        public string UserNickname(int controllerId)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester)
            {
                List<TiltspotUser> l = JsonConvert.DeserializeObject<List<TiltspotUser>>(GameTesterHandler.info["room"]["users"].ToString());
                foreach (TiltspotUser u in l)
                {
                    if (u.controllerId == controllerId) return u.nickname;
                }
                return null;
            }
            else { return "Nickname"; }
#else
            return Tiltspot.Ext_GetUserNickname(controllerId);
#endif
        }

        public int HostId()
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester){return (int)GameTesterHandler.info["room"]["hostId"];}
            else { return 0; }
#else
            return Int32.Parse(Tiltspot.Ext_GetHostId());
#endif
        }

        public TiltspotUser Host()
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester)
            {
                List<TiltspotUser> l = JsonConvert.DeserializeObject<List<TiltspotUser>>(GameTesterHandler.info["room"]["users"].ToString());
                foreach (TiltspotUser u in l)
                {
                    if (u.controllerId == (int)GameTesterHandler.info["room"]["hostId"]) return u;
                }
                return null;
            }
            else { return new TiltspotUser(); }
#else
            return JsonConvert.DeserializeObject<TiltspotUser>(Tiltspot.Ext_GetHost());
#endif
        }

        public bool IsUserHost(int controllerId)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester) { return controllerId == (int)GameTesterHandler.info["room"]["hostId"]; }
            else { return controllerId == 0; }
#else
            return Tiltspot.Ext_GetIsUserHost(controllerId) == "true";
#endif
        }

        public string EntryCode()
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester) { return GameTesterHandler.info["room"]["entryCode"].ToString(); }
            else { return "000000"; }
#else
            return Tiltspot.Ext_GetEntryCode();
#endif
        }

        public bool IsGameOwnedByUser(int userId)
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester) {
                List<TiltspotUser> l = JsonConvert.DeserializeObject<List<TiltspotUser>>(GameTesterHandler.info["room"]["users"].ToString());
                foreach (TiltspotUser u in l)
                {
                    if (u.userId == userId) return u.ownsGame;
                }
                return false;
            }
            else { return false; }
#else
            return Tiltspot.Ext_GetIsGameOwnedByUser(userId) == "true";
#endif
        }

        public string BrowserLanguage()
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester){return GameTesterHandler.info["browser"]["language"].ToString();}
            else{return "en-us";}
#else
            return Tiltspot.Ext_GetBrowserLanguage();
#endif
        }

        public long StartTime()
        {
#if UNITY_WEBGL && UNITY_EDITOR
            if (Tiltspot._.useTiltspotGameTester) { return (long)GameTesterHandler.info["game"]["startTime"]; }
            else { return 0; }
#else
            return Convert.ToInt64(Tiltspot.Ext_GetStartTime());
#endif
        }

        public bool IsTiltspotReady()
        {
            return Tiltspot._.isReady;
        }

    }
  
    public class TiltspotAPIOn
    {
        public _OnMsg Msg = new _OnMsg();
        public _OnReady Ready = new _OnReady();
        public _OnConnect Connect = new _OnConnect();
        public _OnDisconnect Disconnect = new _OnDisconnect();
        public _OnReconnect Reconnect = new _OnReconnect();

        public class _OnMsg
        {
            public void Add(Action<int, string, JToken> func) { Tiltspot._.OnMessageDelegate += func.Invoke; }
            public void Remove(Action<int, string, JToken> func) {
                if (Tiltspot._ == null){return;}
                try {
                    foreach (Delegate d in Tiltspot._.OnMessageDelegate.GetInvocationList())
                    {
                        if (d.Target.Equals(func)){Tiltspot._.OnMessageDelegate -= d as OnMessage;}
                    }
                } catch(Exception e) { Debug.LogWarning("Could not remove OnMsg delegate. Error: " + e.ToString()); }
            }
        }
        public class _OnReady
        {
            public void Add(Action func) { Tiltspot._.OnReadyDelegate += delegate () { func(); }; }
            public void Remove(Action func)
            {
                if (Tiltspot._ == null) { return; }
                try
                {
                    foreach (Delegate d in Tiltspot._.OnReadyDelegate.GetInvocationList())
                    {
                        if (d.Target.Equals(func)) { Tiltspot._.OnReadyDelegate -= d as OnReady; }
                    }
                }catch (Exception e) { Debug.LogWarning("Could not remove OnReady delegate. Error: " + e.ToString()); }
            }
        }
        public class _OnConnect
        {
            public void Add(Action<int> func) { Tiltspot._.OnConnectDelegate += func.Invoke; }
            public void Remove(Action<int> func) {
                if (Tiltspot._ == null) { return; }
                try {
                    foreach (Delegate d in Tiltspot._.OnConnectDelegate.GetInvocationList())
                    {
                        if (d.Target.Equals(func)) { Tiltspot._.OnConnectDelegate -= d as OnConnect; }
                    }
                } catch(Exception e) { Debug.LogWarning("Could not remove OnConnect delegate. Error: " + e.ToString()); }
            }
        }
        public class _OnReconnect
        {
            public void Add(Action<int> func) { Tiltspot._.OnReconnectDelegate += func.Invoke; }
            public void Remove(Action<int> func) {
                if (Tiltspot._ == null) { return; }
                try
                {
                    foreach (Delegate d in Tiltspot._.OnReconnectDelegate.GetInvocationList())
                    {
                        if (d.Target.Equals(func)) { Tiltspot._.OnReconnectDelegate -= d as OnReconnect; }
                    }
                }catch (Exception e) { Debug.LogWarning("Could not remove OnReconnect delegate. Error: " + e.ToString()); }
            }
        }
        public class _OnDisconnect
        {
            public void Add(Action<int> func) { Tiltspot._.OnDisconnectDelegate += func.Invoke; }
            public void Remove(Action<int> func) {
                if (Tiltspot._ == null) { return; }
                try
                {
                    foreach (Delegate d in Tiltspot._.OnDisconnectDelegate.GetInvocationList())
                    {
                        if (d.Target.Equals(func)) { Tiltspot._.OnDisconnectDelegate -= d as OnDisconnect; }
                    }
                }catch (Exception e) { Debug.LogWarning("Could not remove OnDisconnect delegate. Error: "+e.ToString()); }
            }
        }
    }

    public class TiltspotLoad : MonoBehaviour
    {
        public void ImageToSprite(SpriteRenderer sr, string url, float units = 0)
        {
            StartCoroutine(Tiltspot._.LoadImage(sr, url, units!=0, units));
        }
    }

    public class TiltspotUser
    {
        public int userId;
        public string nickname;
        public bool isLoggedIn;
        public string profilePicture;
        public int controllerId;
        public bool ownsGame; 
    }

}
