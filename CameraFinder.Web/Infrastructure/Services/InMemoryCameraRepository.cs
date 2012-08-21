using System;
using System.Collections.Generic;
using System.Linq;
using CameraFinder.Web.Models;

namespace CameraFinder.Web.Infrastructure.Services
{
    public class InMemoryCameraRepository: ICameraRepository {
        private static readonly List<Camera> _database = new List<Camera> {
            new Camera { Id = "Canon1Dx", Make = "Canon", Model = "1Dx" },
            new Camera { Id = "Canon5DMkII", Make = "Canon", Model = "5D Mk II" },
            new Camera { Id = "Canon5DMkIII", Make = "Canon", Model = "5D Mk III" },
            new Camera { Id = "Canon7D", Make = "Canon", Model = "7D" },
            new Camera { Id = "Canon60D", Make = "Canon", Model = "60D" },
            new Camera { Id = "CanonT2i", Make = "Canon", Model = "T2i" },
            new Camera { Id = "CanonT3i", Make = "Canon", Model = "T3i" },
            new Camera { Id = "CanonT4i", Make = "Canon", Model = "T4i" }
        };

        public IEnumerable<Camera> Get() {
            return _database.OrderBy(camera => camera.Id);
        }

        public Camera Get(string id) {
            return _database.SingleOrDefault(camera => camera.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
        }

        public Camera Create(Camera camera) {
            camera.Id = GenerateCameraId(camera);
            
            var found = Get(camera.Id);
            if (found != null) {
                throw new Exception("Duplicate key error.");
            }

            _database.Add(camera);

            return camera;
        }

        private static string GenerateCameraId(Camera camera) {
            var id = camera.Id;
            if (string.IsNullOrEmpty(id)) {
                id = camera.Make + camera.Model;
            }

            return id.Replace(" ", "").Trim();
        }

        public void Update(string id, Camera camera) {
            throw new NotImplementedException();
        }

        public void Delete(string id) {
            var camera = Get(id);
            if (camera != null) {
                _database.Remove(camera);
            }
        }
    }
}