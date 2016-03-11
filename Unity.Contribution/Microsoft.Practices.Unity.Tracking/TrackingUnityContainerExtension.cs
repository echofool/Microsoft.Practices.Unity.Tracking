using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Microsoft.Practices.Unity.Tracking
{
    internal class TrackingUnityContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            this.Context.Strategies.AddNew<TrackingTypeMappingBuilderStrategy>(UnityBuildStage.TypeMapping);
            this.Context.Strategies.AddNew<TrackingPreCreationBuilderStrategy>(UnityBuildStage.PreCreation);
        }

        public static ITrackingBuilderPolicy GetPolicy(IBuilderContext context)
        {
            return context.Policies.Get<ITrackingBuilderPolicy>(context.BuildKey, true);
        }

        public static ITrackingBuilderPolicy SetPolicy(IBuilderContext context)
        {
            ITrackingBuilderPolicy builderPolicy = new TrackingBuilderPolicy();
            context.Policies.SetDefault(builderPolicy);
            return builderPolicy;
        }
    }
}