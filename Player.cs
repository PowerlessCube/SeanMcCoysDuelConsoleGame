using SeanMcCoysDuelConsoleGame;

internal class Player
{
    public string Name;
    public Deck Hand;
    public Player(string name)
    {
        Name = name;
        Hand = new Deck($"{name}'s Hand") 
        {
            Cards = new List<Card>() 
            { 
                Cards.KillingBlow,
                Cards.HiddenStrength, 
                Cards.PrecisionStrike, 
                Cards.Feint,
                Cards.CopyCat,
                Cards.ChangeStance,
                Cards.Disarm,
                Cards.Backstab,
                Cards.Dodge 
            }
        };
    }
}