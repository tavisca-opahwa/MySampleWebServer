using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace MySampleWebServer.Tests
{
    public class MineTypeFixture
    {
        [Fact]
        public void Check_Wrong_Mine_Type()
        {
            Assert.False(MineType.SupportedMine.ContainsKey(".xyz"));
        }
        [Fact]
        public void Check_Correct_Mine_Type()
        {
            Assert.True(MineType.SupportedMine.ContainsKey(".html"));
        }
    }
}
