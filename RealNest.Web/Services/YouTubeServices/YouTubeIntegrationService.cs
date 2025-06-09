using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace RealNest.Web.Services.YouTubeServices
{
    public class YouTubeIntegrationService
    {
        private readonly string clientId;
        private readonly string clientSecret;

        public YouTubeIntegrationService(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        public async Task<string> UploadVideoAsync(string title, string description, IFormFile file, YouTubeService youtubeService)
        {
            var video = new Video
            {
                Snippet = new VideoSnippet
                {
                    Title = title,
                    Description = description,
                    CategoryId = "22" 
                },
                Status = new VideoStatus
                {
                    PrivacyStatus = "public" 
                }
            };

            using (var stream = file.OpenReadStream())
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", stream, "video/*");

                videosInsertRequest.ProgressChanged += progress =>
                {
                    Console.WriteLine($"Upload status: {progress.Status}, {progress.BytesSent} bytes sent.");
                };

                videosInsertRequest.ResponseReceived += response =>
                {
                    Console.WriteLine($"Video ID: {response.Id}");
                };

                await videosInsertRequest.UploadAsync();

                return videosInsertRequest.ResponseBody.Id; 
            }
        }
    }
}
