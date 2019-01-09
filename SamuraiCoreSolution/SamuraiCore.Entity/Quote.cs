namespace SamuraiCore.Entity
{
    public class Quote
    {
        public int QuoteId { get; set; }
        public string Text { get; set; }
        public Samurai Samurai { get; set; }
        public int SamuraiId { get; set; }
    }
}
