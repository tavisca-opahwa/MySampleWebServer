using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;

namespace MySampleWebServer.Tests
{
    public class RequestValidatorFixtures
    {
        private Request _request;
        private RequestProperties _requestProperties;
        public RequestValidator _requestValidator;
        [Fact]
        public void To_Check_It_Is_Not_Valid_Request()
        {
            _requestProperties = new RequestProperties("POST", "ojas/index.html", @"http://localhost:8080", "hello");
            _request = new Request(_requestProperties);
            bool ans= RequestValidator.IsValidRequest(_request);
            Assert.False(ans);
        }

        [Fact]
        public void To_Check_It_Is_Valid_Request()
        {
            _requestProperties = new RequestProperties("GET", "index.html", @"http://localhost:8080", "hello");
            _request = new Request(_requestProperties);
            bool ans = RequestValidator.IsValidRequest(_request);
            Assert.True(ans);
        }
    }
}
