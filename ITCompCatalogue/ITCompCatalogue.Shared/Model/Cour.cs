using System;
using System.Collections.Generic;
using System.Text;

namespace ITCompCatalogue.Model
{
    public partial class Cour
    {
        public Cour()
        {
            this.CursusCours = new List<CursusCour>();
        }

        public long C_id { get; set; }
        public string Code { get; set; }
        public string Intitule { get; set; }
        public string Duree { get; set; }
        public string Niveau { get; set; }
        public string Annee { get; set; }
        public string Description { get; set; }
        public long CategorieID { get; set; }

        public  Category Category { get; set; }   
        public  List<CursusCour> CursusCours { get; set; }
    }
}
