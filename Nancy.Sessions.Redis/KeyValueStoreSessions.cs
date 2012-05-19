using System.Collections.Generic;
using Nancy.Bootstrapper;

namespace Nancy.Sessions.Redis
{
    public class KeyValueStoreSessions
    {
        private static string cookieName = "_nsid";

        public static string GetCookieName()
        {
            return cookieName;
        }

        public static IObjectSerializerSelector Enable(IPipelines pipelines, IKeyValueStore store)
        {
            pipelines.BeforeRequest.AddItemToEndOfPipeline(ctx => LoadSession(ctx, store));
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx => SaveSession(ctx, store));
            return store;
        }

        private static Response LoadSession(NancyContext ctx, IKeyValueStore store)
        {
            if (ctx == null || ctx.Request == null)
                return null;

            IDictionary<string, object> items = null;
            if (ctx.Request.Cookies.ContainsKey(GetCookieName()))
            {
                var id = ctx.Request.Cookies[GetCookieName()];
                if (!string.IsNullOrEmpty(id))
                    items = store.Load(id) as IDictionary<string, object>;
            }

            ctx.Request.Session = new Session.Session(items ?? new Dictionary<string, object>());
            return null;
        }

        private static void SaveSession(NancyContext ctx, IKeyValueStore store)
        {
            if (ctx.Request == null || ctx.Request.Session == null || !ctx.Request.Session.HasChanged)
                return;
        }
    }
}