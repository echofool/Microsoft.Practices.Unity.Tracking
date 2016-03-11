using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity.Tracking
{
    /// <summary>
    /// tracking dependency chain 
    /// </summary>
    public interface ITrackingBuilderPolicy : IBuilderPolicy
    {
        /// <summary>
        /// dependency chain of current service 
        /// </summary>
        Stack<NamedTypeBuildKey> BuildKeys { get; }

        /// <summary>
        /// which type depend on current service 
        /// </summary>
        Type RequestType { get; set; }
    }
}