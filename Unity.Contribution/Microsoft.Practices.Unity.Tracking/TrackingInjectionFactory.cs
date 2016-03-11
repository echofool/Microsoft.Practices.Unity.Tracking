using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Tracking
{
    /// <summary>
    /// register factory with tracking dependency chain 
    /// </summary>
    public class TrackingInjectionFactory : InjectionMember
    {
        private readonly Func<IUnityContainer, IBuilderContext, ITrackingBuilderPolicy, object> _factory;

        /// <summary>
        /// register factory with tracking dependency chain
        /// </summary>
        /// <param name="factory"></param>
        public TrackingInjectionFactory(Func<IUnityContainer, IBuilderContext, ITrackingBuilderPolicy, object> factory)
        {
            Guard.ArgumentNotNull(factory, "factory");
            _factory = factory;
        }

        public override void AddPolicies(Type serviceType, Type implementationType, string name, IPolicyList policies)
        {
            Guard.ArgumentNotNull(implementationType, "implementationType");
            Guard.ArgumentNotNull(policies, "policies");
            var factoryDelegateBuildPlanPolicy = new TrackingBuildPlanPolicy(this._factory);
            policies.Set<IBuildPlanPolicy>(factoryDelegateBuildPlanPolicy, new NamedTypeBuildKey(implementationType, name));
        }
    }
}