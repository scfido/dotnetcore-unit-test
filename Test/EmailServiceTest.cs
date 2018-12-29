using DotnetUnitTest;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test
{
    public class EmailServiceTest
    {
        Mock<IEmailService> mock = new Mock<IEmailService>();

        public EmailServiceTest()
        {
            mock.Setup(s => s.Send(It.IsRegex(@"\w+@\w+\.\w+"), "Test", "Test mail for you.")).Returns(true);
            mock.Setup(s => s.IsOnline).Returns(true);
            mock.SetupProperty(s => s.Host, "192.168.0.1");
        }

        [Fact(DisplayName = "邮件服务地址")]
        public void Host()
        {
            Assert.True(mock.Object.Host == "192.168.0.1");

        }

        [Fact(DisplayName = "邮件服务在线")]
        public void IsOnline()
        {
            Assert.True(mock.Object.IsOnline);

        }

        [Fact(DisplayName ="发送邮件")]
        public void Send()
        {
            Assert.True(mock.Object.Send("admin@xunmei.com", "Test", "Test mail for you."));

        }

    }
}
