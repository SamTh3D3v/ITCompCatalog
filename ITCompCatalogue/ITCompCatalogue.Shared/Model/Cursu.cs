using System;
using System.Collections.Generic;
using System.Text;

namespace ITCompCatalogue.Model
{
    public partial class Cursu
    {
        public Cursu()
        {
            this.CursusCours = new List<CursusCour>();
        }

        public long C_id { get; set; }
        public string Code { get; set; }
        public string Intitule { get; set; }

        public  List<CursusCour> CursusCours { get; set; }
    }
}
