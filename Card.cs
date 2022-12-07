namespace SeanMcCoysDuelConsoleGame
{
    public class Card
    {
        public Card(int value, string name, string description)
        {
            Value = value;
            Name = name;
            Description = description;
        }

        public int Value { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
