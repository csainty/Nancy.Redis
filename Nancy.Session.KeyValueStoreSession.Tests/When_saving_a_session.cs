using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Nancy.Testing;
using Xunit;

namespace Nancy.Session.KeyValueStoreSession.Tests
{
    public class When_saving_a_session
    {
        private Browser _Browser;
        private IKeyValueStore _Store;

        public When_saving_a_session()
        {
            var boot = new ConfigurableBootstrapper(with =>
            {
                with.DisableAutoRegistration();
                with.Module<SessionTestModule>();
            });
            _Store = A.Fake<IKeyValueStore>();
            KeyValueStoreSessions.Enable(boot, _Store);
            _Browser = new Browser(boot);
        }

        [Fact]
        public void It_should_generate_a_session_if_required()
        {
            var response = _Browser.Post("/Key", with =>
            {
                with.Body("Value");
            });
            Assert.True(response.Context.Response.Cookies.Any(d => d.Name == KeyValueStoreSessions.GetCookieName() && !String.IsNullOrEmpty(d.Value)));
        }

        [Fact]
        public void It_should_save_the_session_to_the_store()
        {
            var response = _Browser.Post("/Key", with =>
            {
                with.Body("Value");
            });
            A.CallTo(() => _Store.Save(A<string>.That.Not.IsNullOrEmpty(), A<IDictionary<string, object>>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void It_should_keep_session_id_between_calls()
        {
            var response = _Browser.Post("/Key", with =>
            {
                with.Cookie(KeyValueStoreSessions.GetCookieName(), "12345");
                with.Body("Value");
            });
            A.CallTo(() => _Store.Save("12345", A<IDictionary<string, object>>.Ignored)).MustHaveHappened();
        }
    }
}