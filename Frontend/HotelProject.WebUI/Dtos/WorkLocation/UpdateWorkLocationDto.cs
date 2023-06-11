using System.ComponentModel.DataAnnotations;

namespace HotelProject.WebUI.Dtos.WorkLocation
{
    public class UpdateWorkLocationDto
    {
        [Key]
        public int WorkLocationID { get; set; }
        public string WorkLocationName { get; set; }
        public string WorkLocationCity { get; set; }
    }
}
