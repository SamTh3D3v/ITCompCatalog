using System;
using System.Collections.Generic;
using System.Text;

namespace ITCompCatalogue.Model
{
    public partial class CursusCour
    {
        public long C_id { get; set; }
        public long CursusID { get; set; }
        public long CourID { get; set; }
        public long Ordre { get; set; }
        public string Recommandation { get; set; }

        public  Cour Cour { get; set; }
        public  Cursu Cursu { get; set; }
    }
}
