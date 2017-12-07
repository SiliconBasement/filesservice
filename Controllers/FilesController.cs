using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ru.siliconbasement.micros.filesservice.storage;

namespace ru.siliconbasement.micros.filesservice.controllers {
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private readonly ILogger _logger;
        private readonly IDataStorage _storage;

        public FilesController(IDataStorage storage, ILogger<FilesController> logger)
        {
            _logger = logger;
            _storage = storage;
        }       

        // GET api/files
        [HttpGet]
        public IEnumerable<object> Get()
        {
            _logger.LogDebug("GET /api/files");
            return _storage.LoadAll();
        }

        // GET api/files/{uid}
        [HttpGet("{uid}", Name = "GetFile")]
        public IActionResult Get(Guid uid)
        {
            _logger.LogDebug("GET /api/files/{uid}", uid);
            var item = _storage.Load(uid);
            if (item == null)
            {
                _logger.LogWarning("File not found. uid: {uid}", uid);
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