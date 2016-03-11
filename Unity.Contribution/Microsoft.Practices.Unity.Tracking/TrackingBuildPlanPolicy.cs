using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Tracking
{
    internal class TrackingBuildPlanPolicy : IBuildPlanPolicy
    {
        private readonly Func<IUnityContainer, IBuilderContext, ITrackingBuilderPolicy, object> _factory;

        public TrackingBuildPlanPolicy(Func<IUnityContainer, IBuilderContext, ITrackingBuilderPolicy, object> factory)
        {
            Guard.ArgumentNotNull(factory, "factory");
            _factory = factory;
        }

        public void BuildUp(IBuilderContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            if (context.Existing == null)
            {
                var unityContainer = context.NewBuildUp<IUnityContainer>();
                context.Existing = this._factory(unityContainer, context, context.Policies.Get<ITrackingBuilderPolicy>(context.BuildKey, true));
            }
        }
    }
}