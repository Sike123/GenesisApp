using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenApp.Repository
{
    public  class Book:Asset
    {
      
        public string Isbn { get; set; }
        public string Edition { get; set; }
        public string  Publisher { get; set; }
        
        public virtual  User User { get; set; }
    }
}
