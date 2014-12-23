using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ITCompCatalogue.Model
{
    public partial class Category
    {
        public Category()
        {
            this.Cours = new ObservableCollection<Cour>();
        }

        public long C_id { get; set; }
        public string Code { get; set; }
        public string Intitule { get; set; }
        public long TechnologieID { get; set; }

        public  Technology Technology { get; set; }
        public  ObservableCollection<Cour> Cours { get; set; }
    }
}
