using System;
using System.Collections.Generic;

namespace Films_Form
{
    public partial class FilmsToGenre
    {
        public int GId { get; set; }
        public int FId { get; set; }

        public virtual Film F { get; set; }
        public virtual Genre G { get; set; }
    }
}
