using System.Linq;
using Nancy.Testing;
using Xunit;

namespace Nancy.Sessions.Redis.Tests
{
    public class When_enabling_sessions
    {
        private ConfigurableBootstrapper _Bootstrapper;

        public When_enabling_sessions()
        {
            _Bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.DisableAutoRegistration();
            });
            RedisBasedSessions.Enable(_Bootstrapper);
        }

        [Fact]
        public void It_should_register_a_before_request_hook()
        {
            Assert.Equal(1, _Bootstrapper.BeforeRequest.PipelineItems.Count());
        }

        [Fact]
        public void It_should_register_an_after_request_hook()
        {
            Assert.Equal(1, _Bootstrapper.AfterRequest.PipelineItems.Count());
        }
    }
}