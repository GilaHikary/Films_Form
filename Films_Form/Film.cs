using System;
using System.Collections.Generic;

namespace Films_Form
{
    public partial class Film
    {
        public Film()
        {
            FilmsToGenre = new HashSet<FilmsToGenre>();
            Purchase = new HashSet<Purchase>();
        }

        public string FName { get; set; }
        public string FDesc { get; set; }
        public decimal? FImdb { get; set; }
        public int FAge { get; set; }
        public int FId { get; set; }
        public int FPrice { get; set; }

        public virtual ICollection<FilmsToGenre> FilmsToGenre { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
