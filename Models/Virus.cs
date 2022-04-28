using System;
using System.Collections.Generic;

namespace VirusAppFinal
{
    public partial class Virus
    {
        public Virus()
        {
            Variants = new List<Variant>();
        }

        public int Id { get; set; }
        public string? VirusName { get; set; }
        public string? VirusDateDiscovered { get; set; }
        public int? GroupId { get; set; }

        public virtual VirusGroup? Group { get; set; }
        public virtual ICollection<Variant> Variants { get; set; } = new List<Variant>();
    }
}
