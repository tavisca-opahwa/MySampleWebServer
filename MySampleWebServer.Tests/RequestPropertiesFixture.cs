using System;
using Xunit;
using FluentAssertions;

namespace MySampleWebServer.Tests
{
    public class RequestPropertiesFixture
    {
        private RequestProperties _requestProperties; 
        [Fact]
        public void Resquest_properties_should_have_host_type_referrer_url()
        {
            _requestProperties = new RequestProperties("GET", "ojas/index.html", @"http://localhost:8080", "hello");
            _requestProperties.Host.Should().Be(@"http://localhost:8080");
            _requestProperties.Type.Should().Be("GET");
            _requestProperties.Url.Should().Be("ojas/index.html");
            _requestProperties.Referer.Should().Be("hello");

        }
    }
}
