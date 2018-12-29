using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotnetUnitTest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        HttpClient client;

        public ValuesController(HttpClient client)
        {
            this.client = client;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public int Get(int id)
        {
            return id;
        }

        //从https://baidu.com获取数据，将演示该操作被单元测试拦截。
        // GET api/values/baidu
        [HttpGet("baidu")]
        public async Task<string> Baidu()
        {
            return await client.GetStringAsync("https://baidu.com");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
