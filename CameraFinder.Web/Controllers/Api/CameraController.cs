using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CameraFinder.Web.Infrastructure.Services;
using CameraFinder.Web.Models;

namespace CameraFinder.Web.Controllers.Api
{
    public class CamerasController : ApiController
    {
        private readonly ICameraRepository _repository;

        public CamerasController(): this(new InMemoryCameraRepository()) {}
        public CamerasController(ICameraRepository repository) {
            _repository = repository;
        }

        // GET api/camera
        public IEnumerable<Camera> Get() {
            return _repository.Get();
        }

        // GET api/camera/5
        public HttpResponseMessage Get(string id) {
            var camera = _repository.Get(id);
            if (camera == null) {
                var message = String.Format("No camera found for id: {0}", id);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, camera);
        }

        // POST api/camera
        public HttpResponseMessage Post(Camera camera) {

            if (!ModelState.IsValid) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            camera = _repository.Create(camera);
            var url = Url.Link("DefaultApi", new {controller = "cameras", id = camera.Id});

            var response = Request.CreateResponse(HttpStatusCode.Created, camera);
            response.Headers.Location = new Uri(url);

            return response;
        }

        // PUT api/camera/5
        public void Put(int id, Camera camera) {
            Request.CreateErrorResponse(HttpStatusCode.NotImplemented, String.Empty);
        }

        // DELETE api/camera/5
        public void Delete(string id) {
            _repository.Delete(id);
        }
    }
}
