using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using OnlyFansDL.Models;
using OnlyFansDL.Requests;
using RestSharp;

namespace OnlyFansDL.Core
{
    public class Main
    {
        public ApiDefinition ApiDefinition { get; set; } = new ApiDefinition();
        public Requester Requester { get; set; }
        public string AuthToken { get; set; }
        public User User { get; set; } = new User();

        public KeyValuePair<string, string>[] Dirs { get; set; }

        public dynamic RequestedProfile { get; set; }

        public Main()
        {
            Requester = new Requester(ApiDefinition.Endpoint);
        }

        public async Task Start()
        {
            WelcomeMessage();
            GetAuthToken();

            if (String.IsNullOrEmpty(AuthToken))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("no AuthToken provided");
                Console.ResetColor();
                return;
            }


            var ownProfile = new ProfileRequest(ApiDefinition.GetUserProfileUrl(), AuthToken).Create();
            var result = Requester.SendRequest(ownProfile);

            User.Id = result["id"];
            User.Username = result["name"];

            if (!String.IsNullOrEmpty(User.Username))
            {
                Console.WriteLine("own Account found!");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{User.Username} / {User.Id}");
                Console.ResetColor();
            }

            var userName = GetUserName();

            var requestProfile = new ProfileRequest(ApiDefinition.GetSpecificUserUrl(userName), AuthToken).Create();
            RequestedProfile = Requester.SendRequest(requestProfile);


            if (RequestedProfile == null) return;

            string photos = Convert.ToString(RequestedProfile["photosCount"]);
            string videos = Convert.ToString(RequestedProfile["videosCount"]);
            var totalPosts = RequestedProfile["postsCount"];
            var id = RequestedProfile["id"];
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            Console.WriteLine($"=== User informations ({userName} / {id})");
            Console.WriteLine("");
            Console.WriteLine($"total posts: {totalPosts}");
            Console.WriteLine($"total videos: {videos}");
            Console.WriteLine($"total photos: {photos}");
            Console.ResetColor();


            Console.WriteLine("[OnlyFansDL] > creating folder structure...");
            if (!CreateUserFolderStructure(userName))
            {
                return;
            }

            Console.WriteLine("[OnlyFansDL] > folder structure created.");
            string strId = Convert.ToString(id);
            var photosRequest = new PhotosRequest(ApiDefinition.GetPhotosUrl(strId), AuthToken).Create(99999);
            var photoPosts = Requester.SendRequest(photosRequest);

            var videosRequest = new VideosRequest(ApiDefinition.GetVideosUrl(strId), AuthToken).Create(99999);
            var videosPosts = Requester.SendRequest(videosRequest);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Photos Download started ===");
            await SaveMaterial(photoPosts, photos);
            Console.WriteLine();
            Console.WriteLine("=== Videos Download started ===");
            await SaveMaterial(videosPosts, videos);
            Console.WriteLine();
            Console.ResetColor();
            
        }

        private async Task SaveMaterial(dynamic collection, string count)
        {
            var counter = 1;
            foreach (dynamic data in collection)
            {
                var media = data["media"];
                if (media == null)
                {
                    continue;
                }

                foreach (var material in media)
                {
                    string type = Convert.ToString(material["type"]);
                    string cdnUrl = Convert.ToString(material["full"]);
                    bool canView = material["canView"];

                    var requester = new Requester(cdnUrl);
                    KeyValuePair<string, string> path = new KeyValuePair<string, string>();

                    switch (type.ToLower())
                    {
                        case "photo":
                        {
                            if (canView)
                            {
                                path = Dirs.SingleOrDefault(a => a.Key == "PhotosDir");
                            }

                            break;
                        }
                        case "video":
                        {
                            if (canView)
                            {
                                path = Dirs.SingleOrDefault(a => a.Key == "VideoDir");
                            }

                            break;
                        }
                    }

                    if (path.Key == null)
                    {
                        continue;
                    }

                    var names = Regex.Matches(cdnUrl.ToLower(), @"[\w-]+\.(jpg|png|mp4|mov|jpeg)");
                    var fileName = names[0].Groups[0].Value;
                    
                    if (!File.Exists(path.Value + $"/{fileName}"))
                    {
                        var request = new DownloadRequest(AuthToken).Create();
                        requester.DownloadContent(request,path.Value,fileName);
                        if (path.Key == "PhotosDir")
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(0.1));
                        }
                    }
                    
                    counter++;
                    Console.Write($"\rDownloaded: {counter} / {count}");
                }
            }
        }

        private bool CreateUserFolderStructure(string username)
        {
            var baseDir = System.AppContext.BaseDirectory;
            var basePath = baseDir + $"{username}";

            if (!Directory.Exists(baseDir + $"{username}"))
            {
                Directory.CreateDirectory(baseDir + $"{username}");
            }

            if (!Directory.Exists(basePath + "/videos"))
            {
                Directory.CreateDirectory(basePath + "videos");
            }

            if (!Directory.Exists(basePath + "/photos"))
            {
                Directory.CreateDirectory(basePath + "photos");
            }

            Dirs = GetDirs(username);


            return true;
        }

        private KeyValuePair<string, string>[] GetDirs(string username)
        {
            var baseDir = System.AppContext.BaseDirectory;
            var basePath = baseDir + $"{username}";

            return new KeyValuePair<string, string>[]
            {
                new("BaseDir", basePath),
                new("VideoDir", $"{basePath}/videos"),
                new("PhotosDir", $"{basePath}/photos"),
            };
        }

        private string GetUserName()
        {
            Console.WriteLine();
            Console.WriteLine("please, enter the username: ");
            var userName = Console.ReadLine();
            return !String.IsNullOrEmpty(userName) ? userName : throw new Exception("No username was given!");
        }

        private void GetAuthToken()
        {
            Console.WriteLine("[OnlyFansDL] please Enter your AuthToken > ");
            AuthToken = Console.ReadLine();
        }

        private void WelcomeMessage()
        {
            Console.WriteLine("===== OnlyFans Downloader =====");
            Console.WriteLine("");
            Console.WriteLine("Repository: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("this tool is for educational purposes only");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ResetColor();
        }
    }
}