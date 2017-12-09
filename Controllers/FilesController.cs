using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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

        // GET api/files/{uid}
        [HttpGet("{uid}", Name = "GetFile")]
        public async Task<IActionResult> Get(Guid uid)
        {
            _logger.LogDebug("GET /api/files/{uid}", uid);
            var item = await _storage.GitFileInfoAsync(uid);
            if (item == null)
            {
                _logger.LogWarning("File not found. uid: {uid}", uid);
                return NotFound();
            }
            // TODO: Return file metadata {name, links { self, delete }}
            return Ok(new { 
                file = new { 
                    name = "filename", 
                    links = new {
                        patch = @"http://fileservice/api/files/{uid}/download", 
                        download = @"http://fileservice/api/files/{uid}/download", 
                        delete = @"http://fileservice/api/files/{uid}/delete"
                    }
                }
            });
        }

        // POST api/files/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            // long size = files.Sum(f => f.Length);
            Guid[] uids = new Guid[files.Count];
            for (var i=0; i < files.Count; i++)
            {
                if (files[i].Length > 0)
                {
                    uids[i] = await _storage.SaveAsync(files[i]);
                }
            }

            return Ok(new { count = files.Count, uids = uids });
        }

        // POST api/files/preload
        [HttpPost("preload")]
        public async Task<IActionResult> PostPreload(List<IFormFile> files)
        {
            // long size = files.Sum(f => f.Length);
            Guid[] uids = new Guid[files.Count];
            for (var i=0; i < files.Count; i++)
            {
                if (files[i].Length > 0)
                {
                    uids[i] = await _storage.SaveTempAsync(files[i]);
                }
            }

            return Ok(new { count = files.Count, uids = uids });
        }

// // see http://benfoster.io/blog/aspnet-core-json-patch-partial-api-updates
//         // PATCH api/files/{uid}
//         [HttpPatch("{uid}")]
//         public async Task<IActionResult> Patch(Guid uid, [FromBody] IPartial<Person> partialPerson)
//         {
//             if (partialPerson.IsSet(p => p.Id))
//             {
//                 return BadRequest();
//             }

//             var person = await _person.Get(id);
//             partialPerson.Populate(person);
//             _person.Save(person);
//             return Ok();
//         }

        // DELETE api/files/{uid}/delete
        [HttpDelete("{uid}/delete")]
        public void Delete(Guid uid)
        {
            _storage.Delete(uid);
        }
    }
}