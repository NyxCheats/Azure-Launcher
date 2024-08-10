// Decompiled with JetBrains decompiler
// Type: KeyAuth.api
// Assembly: Azure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34575351-D411-4450-A7EC-E5B2579DA4D6
// Assembly location: C:\Users\xifib\OneDrive\Bureau\Azure.exe

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace KeyAuth
{
  public class api
  {
    public string name;
    public string ownerid;
    public string secret;
    public string version;
    public string path;
    public static long responseTime;
    private static string sessionid;
    private static string enckey;
    private bool initialized;
    public api.app_data_class app_data = new api.app_data_class();
    public api.user_data_class user_data = new api.user_data_class();
    public api.response_class response = new api.response_class();
    private json_wrapper response_decoder = new json_wrapper((object) new api.response_structure());

    public api(string name, string ownerid, string secret, string version, string path = null)
    {
      if (ownerid.Length != 10 || secret.Length != 64)
      {
        Process.Start("https://youtube.com/watch?v=RfDTdiBq4_o");
        Process.Start("https://keyauth.cc/app/");
        Thread.Sleep(2000);
        api.error("Application not setup correctly. Please watch the YouTube video for setup.");
        Environment.Exit(0);
      }
      this.name = name;
      this.ownerid = ownerid;
      this.secret = secret;
      this.version = version;
      this.path = path;
    }

    public void init()
    {
      string str = encryption.iv_key();
      api.enckey = str + "-" + this.secret;
      NameValueCollection post_data = new NameValueCollection()
      {
        ["type"] = nameof (init),
        ["ver"] = this.version,
        ["hash"] = api.checksum(Process.GetCurrentProcess().MainModule.FileName),
        ["enckey"] = str,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      };
      if (!string.IsNullOrEmpty(this.path))
      {
        post_data.Add("token", System.IO.File.ReadAllText(this.path));
        post_data.Add("thash", api.TokenHash(this.path));
      }
      string json = api.req(post_data);
      if (json == "KeyAuth_Invalid")
      {
        api.error("Application not found");
        Environment.Exit(0);
      }
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(json);
      this.load_response_struct(generic);
      if (generic.success)
      {
        if (generic.newSession)
          Thread.Sleep(100);
        api.sessionid = generic.sessionid;
        this.initialized = true;
      }
      else
      {
        if (!(generic.message == "invalidver"))
          return;
        this.app_data.downloadLink = generic.download;
      }
    }

    public static string TokenHash(string tokenPath)
    {
      using (SHA256 shA256 = SHA256.Create())
      {
        using (FileStream inputStream = System.IO.File.OpenRead(tokenPath))
          return BitConverter.ToString(shA256.ComputeHash((Stream) inputStream)).Replace("-", string.Empty);
      }
    }

    public void CheckInit()
    {
      if (this.initialized)
        return;
    }

    public string expirydaysleft(string Type, int subscription)
    {
      this.CheckInit();
      TimeSpan timeSpan = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local).AddSeconds((double) long.Parse(this.user_data.subscriptions[subscription].expiry)).ToLocalTime() - DateTime.Now;
      switch (Type.ToLower())
      {
        case "months":
          return Convert.ToString(timeSpan.Days / 30);
        case "days":
          return Convert.ToString(timeSpan.Days);
        case "hours":
          return Convert.ToString(timeSpan.Hours);
        default:
          return (string) null;
      }
    }

    public void register(string username, string pass, string key, string email = "")
    {
      this.CheckInit();
      string str = WindowsIdentity.GetCurrent().User.Value;
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (register),
        [nameof (username)] = username,
        [nameof (pass)] = pass,
        [nameof (key)] = key,
        [nameof (email)] = email,
        ["hwid"] = str,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      this.load_response_struct(generic);
      if (!generic.success)
        return;
      this.load_user_data(generic.info);
    }

    public void forgot(string username, string email)
    {
      this.CheckInit();
      this.load_response_struct(this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (forgot),
        [nameof (username)] = username,
        [nameof (email)] = email,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      })));
    }

    public void login(string username, string pass)
    {
      this.CheckInit();
      string str = WindowsIdentity.GetCurrent().User.Value;
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (login),
        [nameof (username)] = username,
        [nameof (pass)] = pass,
        ["hwid"] = str,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      this.load_response_struct(generic);
      if (!generic.success)
        return;
      this.load_user_data(generic.info);
    }

    public void logout()
    {
      this.CheckInit();
      this.load_response_struct(this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (logout),
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      })));
    }

    public void web_login()
    {
     
    }

    public void button(string button)
    {
      this.CheckInit();
      HttpListener httpListener = new HttpListener();
      httpListener.Prefixes.Add("http://localhost:1337/" + button + "/");
      httpListener.Start();
      HttpListenerContext context = httpListener.GetContext();
      HttpListenerRequest request = context.Request;
      HttpListenerResponse response = context.Response;
      response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
      response.AddHeader("Access-Control-Allow-Origin", "*");
      response.AddHeader("Via", "hugzho's big brain");
      response.AddHeader("Location", "your kernel ;)");
      response.AddHeader("Retry-After", "never lmao");
      response.Headers.Add("Server", "\r\n\r\n");
      response.StatusCode = 420;
      response.StatusDescription = "SHEESH";
      httpListener.AuthenticationSchemes = AuthenticationSchemes.Negotiate;
      httpListener.UnsafeConnectionNtlmAuthentication = true;
      httpListener.IgnoreWriteExceptions = true;
      httpListener.Stop();
    }

    public void upgrade(string username, string key)
    {
      this.CheckInit();
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (upgrade),
        [nameof (username)] = username,
        [nameof (key)] = key,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      generic.success = false;
      this.load_response_struct(generic);
    }

    public void license(string key)
    {
      this.CheckInit();
      string str = WindowsIdentity.GetCurrent().User.Value;
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (license),
        [nameof (key)] = key,
        ["hwid"] = str,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      this.load_response_struct(generic);
      if (!generic.success)
        return;
      this.load_user_data(generic.info);
    }

    public void check()
    {
      this.CheckInit();
      this.load_response_struct(this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (check),
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      })));
    }

    public void setvar(string var, string data)
    {
      this.CheckInit();
      this.load_response_struct(this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (setvar),
        [nameof (var)] = var,
        [nameof (data)] = data,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      })));
    }

    public string getvar(string var)
    {
      this.CheckInit();
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (getvar),
        [nameof (var)] = var,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      this.load_response_struct(generic);
      return generic.success ? generic.response : (string) null;
    }

    public void ban(string reason = null)
    {
      this.CheckInit();
      this.load_response_struct(this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (ban),
        [nameof (reason)] = reason,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      })));
    }

    public string var(string varid)
    {
      this.CheckInit();
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (var),
        [nameof (varid)] = varid,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      this.load_response_struct(generic);
      return generic.success ? generic.message : (string) null;
    }

    public List<api.users> fetchOnline()
    {
      this.CheckInit();
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (fetchOnline),
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      this.load_response_struct(generic);
      return generic.success ? generic.users : (List<api.users>) null;
    }

    public void fetchStats()
    {
      this.CheckInit();
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (fetchStats),
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      this.load_response_struct(generic);
      if (!generic.success)
        return;
      this.load_app_data(generic.appinfo);
    }

    public List<api.msg> chatget(string channelname)
    {
      this.CheckInit();
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (chatget),
        ["channel"] = channelname,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      this.load_response_struct(generic);
      return generic.success ? generic.messages : (List<api.msg>) null;
    }

    public bool chatsend(string msg, string channelname)
    {
      this.CheckInit();
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (chatsend),
        ["message"] = msg,
        ["channel"] = channelname,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      this.load_response_struct(generic);
      return generic.success;
    }

    public bool checkblack()
    {
      this.CheckInit();
      string str = WindowsIdentity.GetCurrent().User.Value;
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = "checkblacklist",
        ["hwid"] = str,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      this.load_response_struct(generic);
      return generic.success;
    }

    public string webhook(string webid, string param, string body = "", string conttype = "")
    {
      this.CheckInit();
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (webhook),
        [nameof (webid)] = webid,
        ["params"] = param,
        [nameof (body)] = body,
        [nameof (conttype)] = conttype,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      this.load_response_struct(generic);
      return generic.success ? generic.response : (string) null;
    }

    public byte[] download(string fileid)
    {
      this.CheckInit();
      api.response_structure generic = this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = "file",
        [nameof (fileid)] = fileid,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      }));
      this.load_response_struct(generic);
      return generic.success ? encryption.str_to_byte_arr(generic.contents) : (byte[]) null;
    }

    public void log(string message)
    {
      this.CheckInit();
      api.req(new NameValueCollection()
      {
        ["type"] = nameof (log),
        ["pcuser"] = Environment.UserName,
        [nameof (message)] = message,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      });
    }

    public void changeUsername(string username)
    {
      this.CheckInit();
      this.load_response_struct(this.response_decoder.string_to_generic<api.response_structure>(api.req(new NameValueCollection()
      {
        ["type"] = nameof (changeUsername),
        ["newUsername"] = username,
        ["sessionid"] = api.sessionid,
        ["name"] = this.name,
        ["ownerid"] = this.ownerid
      })));
    }

    public static string checksum(string filename)
    {
      using (MD5 md5 = MD5.Create())
      {
        using (FileStream inputStream = System.IO.File.OpenRead(filename))
          return BitConverter.ToString(md5.ComputeHash((Stream) inputStream)).Replace("-", "").ToLowerInvariant();
      }
    }

    public static void LogEvent(string content)
    {
      string withoutExtension = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
      string str = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "KeyAuth", "debug", withoutExtension);
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      string path2 = string.Format("{0:MMM_dd_yyyy}_logs.txt", (object) DateTime.Now);
      string path = Path.Combine(str, path2);
      try
      {
        using (StreamWriter streamWriter = System.IO.File.AppendText(path))
          streamWriter.WriteLine(string.Format("[{0}] [{1}] {2}", (object) DateTime.Now, (object) AppDomain.CurrentDomain.FriendlyName, (object) content));
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error logging data: " + ex.Message);
      }
    }

    public static void error(string message)
    {
      string str = "Logs";
      string path = Path.Combine(str, "ErrorLogs.txt");
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      if (!System.IO.File.Exists(path))
      {
        using (System.IO.File.Create(path))
          System.IO.File.AppendAllText(path, DateTime.Now.ToString() + " > This is the start of your error logs file");
      }
      System.IO.File.AppendAllText(path, DateTime.Now.ToString() + " > " + message + Environment.NewLine);
      Process.Start(new ProcessStartInfo("cmd.exe", "/c start cmd /C \"color b && title Error && echo " + message + " && timeout /t 5\"")
      {
        CreateNoWindow = true,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false
      });
      Environment.Exit(0);
    }

    private static string req(NameValueCollection post_data)
    {
      try
      {
        using (WebClient webClient = new WebClient())
        {
          webClient.Proxy = (IWebProxy) null;
          ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(api.assertSSL);
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();
          byte[] bytes = webClient.UploadValues("https://keyauth.win/api/1.2/", post_data);
          stopwatch.Stop();
          api.responseTime = stopwatch.ElapsedMilliseconds;
          ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback) ((_param1, _param2, _param3, _param4) => true);
          api.sigCheck(Encoding.UTF8.GetString(bytes), webClient.ResponseHeaders["signature"], post_data.Get(0));
          api.LogEvent(Encoding.Default.GetString(bytes) + "\n");
          return Encoding.Default.GetString(bytes);
        }
      }
      catch (WebException ex)
      {
        if (((HttpWebResponse) ex.Response).StatusCode == (HttpStatusCode) 429)
        {
          api.error("You're connecting too fast to loader, slow down.");
          api.LogEvent("You're connecting too fast to loader, slow down.");
          Environment.Exit(0);
          return "";
        }
        api.error("Connection failure. Please try again, or contact us for help.");
        api.LogEvent("Connection failure. Please try again, or contact us for help.");
        Environment.Exit(0);
        return "";
      }
    }

    private static bool assertSSL(
      object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
    {
      if ((certificate.Issuer.Contains("Cloudflare Inc") || certificate.Issuer.Contains("Google Trust Services") || certificate.Issuer.Contains("Let's Encrypt")) && sslPolicyErrors == SslPolicyErrors.None)
        return true;
      api.error("SSL assertion fail, make sure you're not debugging Network. Disable internet firewall on router if possible. & echo: & echo If not, ask the developer of the program to use custom domains to fix this.");
      api.LogEvent("SSL assertion fail, make sure you're not debugging Network. Disable internet firewall on router if possible. If not, ask the developer of the program to use custom domains to fix this.");
      return false;
    }

    private static void sigCheck(string resp, string signature, string type)
    {
      switch (type)
      {
        case "log":
          break;
        case "file":
          break;
        default:
          try
          {
            if (encryption.CheckStringsFixedTime(encryption.HashHMAC(type == "init" ? api.enckey.Substring(17, 64) : api.enckey, resp), signature))
              break;
            break;
          }
          catch
          {
            break;
          }
      }
    }

    private void load_app_data(api.app_data_structure data)
    {
      this.app_data.numUsers = data.numUsers;
      this.app_data.numOnlineUsers = data.numOnlineUsers;
      this.app_data.numKeys = data.numKeys;
      this.app_data.version = data.version;
      this.app_data.customerPanelLink = data.customerPanelLink;
    }

    private void load_user_data(api.user_data_structure data)
    {
      this.user_data.username = data.username;
      this.user_data.ip = data.ip;
      this.user_data.hwid = data.hwid;
      this.user_data.createdate = data.createdate;
      this.user_data.lastlogin = data.lastlogin;
      this.user_data.subscriptions = data.subscriptions;
    }

    private void load_response_struct(api.response_structure data)
    {
      this.response.success = data.success;
      this.response.message = data.message;
    }

    [DataContract]
    private class response_structure
    {
      [DataMember]
      public bool success { get; set; }

      [DataMember]
      public bool newSession { get; set; }

      [DataMember]
      public string sessionid { get; set; }

      [DataMember]
      public string contents { get; set; }

      [DataMember]
      public string response { get; set; }

      [DataMember]
      public string message { get; set; }

      [DataMember]
      public string download { get; set; }

      [DataMember(IsRequired = false, EmitDefaultValue = false)]
      public api.user_data_structure info { get; set; }

      [DataMember(IsRequired = false, EmitDefaultValue = false)]
      public api.app_data_structure appinfo { get; set; }

      [DataMember]
      public List<api.msg> messages { get; set; }

      [DataMember]
      public List<api.users> users { get; set; }
    }

    public class msg
    {
      public string message { get; set; }

      public string author { get; set; }

      public string timestamp { get; set; }
    }

    public class users
    {
      public string credential { get; set; }
    }

    [DataContract]
    private class user_data_structure
    {
      [DataMember]
      public string username { get; set; }

      [DataMember]
      public string ip { get; set; }

      [DataMember]
      public string hwid { get; set; }

      [DataMember]
      public string createdate { get; set; }

      [DataMember]
      public string lastlogin { get; set; }

      [DataMember]
      public List<api.Data> subscriptions { get; set; }
    }

    [DataContract]
    private class app_data_structure
    {
      [DataMember]
      public string numUsers { get; set; }

      [DataMember]
      public string numOnlineUsers { get; set; }

      [DataMember]
      public string numKeys { get; set; }

      [DataMember]
      public string version { get; set; }

      [DataMember]
      public string customerPanelLink { get; set; }

      [DataMember]
      public string downloadLink { get; set; }
    }

    public class app_data_class
    {
      public string numUsers { get; set; }

      public string numOnlineUsers { get; set; }

      public string numKeys { get; set; }

      public string version { get; set; }

      public string customerPanelLink { get; set; }

      public string downloadLink { get; set; }
    }

    public class user_data_class
    {
      public string username { get; set; }

      public string ip { get; set; }

      public string hwid { get; set; }

      public string createdate { get; set; }

      public string lastlogin { get; set; }

      public List<api.Data> subscriptions { get; set; }
    }

    public class Data
    {
      public string subscription { get; set; }

      public string expiry { get; set; }

      public string timeleft { get; set; }
    }

    public class response_class
    {
      public bool success { get; set; }

      public string message { get; set; }
    }
  }
}
