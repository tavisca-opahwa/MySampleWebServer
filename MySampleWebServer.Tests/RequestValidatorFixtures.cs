using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using Moq;

namespace MySampleWebServer.Tests
{
    public class RequestValidatorFixtures
    {
        [Fact]
        public void To_check_it_is_not_valid_request_test()
        {
            var requestProperties = new Mock<RequestProperties>("POST", "", "", "");

            var request = new Mock<Request>(requestProperties.Object);

            RequestValidator.IsValidRequest(request.Object).Should().Be(false);
        }
        [Fact]
        public void A_null_request_should_not_be_a_valid_request_test()
        {
            RequestValidator.IsValidRequest(null).Should().Be(false);
        }
        [Fact]
        public void To_check_it_is_valid_request_test()
        {
            var requestProperties = new Mock<RequestProperties>("GET", "", "", "");

            var request = new Mock<Request>(requestProperties.Object);

            RequestValidator.IsValidRequest(request.Object).Should().Be(true);
        }
    }
}
