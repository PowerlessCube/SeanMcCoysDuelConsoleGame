namespace SeanMcCoysDuelConsoleGame
{
    public class Deck
    {
        public Deck(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
    }
}
