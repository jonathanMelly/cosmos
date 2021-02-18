using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace lib
{
    public class Updater
    {
        private const string GITHUB_URI="github.com";
        private const string USER = "jonathanMelly";
        private const string PROJECT = "cosmos";
        private const string EXE_NAME = PROJECT;
        private const string RELEASE_API_ENDPOINT = "https://api."+GITHUB_URI+"/repos/"+USER+"/"+PROJECT+"/releases/latest";
        private const string RELEASE_DOWNLOAD_ENDPOINT =
            "https://"+GITHUB_URI+"/" + USER + "/" + PROJECT + "/releases/latest/download/"+PROJECT;
        
        private string currentVersion;
        public string LatestVersion { get; private set; }

        private string httpResult;
        private string appPath;

        public byte[] NewExecutableContent { private get; set; }

        public Updater(string currentVersion,string appPath)
        {
            this.currentVersion = currentVersion;
            this.appPath = appPath;
        }

        public string HttpResult
        {
            set => httpResult = value;
        }

        public bool IsUpdateAvailable()
        {
            if (httpResult == null)
            {
                using var client = new HttpClient();
                //Required for github API
                SetRequiredHeaders(client);

                using HttpResponseMessage response = client.GetAsync(RELEASE_API_ENDPOINT).Result;
                response.EnsureSuccessStatusCode();
                httpResult = response.Content.ReadAsStringAsync().Result;
            }
            
            //Parse http result
            var versionRegex = new Regex("\"name\"\\s*:\\s*\"(\\d+\\.\\d+\\.\\d+)\"");
            var match = versionRegex.Match(httpResult);
            if (match.Success && match.Groups.Count==2)
            {
                LatestVersion = match.Groups[1].Value;
            }
            
            return LatestVersion!=null &&  currentVersion != LatestVersion;
        }

        private static void SetRequiredHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
        }

        public bool DoUpdate()
        {
            //Prevent empty data
            if (IsUpdateAvailable())
            {
                var currentRunningAppDirectory = Path.GetDirectoryName(appPath);
                var currentRunningAppFilename = Path.GetFileName(appPath);
                
                var oldVersion = $"{appPath}.{currentVersion}";
                
                var platformIdentifier =
                    OperatingSystem.IsLinux() ? "linux" : OperatingSystem.IsMacOS() ? "osx" : "win";
                var uri =
                    $"{RELEASE_DOWNLOAD_ENDPOINT}-{platformIdentifier}-x64.zip";
                
                //To facilitate tests....
                if (NewExecutableContent == null)
                {
                    //Download latest version
                    using var client = new HttpClient();
                    
                    //Required for github API
                    SetRequiredHeaders(client);

                    using HttpResponseMessage response = client.GetAsync(uri).Result;
                    response.EnsureSuccessStatusCode();
                    NewExecutableContent = response.Content.ReadAsByteArrayAsync().Result;
                }

                try
                {

                    //Previous update failed ?
                    if (File.Exists(oldVersion))
                    {
                        File.Delete(oldVersion);
                    }

                    var newVersionZipped = new ZipArchive(new MemoryStream(NewExecutableContent));

                    var exeEntry = newVersionZipped.Entries
                        .Where(a => a.Name.StartsWith(EXE_NAME));

                    if (exeEntry.Count() !=1)
                    {
                        throw new ApplicationException($"Archive provenant de {uri} invalide");
                    }
                    else
                    {
                        var exeContent = new StreamContent(exeEntry.First().Open());

                        File.Move(appPath, oldVersion); //rename to avoid running process right issue...
                        File.WriteAllBytes(appPath, exeContent.ReadAsByteArrayAsync().Result);

                        return true;
                    }
                    
                    
                }
                finally
                {
                    //Try to put back initial version if something went wrong
                    if (!File.Exists(appPath) && File.Exists(oldVersion))
                    {
                        File.Move(oldVersion,appPath);
                    }
                }

            }

            return false;
        }
        
        


    }
    
    public static class OperatingSystem
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}