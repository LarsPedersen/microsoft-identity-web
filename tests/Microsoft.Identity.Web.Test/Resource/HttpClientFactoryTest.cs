﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Net.Http;

namespace Microsoft.Identity.Web.Test.Resource
{
    public class HttpClientFactoryTest : IHttpClientFactory
    {
        public Dictionary<string, HttpClient> dictionary = new Dictionary<string, HttpClient>();

        public HttpClient CreateClient(string name)
        {
            using SocketsHttpHandler socketsHttpHandler = new SocketsHttpHandler();
            socketsHttpHandler.UseProxy = true;
            return new HttpClient(socketsHttpHandler);
        }
    }
}
