using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogrenci {

        // Id -- primary key
        [Key]
        [DisplayName("Id")]
        public int OgrenciId{ get; set; }

        [DisplayName("Örenci Adı")]
        public string? OgrenciAd { get; set; }

        [DisplayName("Örenci Soyadı")]
        public string? OgrenciSoyad { get; set; }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

    }
}