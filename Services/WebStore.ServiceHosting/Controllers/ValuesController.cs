﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Values)]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> Values = Enumerable.Range(1, 10).Select(i => $"Value {i}").ToList();

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() => Values;

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= Values.Count)
                return NotFound();

            return Values[id];
        }

        [HttpPost]
        public void Post(string value) => Values.Add(value);

        [HttpPut("{id}")]
        public ActionResult Put(int id, string value)
        {
            if (id < 0 || id >= Values.Count)
                return BadRequest();

            Values[id] = value;

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= Values.Count)
                return NotFound();

            Values.RemoveAt(id);

            return Ok();
        }
    }
}