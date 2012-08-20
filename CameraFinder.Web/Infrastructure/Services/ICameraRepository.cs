using System.Collections.Generic;
using CameraFinder.Web.Models;

namespace CameraFinder.Web.Infrastructure.Services {
    public interface ICameraRepository {
        IEnumerable<Camera> Get();
        Camera Get(string id);
        Camera Create(Camera camera);
        void Update(string id, Camera camera);
        void Delete(string id);
    }
}