using System.Collections.Generic;

namespace SamuraiCore.Entity
{
    public class Samurai
    {
        public Samurai()
        {
            Quotes = new List<Quote>();
        }

        public int SamuraiId { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        //public int BattleId { get; set; }
        public List<SamuraiBattle> SamuraiBattle { get; set; }
        public SecretIdentity SecretIdentity { get; set; }
    }
}
