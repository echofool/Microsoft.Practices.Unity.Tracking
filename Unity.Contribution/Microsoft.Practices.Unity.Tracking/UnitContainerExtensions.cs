using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Tracking
{
    public static class UnitContainerExtensions
    {
        /// <summary>
        /// tracking dependency chain
        /// </summary>
        /// <param name="container"></param>
        public static void Tracking(this IUnityContainer container)
        {
            Guard.ArgumentNotNull(container, "container");
            container.AddExtension(new TrackingUnityContainerExtension());
        }
    }
}