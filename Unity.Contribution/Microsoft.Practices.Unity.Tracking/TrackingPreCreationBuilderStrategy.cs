using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity.Tracking
{
    internal class TrackingPreCreationBuilderStrategy : BuilderStrategy
    {
        private static readonly Assembly Mscorlib = Assembly.Load("mscorlib");
        private static readonly Assembly Unity = Assembly.Load("Microsoft.Practices.Unity");
        private static readonly Type TypeOfUnityContainerExtension = typeof(UnityContainerExtension);
        private static readonly Type TypeOfBuilderStrategy = typeof(BuilderStrategy);

        public override void PreBuildUp(IBuilderContext context)
        {
            var builderPolicy = TrackingUnityContainerExtension.GetPolicy(context);
            if (builderPolicy != null)
            {
                if (builderPolicy.BuildKeys.Count > 1)
                {
                    builderPolicy.RequestType = builderPolicy.BuildKeys.ElementAt(1).Type;
                }
                else
                {
                    var stackTrace = new StackTrace();
                    var assigned = false;
                    for (var i = 0; i < stackTrace.FrameCount; i++)
                    {
                        var frame = stackTrace.GetFrame(i);
                        var type = frame.GetMethod().DeclaringType;
                        if (type?.GetCustomAttributes<CompilerGeneratedAttribute>() != null
                            && type.Assembly != Mscorlib
                            && type.Assembly != Unity
                            && !TypeOfUnityContainerExtension.IsAssignableFrom(type)
                            && !TypeOfBuilderStrategy.IsAssignableFrom(type))
                        {
                            builderPolicy.RequestType = type;
                            assigned = true;
                        }
                        else if (assigned)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}