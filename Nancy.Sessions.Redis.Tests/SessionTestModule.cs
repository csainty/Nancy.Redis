﻿using System;
using Nancy.ModelBinding;

namespace Nancy.Sessions.Redis.Tests
{
    public class SessionTestModule : NancyModule
    {
        public SessionTestModule()
        {
            Get["/{id}"] = p => (string)Request.Session[p.id];
            Post["/{id}"] = p => Request.Session[p.id] = this.Bind();
        }
    }
}