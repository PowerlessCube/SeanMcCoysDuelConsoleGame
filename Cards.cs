namespace SeanMcCoysDuelConsoleGame
{
    static class Cards
    {
        public static readonly Card KillingBlow = new Card(0, "Killing Blow", "Bury a face up card.");
        public static readonly Card HiddenStrength = new Card(1, "Hidden Strength", "Draw a random buried card.");
        public static readonly Card PrecisionStrike = new Card(2, "Precision Strike", "Put a face up card in your hand.");
        public static readonly Card Feint = new Card(3, "Feint", "Play a random card from your target's hand. Ignore the ability.");
        public static readonly Card CopyCat = new Card(4, "Copycat", "Copy the ability of any face up card."); // 2 player game only and tricky to implement
        //public static readonly Card TidesOfWar = new Card(4, "Tides Of War", "Reverse the direction of play."); // 3 player game only
        //public static readonly Card Ambidextrous = new Card(4, "Ambidextrous", "Pass a card face down to your teammate."); // 4 player game only
        public static readonly Card ChangeStance = new Card(5, "Change Stance", "The next card played must be greater than 5.");
        public static readonly Card Disarm = new Card(6, "Disarm", "Target buries a card at random.");
        public static readonly Card Backstab = new Card(7, "Backstab", "Target gives you a card of their choice.");
        public static readonly Card Dodge = new Card(8, "Dodge", "Trumps last card played. Then counts as an 8.");
    }
}
