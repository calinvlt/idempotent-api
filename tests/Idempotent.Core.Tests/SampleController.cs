using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Idempotent.Core.Tests
{
    [Route("sample")]
    public class SampleController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Enumerable.Range(1, 5).Select(index => "test_" + index).ToArray();
        }
    }
}