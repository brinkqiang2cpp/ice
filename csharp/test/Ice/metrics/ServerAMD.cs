//
// Copyright (c) ZeroC, Inc. All rights reserved.
//

using System.Collections.Generic;
using System.Threading.Tasks;
using Test;

namespace ZeroC.Ice.Test.Metrics
{
    public class ServerAMD : TestHelper
    {
        public override void Run(string[] args)
        {
            Dictionary<string, string> properties = CreateTestProperties(ref args);
            properties["Ice.Admin.Endpoints"] = "tcp";
            properties["Ice.Admin.InstanceName"] = "server";
            properties["Ice.Warn.Connections"] = "0";
            properties["Ice.Warn.Dispatch"] = "0";
            properties["Ice.MessageSizeMax"] = "50000";
            properties["Ice.Default.Host"] = "127.0.0.1";

            using Communicator communicator = Initialize(properties);
            communicator.SetProperty("TestAdapter.Endpoints", GetTestEndpoint(0));

            ObjectAdapter adapter = communicator.CreateObjectAdapter("TestAdapter");
            adapter.Add("metrics", new MetricsAsync());
            adapter.Activate();

            var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default);
            var adapter2 = communicator.CreateObjectAdapterWithEndpoints("TestAdapterExclusiveTS", GetTestEndpoint(2),
                taskScheduler: schedulerPair.ExclusiveScheduler);
            adapter2.Add("metrics", new MetricsAsync());
            adapter2.Activate();

            communicator.SetProperty("ControllerAdapter.Endpoints", GetTestEndpoint(1));
            ObjectAdapter controllerAdapter = communicator.CreateObjectAdapter("ControllerAdapter");
            controllerAdapter.Add("controller", new Controller(schedulerPair.ExclusiveScheduler));
            controllerAdapter.Activate();

            communicator.WaitForShutdown();
        }

        public static int Main(string[] args) => TestDriver.RunTest<ServerAMD>(args);
    }
}