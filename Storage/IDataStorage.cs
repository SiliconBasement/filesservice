using System.Collections.Generic;

namespace ru.siliconbasement.micros.filesservice.storage {
    public interface IDataStorage
    {
        System.Guid Save(string filePath);
        object Load(System.Guid uid);

        IEnumerable<object> LoadAll();

        void Delete(System.Guid uid);
    }
}