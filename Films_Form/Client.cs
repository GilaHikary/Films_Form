using System;
using System.Collections.Generic;

namespace Films_Form
{
    public partial class Client
    {
        public Client()
        {
            Buy = new HashSet<Buy>();
        }

        public int CId { get; set; }
        public string CFio { get; set; }
        public string CCardNumber { get; set; }
        public int? CCvv { get; set; }
        public string CLogin { get; set; }
        public string CPassword { get; set; }
        public string CDate { get; set; }

        public virtual ICollection<Buy> Buy { get; set; }
    }
}
