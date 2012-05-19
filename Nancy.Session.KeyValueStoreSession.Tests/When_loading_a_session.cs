using System.Collections.Generic;
using FakeItEasy;
using Nancy.Testing;
using Xunit;

namespace Nancy.Session.KeyValueStoreSession.Tests
{
    public class When_loading_a_session
    {
        private IKeyValueStore _Store;
        private Browser _Browser;

        public When_loading_a_session()
        {
            var boot = new ConfigurableBootstrapper(with =>
            {
                with.DisableAutoRegistration();
                with.Module<SessionTestModule>();
            });
            _Store = A.Fake<IKeyValueStore>();
            A.CallTo(() => _Store.Load<IDictionary<string, object>>("12345")).Returns(new Dictionary<string, object> { { "TestVariable", "TestValue" } });
            KeyValueStoreSessions.Enable(boot, _Store);
            _Browser = new Browser(boot);
        }

        [Fact]
        public void It_should_load_the_session_from_the_store()
        {
            var response = _Browser.Get("/TestVariable", with =>
            {
                with.Cookie(KeyValueStoreSessions.GetCookieName(), "12345");
            });

            Assert.Equal("TestValue", response.Body.AsString());
        }

        [Fact]
        public void The_session_should_not_contain_bad_data()
        {
            var response = _Browser.Get("/Foo", with =>
            {
                with.Cookie(KeyValueStoreSessions.GetCookieName(), "12345");
            });

            Assert.Equal("", response.Body.AsString());
        }

        [Fact]
        public void Changing_the_session_should_update_the_haschanged_variable()
        {
            var response = _Browser.Post("/New", with =>
            {
                with.Body("Value");
                with.Cookie(KeyValueStoreSessions.GetCookieName(), "12345");
            });

            Assert.True(response.Context.Request.Session.HasChanged);
        }
    }
}