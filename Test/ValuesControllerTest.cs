using DotnetUnitTest.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Test
{
    public class ValuesControllerTest
    {
        ValuesController controller;

        public ValuesControllerTest()
        {
            controller = new ValuesController(new System.Net.Http.HttpClient());
        }

        [Fact]
        public void Get()
        {
            Assert.Equal<IEnumerable<string>>(new String[] { "value1", "value2" }, controller.Get());
        }

        [Fact]
        public void Post()
        {
            controller.Post("def");
        }

        [Fact(DisplayName = "Put")]
        public void Put()
        {
            controller.Put(9, "abc");
        }

        [Fact]
        public void Delete()
        {
            controller.Delete(10);
        }
    }
}
