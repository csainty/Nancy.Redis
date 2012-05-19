using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Bootstrapper;

namespace Nancy.Sessions.Redis
{
    public class RedisBasedSessions : IObjectSerializerSelector
    {
        IObjectSerializer serializer;

        private static string cookieName = "_nsid";

        public static IObjectSerializerSelector Enable(IPipelines pipelines)
        {
            var sessions = new RedisBasedSessions();
            pipelines.BeforeRequest.AddItemToEndOfPipeline(LoadSession);
            pipelines.AfterRequest.AddItemToEndOfPipeline(SaveSession);
            return sessions;
        }

        private static Response LoadSession(NancyContext ctx)
        {
            return null;
        }

        private static void SaveSession(NancyContext ctx)
        {
            if (ctx.Request == null || ctx.Request.Session == null || !ctx.Request.Session.HasChanged)
                return;
        }

        public void WithSerializer(IObjectSerializer newSerializer)
        {
            serializer = newSerializer;
        }
    }
}