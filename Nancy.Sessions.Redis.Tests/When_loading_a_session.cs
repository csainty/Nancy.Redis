using System.Collections.Generic;
using FakeItEasy;
using Nancy.Testing;
using Xunit;

namespace Nancy.Sessions.Redis.Tests
{
    public class When_loading_a_session
    {
        private BrowserResponse _Response;
        private IKeyValueStore _Store;

        public When_loading_a_session()
        {
            var boot = new ConfigurableBootstrapper(with =>
            {
                with.DisableAutoRegistration();
                with.Module<SessionTestModule>();
            });
            _Store = A.Fake<IKeyValueStore>();
            A.CallTo(() => _Store.Load("12345")).Returns(new Dictionary<string, object> { { "TestVariable", "TestValue" } });
            KeyValueStoreSessions.Enable(boot, _Store);
            var browser = new Browser(boot);
            _Response = browser.Get("/TestVariable", with =>
            {
                with.Cookie(KeyValueStoreSessions.GetCookieName(), "12345");
            });
        }

        [Fact]
        public void It_should_load_the_session_from_the_store()
        {
            Assert.Equal("TestValue", _Response.Body.AsString());
        }
    }
}