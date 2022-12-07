namespace SeanMcCoysDuelConsoleGame
{
    public class Deck
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public Deck(string name)
        {
            Name = name;
        }
    }
}
