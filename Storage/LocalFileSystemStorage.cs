using System;
using System.Collections.Generic;

namespace ru.siliconbasement.micros.filesservice.storage {
    public class LocalFileSystemStorage : IDataStorage {
        public Guid Save(string filePath) {
            Guid uid = new Guid();
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

        }
    }
}