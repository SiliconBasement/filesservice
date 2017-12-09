using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ru.siliconbasement.micros.filesservice.storage {
    public interface IDataStorage
    {
        Task<Guid> SaveAsync(IFormFile file);
        Task<Guid> SaveTempAsync(IFormFile file);
        Task<object> GitFileInfoAsync(System.Guid uid);

        void Delete(System.Guid uid);
    }
}