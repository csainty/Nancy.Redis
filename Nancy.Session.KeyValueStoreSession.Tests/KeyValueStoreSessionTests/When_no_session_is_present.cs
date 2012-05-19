using FakeItEasy;
using Nancy.Testing;
using Xunit;

namespace Nancy.Session.KeyValueStoreSession.Tests.KeyValueStoreSessionTests
{
    public class When_no_session_is_present
    {
        private BrowserResponse _Response;

        public When_no_session_is_present()
        {
            var boot = new ConfigurableBootstrapper(with =>
            {
                with.DisableAutoRegistration();
                with.Module<SessionTestModule>();
            });
            KeyValueStoreSessions.Enable(boot, A.Fake<IKeyValueStore>());
            var browser = new Browser(boot);
            _Response = browser.Get("/TestVariable");
        }

        [Fact]
        public void No_session_should_be_loaded()
        {
            Assert.Equal("", _Response.Body.AsString());
        }
    }
}