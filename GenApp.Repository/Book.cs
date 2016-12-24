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
