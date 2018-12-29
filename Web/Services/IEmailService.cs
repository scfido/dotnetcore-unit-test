using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetUnitTest
{
    public interface IEmailService
    {
        string Host { get; set; }

        bool IsOnline { get; }

        bool Send(string address, string title, string message);
    }
}
