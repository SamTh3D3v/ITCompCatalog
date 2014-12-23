﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ITCompCatalogue.Model
{
    public partial class Technology
    {
        public Technology()
        {
            this.Categories = new List<Category>();
        }

        public long C_id { get; set; }
        public string Code { get; set; }
        public string Intitule { get; set; }

        public List<Category> Categories { get; set; }
    }
}
