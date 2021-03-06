﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlayFab.EditorModels;
using PlayFab.Internal;

namespace PlayFab
{
    public class PlayFabExtensions
    {
        public static void UploadFile(string uri, string filePath, Action<bool> callback )
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    Console.WriteLine("Uploading file to: " + uri);
                    Console.WriteLine("");
                    client.UploadFile(uri, "PUT", filePath);
                    do
                    {
                        //Block while client file is uploading.
                    } while (client.IsBusy);
                    callback(true);
                }
            }
            catch (Exception e)
            {
               Console.WriteLine(e.Message);
               callback(false);
            }
        }

        public static void DownloadFile(string uri, string filePath, Action<bool> callback)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    Console.WriteLine("Downloading file: " + uri);
                    Console.WriteLine("");
                    client.DownloadFile(uri, filePath);
                    do
                    {
                        //Block while downloading file.
                    } while (client.IsBusy);
                    callback(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Download Error:" + e.Message);
                callback(false);
            }
        }


        public static async Task<PlayFabResult<LoginResult>> Login(LoginRequest request)
        {
            //Save titleId
            var titleId = PlayFabSettings.TitleId;
            //Set titleId to editor;
            PlayFabSettings.TitleId = "editor";
            object httpResult = await PlayFabHTTP.DoPost("/DeveloperTools/User/Login", request, null, null);
            if (httpResult is PlayFabError)
            {
                PlayFabError error = (PlayFabError)httpResult;
                if (PlayFabSettings.GlobalErrorHandler != null)
                    PlayFabSettings.GlobalErrorHandler(error);
                return new PlayFabResult<LoginResult> { Error = error, };
            }
            string resultRawJson = (string)httpResult;

            var serializer = JsonSerializer.Create(PlayFabUtil.JsonSettings);
            var resultData = serializer.Deserialize<PlayFabJsonSuccess<LoginResult>>(new JsonTextReader(new StringReader(resultRawJson)));
            LoginResult result = resultData.data;

            //Set titleId back to what it was before.
            PlayFabSettings.TitleId = titleId;
            return new PlayFabResult<LoginResult> { Result = result };
        }

        public static async Task<PlayFabResult<LogoutResult>> Logout(LogoutRequest request)
        {
            //Save titleId
            var titleId = PlayFabSettings.TitleId;
            //Set titleId to editor;
            PlayFabSettings.TitleId = "editor";
            object httpResult = await PlayFabHTTP.DoPost("/DeveloperTools/User/Logout", request, null, null);
            if (httpResult is PlayFabError)
            {
                PlayFabError error = (PlayFabError)httpResult;
                if (PlayFabSettings.GlobalErrorHandler != null)
                    PlayFabSettings.GlobalErrorHandler(error);
                return new PlayFabResult<LogoutResult> { Error = error, };
            }
            string resultRawJson = (string)httpResult;

            var serializer = JsonSerializer.Create(PlayFabUtil.JsonSettings);
            var resultData = serializer.Deserialize<PlayFabJsonSuccess<LogoutResult>>(new JsonTextReader(new StringReader(resultRawJson)));
            LogoutResult result = resultData.data;

            //Set titleId back to what it was before.
            PlayFabSettings.TitleId = titleId;
            return new PlayFabResult<LogoutResult> { Result = result };
        }

        public static async Task<PlayFabResult<GetStudiosResult>> GetStudios(GetStudiosRequest request)
        {
            //Save titleId
            var titleId = PlayFabSettings.TitleId;
            //Set titleId to editor;
            PlayFabSettings.TitleId = "editor";

            object httpResult = await PlayFabHTTP.DoPost("/DeveloperTools/User/GetStudios", request, null, null);
            if (httpResult is PlayFabError)
            {
                PlayFabError error = (PlayFabError)httpResult;
                if (PlayFabSettings.GlobalErrorHandler != null)
                    PlayFabSettings.GlobalErrorHandler(error);
                return new PlayFabResult<GetStudiosResult> { Error = error, };
            }
            string resultRawJson = (string)httpResult;

            var serializer = JsonSerializer.Create(PlayFabUtil.JsonSettings);
            var resultData = serializer.Deserialize<PlayFabJsonSuccess<GetStudiosResult>>(new JsonTextReader(new StringReader(resultRawJson)));
            GetStudiosResult result = resultData.data;

            //Set titleId back to what it was before.
            PlayFabSettings.TitleId = titleId;
            return new PlayFabResult<GetStudiosResult> { Result = result };
        }

        public static async Task<PlayFabResult<CreateTitleResult>> CreateTitle(CreateTitleRequest request, Action<CreateTitleResult> resultCallback,
            Action<PlayFab.PlayFabError> errorCb)
        {
            //Save titleId
            var titleId = PlayFabSettings.TitleId;
            //Set titleId to editor;
            PlayFabSettings.TitleId = "editor";

            object httpResult = await PlayFabHTTP.DoPost("/DeveloperTools/User/CreateTitle", request, "X -Authorization", null);
            if (httpResult is PlayFabError)
            {
                PlayFabError error = (PlayFabError)httpResult;
                if (PlayFabSettings.GlobalErrorHandler != null)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
                    PlayFabSettings.GlobalErrorHandler(error);
                return new PlayFabResult<CreateTitleResult> { Error = error, };
            }
            string resultRawJson = (string)httpResult;

            var serializer = JsonSerializer.Create(PlayFabUtil.JsonSettings);
            var resultData = serializer.Deserialize<PlayFabJsonSuccess<CreateTitleResult>>(new JsonTextReader(new StringReader(resultRawJson)));
            CreateTitleResult result = resultData.data;

            //Set titleId back to what it was before.
            PlayFabSettings.TitleId = titleId;
            return new PlayFabResult<CreateTitleResult> { Result = result };
        }

    }
}
