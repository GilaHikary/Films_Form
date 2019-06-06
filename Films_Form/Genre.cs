using System;
using System.Collections.Generic;

namespace Films_Form
{
    public partial class Genre
    {
        public Genre()
        {
            FilmsToGenre = new HashSet<FilmsToGenre>();
        }

        public int GId { get; set; }
        public string GName { get; set; }

        public virtual ICollection<FilmsToGenre> FilmsToGenre { get; set; }
    }
}
