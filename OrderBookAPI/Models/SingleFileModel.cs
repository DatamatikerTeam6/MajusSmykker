using System.ComponentModel.DataAnnotations;

namespace OrderBookAPI.Models
{
    public class SingleFileModel 

    {
         
        
        public string name { get; set; }
      
        public IFormFile File { get; set; }
    }


}
