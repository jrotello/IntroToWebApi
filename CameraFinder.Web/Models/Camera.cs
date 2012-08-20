using System.ComponentModel.DataAnnotations;

namespace CameraFinder.Web.Models {
    public class Camera {
        public string Id { get; set; }

        [Required]
        public string Make { get; set; }
        
        [Required]
        public string Model { get; set; }
    }
}