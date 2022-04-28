using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirusAppFinal.Models;

    public class VariantsEdit
    {
        public int Id { get; set; }
        //[Display(Name = "Назва")]
        //[StringLength(255, ErrorMessage = "Занадто коротке або занадто довге.")]
        //[Required(ErrorMessage = "Поле не повинно бути пустим.")]
        public string? VariantName { get; set; }

        public List<int> CountriesIds { get; set; } = new List<int>();
    }

