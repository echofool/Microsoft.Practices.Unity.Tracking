using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity.Tracking
{
    internal class TrackingBuilderPolicy : ITrackingBuilderPolicy
    {

        public TrackingBuilderPolicy()
        {
            this.BuildKeys = new Stack<NamedTypeBuildKey>();
        }

        public Stack<NamedTypeBuildKey> BuildKeys { get; private set; }

        public Type RequestType { get; set; }
    }
}