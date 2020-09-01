//
// Copyright (c) ZeroC, Inc. All rights reserved.
//

using System.Collections.Generic;
using ZeroC.Ice.Instrumentation;

namespace ZeroC.Ice
{
    internal static class ObserverHelper
    {
        internal static IInvocationObserver? GetInvocationObserver(IObjectPrx proxy, string op,
                                                                   IReadOnlyDictionary<string, string> context)
        {
            if (proxy.Communicator.Observer is ICommunicatorObserver communicatorObserver)
            {
                IInvocationObserver? observer = communicatorObserver.GetInvocationObserver(proxy, op, context);
                observer?.Attach();
                return observer;
            }
            return null;
        }
    }
}
