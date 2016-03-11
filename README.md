# Microsoft.Practices.Unity.Tracking

tracking dependency chain when use Microsoft.Practices.Unity for IoC 

```c#
namespace Microsoft.Practices.Unity.Tracking.Tests
{
    [TestFixture]
    public class TrackingInjectionFactoryTests
    {
        public IUnityContainer Container { get; set; }
        [SetUp]
        public void Initialize()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            Container = new UnityContainer();
            Container.Tracking();
            Container.RegisterType<UserService>();
            Container.RegisterType<ILog>(new TrackingInjectionFactory((container, context, policy) => LogManager.GetLogger(policy.RequestType?.Name ?? "null")));
        }

        [Test]
        public void TrackingInjectionFactoryTest()
        {
            Parallel.For(0, 1, new ParallelOptions { MaxDegreeOfParallelism = 10 }, i =>
            {
                var userService = this.Container.CreateChildContainer().Resolve<UserService>();
                Trace.WriteLine(userService.Log.Logger.Name, "UserService.Log.Logger.Name");
                Trace.WriteLine(userService.Repository.Log.Logger.Name, "UserService.Repository.Log.Logger.Name");
            });
            var action = new Action(() =>
            {
                var log = this.Container.CreateChildContainer().Resolve<ILog>();
                Trace.WriteLine(log.Logger.Name, "Logger.Name");
            });
            action();
            var asyncResult = action.BeginInvoke(null, null);
            action.EndInvoke(asyncResult);
        }

        public class UserService
        {
            public UserRepository Repository { get; }
            public ILog Log { get; set; }

            public UserService(UserRepository repository, ILog log)
            {
                Repository = repository;
                Log = log;
            }
        }

        public class UserRepository
        {
            public ILog Log { get; }

            public UserRepository(ILog log)
            {
                Log = log;
            }
        }
    }
}
```

Output:<br/>
UserService.Log.Logger.Name: `UserService`<br/>
UserService.Repository.Log.Logger.Name: `UserRepository`<br/>
Logger.Name: `TrackingInjectionFactoryTests`<br/>
Logger.Name: `TrackingInjectionFactoryTests`<br/>

see [discussions](http://unity.codeplex.com/discussions/203744)<br/>
You can obtain an specified Logger for each type.
