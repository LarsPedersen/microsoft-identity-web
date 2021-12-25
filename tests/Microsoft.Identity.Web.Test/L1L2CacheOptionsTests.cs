﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web.Test.Common.TestHelpers;
using Microsoft.Identity.Web.TokenCacheProviders.Distributed;
using Xunit;

namespace Microsoft.Identity.Web.Test
{
    public class L1L2CacheOptionsTests
    {
        private ServiceProvider _provider;

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(2)]
        public void MsalDistributedTokenCacheAdapterOptions_L1ExpirationTimeRatio_ThrowsException(double expirationRatio)
        {
            // Arrange
            var msalDistributedTokenOptions = Options.Create(
                new MsalDistributedTokenCacheAdapterOptions()
                {
                    L1ExpirationTimeRatio = expirationRatio,
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3),
                });
            BuildTheRequiredServices();

            // Act & Assert Exception
            Assert.Throws<ArgumentOutOfRangeException>(() => new TestMsalDistributedTokenCacheAdapter(
                new TestDistributedCache(),
                msalDistributedTokenOptions,
                _provider.GetService<ILogger<MsalDistributedTokenCacheAdapter>>()));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(.23)]
        public void MsalDistributedTokenCacheAdapterOptions_L1ExpirationTimeRatio(double expirationRatio)
        {
            // Arrange
            var msalDistributedTokenOptions = Options.Create(
                new MsalDistributedTokenCacheAdapterOptions()
                {
                    L1ExpirationTimeRatio = expirationRatio,
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3),
                });
            BuildTheRequiredServices();

            // Act
            var testCache = new TestMsalDistributedTokenCacheAdapter(
                new TestDistributedCache(),
                msalDistributedTokenOptions,
                _provider.GetService<ILogger<MsalDistributedTokenCacheAdapter>>());

            // Assert
            Assert.NotNull(testCache);
            Assert.NotNull(testCache._distributedCache);
            Assert.NotNull(testCache._memoryCache);
            Assert.Equal(0, testCache._memoryCache.Count);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void MsalDistributedTokenCacheAdapterOptions_DisableL1Cache(bool disableL1Cache)
        {
            // Arrange
            var msalDistributedTokenOptions = Options.Create(
                new MsalDistributedTokenCacheAdapterOptions()
                {
                    DisableL1Cache = disableL1Cache,
                });
            BuildTheRequiredServices();

            // Act
            var testCache = new TestMsalDistributedTokenCacheAdapter(
                new TestDistributedCache(),
                msalDistributedTokenOptions,
                _provider.GetService<ILogger<MsalDistributedTokenCacheAdapter>>());

            // Assert
            Assert.NotNull(testCache);
            Assert.NotNull(testCache._distributedCache);

            if (!disableL1Cache)
            {
                Assert.NotNull(testCache._memoryCache);
                Assert.Equal(0, testCache._memoryCache.Count);
            }
            else
            {
                Assert.Null(testCache._memoryCache);
            }
        }

        private void BuildTheRequiredServices()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDistributedTokenCaches();
            _provider = services.BuildServiceProvider();
        }
    }
}
