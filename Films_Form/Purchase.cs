using System;
using System.Collections.Generic;

namespace Films_Form
{
    public partial class Purchase
    {
        public int BId { get; set; }
        public int FId { get; set; }

        public virtual Buy B { get; set; }
        public virtual Film F { get; set; }
    }
}
