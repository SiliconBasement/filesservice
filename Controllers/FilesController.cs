using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ru.siliconbasement.micros.filesservice.storage;

namespace ru.siliconbasement.micros.filesservice.controllers {
    [Route("api/[controller]")]
    public class FilesController : Controller
    {

        private readonly IDataStorage _storage;

        public FilesController(IDataStorage storage)
        {
            _storage = storage;
        }       

        // GET api/files
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return _storage.LoadAll();
        }

        // GET api/files/{uid}
        [HttpGet("{uid}", Name = "GetFile")]
        public IActionResult Get(Guid uid)
        {
            var item = _storage.Load(uid);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // // PUT api/files/5
        // [HttpPut("{uid}")]
        // public void Put(Guid uid, [FromBody]IUploadedFile value)
        // {
        // }

        // DELETE api/files/5
        [HttpDelete("{uid}")]
        public void Delete(Guid uid)
        {
            _storage.Delete(uid);
        }
    }
}