﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Microsoft.Identity.Web.Perf.Benchmark
{
    class Program
    {
        /// <remarks>
        /// Invocation count - how many times to run a benchmark method in an iteration (set). Is probably interchangeable with IterationTime.
        /// Iteration (target) count - how many sets of benchmark methods to run. The statistics of invocations are grouped per set.
        /// Ex: Iteration count 2, invocation count 3 - 2 sets of 3 invocations each; 6 total.
        /// Launch count - how many times to start the benchmark process.
        /// Warmup count - how many iterations to run before the statistics are gathered.
        /// </remarks>
        static void Main(string[] args)
        {
            BenchmarkRunner.Run(typeof(Program).Assembly,
                DefaultConfig.Instance
                    .WithOptions(ConfigOptions.DontOverwriteResults)
                    .AddJob(
                        Job.Default
                            .WithId("Job-TokenAcquisitionTests")
                            .WithLaunchCount(1)
                            //.WithInvocationCount(4)
                            //.WithIterationCount(2)
                            //.WithWarmupCount(0)
                            //.WithUnrollFactor(1)
                            ));
            Console.ReadKey();
        }
    }
}
