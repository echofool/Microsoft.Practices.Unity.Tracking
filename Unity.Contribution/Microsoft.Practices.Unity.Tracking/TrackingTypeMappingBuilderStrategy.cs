using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity.Tracking
{
    internal class TrackingTypeMappingBuilderStrategy : BuilderStrategy
    {
        public override void PreBuildUp(IBuilderContext context)
        {
            var builderPolicy = TrackingUnityContainerExtension.GetPolicy(context);
            if (builderPolicy == null)
            {
                builderPolicy = TrackingUnityContainerExtension.SetPolicy(context);
            }
            builderPolicy.BuildKeys.Push(context.BuildKey);
        }

        public override void PostBuildUp(IBuilderContext context)
        {
            var builderPolicy = TrackingUnityContainerExtension.GetPolicy(context);
            if ((builderPolicy != null) && (builderPolicy.BuildKeys.Count > 0))
            {
                builderPolicy.BuildKeys.Pop();
            }
        }
    }
}