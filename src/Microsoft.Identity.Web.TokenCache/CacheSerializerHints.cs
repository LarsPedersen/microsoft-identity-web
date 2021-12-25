﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;

namespace Microsoft.Identity.Web.TokenCacheProviders
{
    /// <summary>
    /// Set of properties that the token cache serialization implementations might use to optimize the cache.
    /// </summary>
    public class CacheSerializerHints
    {
        /// <summary>
        /// CancellationToken enabling cooperative cancellation between threads, thread pool, or Task objects.
        /// </summary>
        public CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// Suggested cache expiry based on the in-coming token. Use to optimize cache eviction
        /// with the app token cache.
        /// </summary>
        public DateTimeOffset? SuggestedCacheExpiry { get; set; }
    }
}
