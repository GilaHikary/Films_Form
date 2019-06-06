using System;
using System.Collections.Generic;

namespace Films_Form
{
    public partial class Buy
    {
        public Buy()
        {
            Purchase = new HashSet<Purchase>();
        }

        public int BId { get; set; }
        public int CId { get; set; }
        public int BPrice { get; set; }

        public virtual Client C { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
