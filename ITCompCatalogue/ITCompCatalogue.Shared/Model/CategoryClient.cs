using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ITCompCatalogue.Model
{
    public class CategoryClient
    {
        public String NomCategory { get; set; }
        public String LogoCategoryImage { get; set; }
        public ObservableCollection<String> ListClien { get; set; } 
    }
}
