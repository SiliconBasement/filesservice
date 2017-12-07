using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ru.siliconbasement.micros.filesservice.storage {
    public class LocalFileSystemStorage : IDataStorage {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly string _storagePath;

        public LocalFileSystemStorage(IConfiguration configuration, ILogger<LocalFileSystemStorage> logger) {
            _configuration = configuration;
            _logger = logger;
            _storagePath = configuration.GetValue<string>("Storage:Path", "./storage");
            _logger.LogDebug("Storage.Path: {_storagePath}", _storagePath);
        }
        public Guid Save(string filePath) {
            Guid uid = new Guid();
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(_storagePath, uid.ToString()));
            return uid;
        }
        public object Load(Guid uid) {
            var result = new object();
            return result;
        }

        public IEnumerable<object> LoadAll()
        {
            List<object> results = new List<object>();

            return results;
        }

        public void Delete(Guid uid) {
            // TODO: Move {uid} directory to /trash/{uid}
        }
    }
}