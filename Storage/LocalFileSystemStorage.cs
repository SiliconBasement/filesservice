using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ru.siliconbasement.micros.filesservice.storage {
    public class LocalFileSystemStorage : IDataStorage {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly string _storagePath;

        // Path inside _storagePath where regular files to be stored
        const string FILES_PATH = "files";
        // Path inside _storagePath where preloaded temp files to be stored
        const string TEMP_PATH = "temp";

        // Path inside _storagePath where preloaded trashed files to be stored
        const string TRASH_PATH = "trash";

        public LocalFileSystemStorage(IConfiguration configuration, ILogger<LocalFileSystemStorage> logger) {
            _configuration = configuration;
            _logger = logger;
            _storagePath = configuration.GetValue<string>("Storage:Path", "./storage");
            _logger.LogDebug("Storage.Path: {_storagePath}", _storagePath);
        }

        public async Task<Guid> SaveAsync(IFormFile file) {
            Guid uid = new Guid();
            var di = System.IO.Directory.CreateDirectory(System.IO.Path.Combine(_storagePath, FILES_PATH, uid.ToString()));
            // TODO: Validate file.FileName before use it as FileSystem name
            var filePath = System.IO.Path.Combine(di.FullName, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uid;
        }

        public async Task<Guid> SaveTempAsync(IFormFile file) {
            Guid uid = new Guid();
            var di = System.IO.Directory.CreateDirectory(System.IO.Path.Combine(_storagePath, TEMP_PATH, uid.ToString()));
            // TODO: Validate file.FileName before use it as FileSystem name
            var filePath = System.IO.Path.Combine(di.FullName, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uid;
        }

        public async Task<object> GitFileInfoAsync(Guid uid) {
            var result = new object();

            return await Task.FromResult<object>(result);
        }

        public void Delete(Guid uid) {
            // TODO: Move {uid} directory to /trash/{uid}
        }
    }
}