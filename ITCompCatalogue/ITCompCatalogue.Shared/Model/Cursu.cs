using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ITCompCatalogue.Model
{
    public partial class Cursu
    {
        public Cursu()
        {
            this.CursusCours = new ObservableCollection<CursusCour>();
        }

        public long C_id { get; set; }
        public string Code { get; set; }
        public string Intitule { get; set; }

        public  ObservableCollection<CursusCour> CursusCours { get; set; }
    }
}
