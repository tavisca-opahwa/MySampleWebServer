using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using Moq;
namespace MySampleWebServer.Tests
{
    public class HTTPParserFixture
    {
        [Fact]
        public void HttpParser_should_return_http_request_properties_test()
        {
            var requestMessage = @"POST /foo.php HTTP/1.1
            Host: localhost
            User-Agent: Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5 (.NET CLR 3.5.30729)
            Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
            Accept-Language: en-us,en;q=0.5
            Accept-Encoding: gzip,deflate
            Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7
            Keep-Alive: 300
            Connection: keep-alive
            Referer: http://localhost/test.php
            Content-Type: application/x-www-form-urlencoded
            Content-Length: 43";
            HTTPParser.ParseRequest(requestMessage).Should().BeOfType<RequestProperties>();
        }
        [Fact]
        public void HttpParser_should_return_null_for_null_request_test()
        {
            HTTPParser.ParseRequest(null).Should().BeNull();
        }
    }
}
