using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenApp.Web.Models
{
    public class BookViewModel : AssetViewModel
    {
        
        public string Id { get;  set; }
        public string Isbn { get; set; }
        public string Edition { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }


        public bool IsEmpty => string.IsNullOrEmpty(Isbn) && string.IsNullOrEmpty(Edition) && string.IsNullOrEmpty(Publisher) &&
                               string.IsNullOrEmpty(Category);
    }
}