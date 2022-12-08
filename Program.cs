// See https://aka.ms/new-console-template for more information
using SeanMcCoysDuelConsoleGame;

var escape = true;
var isGameStarted = true;

var towerOfPower = new Deck("Tower of Power");
var buriedPile = new Deck("Buried Pile");

var playerOne = new Player("Steve");
var playerTwo = new Player("Jennifer");

var players = new List<Player>() { playerOne, playerTwo };

int i = new Random().Next(0, players.Count);

Player currentPlayer;
Player otherPlayer;

#region Game Session Actions
void StartGame()
{
    if (isGameStarted)
    {
        Console.WriteLine("Game Starts!");
        Console.WriteLine("Burying random cards from players' hands...");
        players.ForEach(player =>
        {
            MoveRandomCardBetweenDecks(player.Hand, buriedPile, true);
        });
    }
    isGameStarted = false;
}

void EndSession(string playerName, bool quit = false)
{
    if (quit)
    {
        Console.WriteLine($"{playerName} is quitting session.");
    }
    else
    {
        Console.WriteLine($"");
        Console.WriteLine($"{playerName} has lost. Game over!");
        Console.WriteLine($"");
    }
    escape = false;
}

int ChangeIndex() => i == 0 ? 1 : 0;

void SwitchPlayers()
{
    i = ChangeIndex();
    int currentPlayerIndex = i;
    int otherPlayerIndex = ChangeIndex();
    currentPlayer = players[currentPlayerIndex];
    otherPlayer = players[otherPlayerIndex];
}

void CheckWinCondition()
{
    if (towerOfPower.Cards.Count == 0 || otherPlayer.Hand.Cards.Contains(Cards.Dodge))
        return;
    
    var lastCardPlayed = towerOfPower.Cards.Last();

    var endSession = true;

    for (int j = 0; j < otherPlayer.Hand.Cards.Count - 1; j++)
    {
        if (otherPlayer.Hand.Cards[j].Value < lastCardPlayed.Value)
        {
            endSession = false;
            break;
        }
    }

    if (endSession)
    {
        Console.WriteLine($"{otherPlayer.Name} has no cards below {lastCardPlayed.Value} to play.");
        EndSession(otherPlayer.Name);
    }
}

void DisplayHand()
{
    Console.WriteLine();
    Console.WriteLine($"{currentPlayer.Name}'s Turn.");

    if (currentPlayer.Hand.Cards.Count == 0)
        EndSession(currentPlayer.Name);

    DisplayDeck(currentPlayer.Hand.Cards);
}

void DisplayGameState()
{
    Console.WriteLine("Game Decks");
    Console.WriteLine($"{towerOfPower.Name}: {towerOfPower.Cards.Count}");
    Console.WriteLine($"{buriedPile.Name}: {buriedPile.Cards.Count}");

    Console.WriteLine();

    Console.WriteLine("Player Hands");
    Console.WriteLine($"{playerOne.Name} has {playerOne.Hand.Cards.Count} card(s) in hand.");
    Console.WriteLine($"{playerTwo.Name} has {playerTwo.Hand.Cards.Count} card(s) in hand.");

    Console.WriteLine();

    if (towerOfPower.Cards.Count == 0)
    {
        Console.WriteLine("Nothing in the Tower");
    }
    else
    {
        var card = towerOfPower?.Cards.Last();
        Console.WriteLine($"Last card played: {card?.Value}: {card?.Name} - {card?.Description}");
    }
}

void DisplayDeck(List<Card> deck)
{
    Console.WriteLine("Choose an option:");

    deck.ForEach(card =>
    {
        Console.WriteLine($"{card.Value}: {card.Name} - {card.Description}");
    });

    Console.WriteLine();
}

void PlayerInputOptions()
{
    while (true)
    {
        DisplayHand();

        Console.WriteLine();

        var userInput = Console.ReadLine();

        Console.WriteLine();

        if (userInput == Cards.KillingBlow.Value.ToString())
        {
            if (ValidateCardAgainstTowerOfPower(Cards.KillingBlow))
            {
                Console.WriteLine($"{currentPlayer.Name} played {Cards.KillingBlow.Name}");
                KillingBlowAction();
                break;
            }
        }
        else if (userInput == Cards.HiddenStrength.Value.ToString())
        {
            if (ValidateCardAgainstTowerOfPower(Cards.HiddenStrength))
            {
                Console.WriteLine($"{currentPlayer.Name} played {Cards.HiddenStrength.Name}");
                HiddenStrengthAction();
                break;
            }
        }
        else if (userInput == Cards.PrecisionStrike.Value.ToString())
        {
            if (ValidateCardAgainstTowerOfPower(Cards.PrecisionStrike))
            {
                Console.WriteLine($"{currentPlayer.Name} played {Cards.PrecisionStrike.Name}");
                PrecisionStrikeAction();
                break;
            }
        }
        else if (userInput == Cards.Feint.Value.ToString())
        {
            if (ValidateCardAgainstTowerOfPower(Cards.Feint))
            {
                Console.WriteLine($"{currentPlayer.Name} played {Cards.Feint.Name}");
                FeintAction();
                break;
            }
        }
        else if (userInput == Cards.CopyCat.Value.ToString())
        {
            if (ValidateCardAgainstTowerOfPower(Cards.CopyCat))
            {
                Console.WriteLine($"{currentPlayer.Name} played {Cards.CopyCat.Name}");
                CopyCatAction();
                break;
            }
        }
        else if (userInput == Cards.ChangeStance.Value.ToString())
        {
            if (ValidateCardAgainstTowerOfPower(Cards.ChangeStance))
            {
                Console.WriteLine($"{currentPlayer.Name} played {Cards.ChangeStance.Name}");
                ChangeStanceAction();
                break;
            }
        }
        else if (userInput == Cards.Disarm.Value.ToString())
        {
            if (ValidateCardAgainstTowerOfPower(Cards.Disarm))
            {
                Console.WriteLine($"{currentPlayer.Name} played {Cards.Disarm.Name}");
                DisarmAction();
                break;
            }
        }
        else if (userInput == Cards.Backstab.Value.ToString())
        {
            if (ValidateCardAgainstTowerOfPower(Cards.Backstab))
            {
                Console.WriteLine($"{currentPlayer.Name} played {Cards.Backstab.Name}");
                BackstabAction();
                break;
            }
        }
        else if (userInput == Cards.Dodge.Value.ToString())
        {
            if (ValidateCardAgainstTowerOfPower(Cards.Dodge))
            {
                Console.WriteLine($"{currentPlayer.Name} played {Cards.Dodge.Name}");
                DodgeActions();
                break;
            }
        }
        else if (userInput == "q")
        {
            EndSession(currentPlayer.Name, true);
            break;
        }
    }
}

bool ValidateCardAgainstTowerOfPower(Card card)
{
    if (towerOfPower.Cards.Count() == 0)
        return true;

    if (card.Value == Cards.Dodge.Value && towerOfPower.Cards.Last() != Cards.Dodge)
        return true;

    if (towerOfPower.Cards.Last().Value == Cards.ChangeStance.Value)
    {
        if (card.Value <= Cards.ChangeStance.Value)
        {
            Console.WriteLine($"You must play a card Higher than {towerOfPower.Cards.Last().Value}: {towerOfPower.Cards.Last().Name}");
            Console.WriteLine();
            return false;
        }
    }

    if (towerOfPower.Cards.Last().Value == Cards.Dodge.Value && card.Value == Cards.KillingBlow.Value)
    {
        Console.WriteLine($"You can not play a {Cards.KillingBlow.Name} after a {Cards.Dodge.Name}.");
        Console.WriteLine();
        return false;
    }

    if ((card.Value >= towerOfPower.Cards.Last().Value) && (towerOfPower.Cards.Last().Value != Cards.ChangeStance.Value))
    {
        Console.WriteLine($"You must play a card lower than {towerOfPower.Cards.Last().Value}: {towerOfPower.Cards.Last().Name}");
        Console.WriteLine();
        return false;
    }

    return true;
}

void PlayCard(Card card)
{
    MoveCardBetweenDecks(card, currentPlayer.Hand, towerOfPower);
}

bool MoveRandomCardBetweenDecks(Deck fromDeck, Deck toDeck, bool hideCard = false)
{
    if (fromDeck.Cards.Count == 0)
    {
        Console.WriteLine($"No cards in {fromDeck.Name} deck.");
        return false;
    }

    int randomDeckIndex = new Random().Next(0, fromDeck.Cards.Count);
    Card randomCard = fromDeck.Cards[randomDeckIndex];

    return MoveCardBetweenDecks(randomCard, fromDeck, toDeck, hideCard);
}

bool MoveCardBetweenDecks(Card card, Deck fromDeck, Deck toDeck, bool hideCard = false)
{
    if (!fromDeck.Cards.Contains(card))
    {
        Console.WriteLine($"{card.Value}: {card.Name}, is not an available choice in {fromDeck.Name}.");
        return false;
    }

    fromDeck.Cards.Remove(card);
    toDeck.Cards.Add(card);
        
    if (hideCard)
    {
        Console.WriteLine($"A card moved from {fromDeck.Name} to the {toDeck.Name}");
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine($"{card.Value}: {card.Name} - moved from {fromDeck.Name} to the {toDeck.Name}");
        Console.WriteLine();
    }
        
    return true;
}
#endregion

#region Card Actions
void KillingBlowAction(bool isCopyCat = false)
{
    // Bury a face up card.
    while (true)
    {
        if (towerOfPower.Cards.Count == 0)
        {
            Console.WriteLine($"Nothing in the Tower of Power to bury.");
            break;
        }

        DisplayDeck(towerOfPower.Cards);

        Console.WriteLine();

        var killingBlowCardChoice = Console.ReadLine();

        if (killingBlowCardChoice == Cards.KillingBlow.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.KillingBlow, towerOfPower, buriedPile))
                break;
        }

        if (killingBlowCardChoice == Cards.HiddenStrength.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.HiddenStrength, towerOfPower, buriedPile))
                break;
        }

        if (killingBlowCardChoice == Cards.PrecisionStrike.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.PrecisionStrike, towerOfPower, buriedPile))
                break;
        }

        if (killingBlowCardChoice == Cards.Feint.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Feint, towerOfPower, buriedPile))
                break;
        }

        if (killingBlowCardChoice == Cards.CopyCat.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.CopyCat, towerOfPower, buriedPile))
                break;
        }

        if (killingBlowCardChoice == Cards.ChangeStance.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.ChangeStance, towerOfPower, buriedPile))
                break;
        }

        if (killingBlowCardChoice == Cards.Disarm.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Disarm, towerOfPower, buriedPile))
                break;
        }

        if (killingBlowCardChoice == Cards.Backstab.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Backstab, towerOfPower, buriedPile))
                break;
        }

        if (killingBlowCardChoice == Cards.Dodge.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Dodge, towerOfPower, buriedPile))
                break;
        }

        if (killingBlowCardChoice == "q")
        {
            EndSession(currentPlayer.Name, true);
            return;
        }
    }

    if (isCopyCat) return;
    if (!escape) return;
    PlayCard(Cards.KillingBlow);
}

void HiddenStrengthAction(bool isCopyCat = false)
{
    // Draw a random buried card.
    MoveRandomCardBetweenDecks(buriedPile, currentPlayer.Hand, true);

    if (isCopyCat) return;
    PlayCard(Cards.HiddenStrength);
}

void PrecisionStrikeAction(bool isCopyCat = false)
{
    // Put a face up card in your hand.
    while (true)
    {
        if (towerOfPower.Cards.Count == 0)
        {
            Console.WriteLine($"Nothing in the Tower of Power to take.");
            break;
        }
        DisplayDeck(towerOfPower.Cards);

        Console.WriteLine();

        var precisionStrikeCardChoice = Console.ReadLine();

        if (precisionStrikeCardChoice == Cards.KillingBlow.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.KillingBlow, towerOfPower, currentPlayer.Hand))
                break;
        }
        else if (precisionStrikeCardChoice == Cards.HiddenStrength.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.HiddenStrength, towerOfPower, currentPlayer.Hand));
                break;
        }
        else if (precisionStrikeCardChoice == Cards.PrecisionStrike.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.PrecisionStrike, towerOfPower, currentPlayer.Hand))
                break;
        }
        else if (precisionStrikeCardChoice == Cards.Feint.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Feint, towerOfPower, currentPlayer.Hand))
                break;
        }
        else if (precisionStrikeCardChoice == Cards.CopyCat.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.CopyCat, towerOfPower, currentPlayer.Hand))
                break;
        }
        else if (precisionStrikeCardChoice == Cards.ChangeStance.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.ChangeStance, towerOfPower, currentPlayer.Hand))
                break;
        }
        else if (precisionStrikeCardChoice == Cards.Disarm.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Disarm, towerOfPower, currentPlayer.Hand))
                break;
        }
        else if (precisionStrikeCardChoice == Cards.Backstab.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Backstab, towerOfPower, currentPlayer.Hand))
                break;
        }
        else if (precisionStrikeCardChoice == Cards.Dodge.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Dodge, towerOfPower, currentPlayer.Hand))
                break;
        }
        else if (precisionStrikeCardChoice == "q")
        {
            EndSession(currentPlayer.Name, true);
            break;
        }
    }

    if (isCopyCat) return;
    if (!escape) return;
    PlayCard(Cards.PrecisionStrike);
}

void FeintAction(bool isCopyCat = false)
{
    // Play a random card from your target's hand. Ignore the ability.
    if (isCopyCat)
    {
        MoveRandomCardBetweenDecks(otherPlayer.Hand, towerOfPower);
    }
    else
    {
        PlayCard(Cards.Feint);
        MoveRandomCardBetweenDecks(otherPlayer.Hand, towerOfPower);
    }
};

void CopyCatAction()
{
    // Copy the ability of any face up card.
    while (true)
    {
        if (towerOfPower.Cards.Count == 0)
        {
            Console.WriteLine($"Nothing in the Tower of Power to copy.");
            break;
        }

        DisplayDeck(towerOfPower.Cards);

        Console.WriteLine();

        var CopyCatCardChoice = Console.ReadLine();

        if (CopyCatCardChoice == Cards.KillingBlow.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.KillingBlow))
            {
                Console.WriteLine($"Copying {Cards.KillingBlow.Name} Ability.");
                KillingBlowAction(true);
                break;
            }
        }
        else if (CopyCatCardChoice == Cards.HiddenStrength.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.HiddenStrength))
            {
                Console.WriteLine($"Copying {Cards.HiddenStrength.Name} Ability.");
                HiddenStrengthAction(true);
                break;
            }
        }
        else if (CopyCatCardChoice == Cards.PrecisionStrike.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.PrecisionStrike))
            {
                Console.WriteLine($"Copying {Cards.PrecisionStrike.Name} Ability.");
                PrecisionStrikeAction(true);
                break;
            }
        }
        else if (CopyCatCardChoice == Cards.Feint.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.Feint))
            {
                Console.WriteLine($"Copying {Cards.Feint.Name} Ability.");
                FeintAction(true);
                break;
            }
        }
        else if (CopyCatCardChoice == Cards.CopyCat.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.CopyCat))
                Console.WriteLine("You can't pick CopyCat to Copy!");
        }
        else if (CopyCatCardChoice == Cards.ChangeStance.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.ChangeStance))
            {
                Console.WriteLine($"Copying {Cards.ChangeStance.Name} Ability.");
                ChangeStanceAction(true);
                break;
            }
        }
        else if (CopyCatCardChoice == Cards.Disarm.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.Disarm))
            {
                Console.WriteLine($"Copying {Cards.Disarm.Name} Ability.");
                DisarmAction(true);
                break;
            }
        }
        else if (CopyCatCardChoice == Cards.Backstab.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.Backstab))
            {
                Console.WriteLine($"Copying {Cards.Backstab.Name} Ability.");
                BackstabAction(true);
                break;
            }
        }
        else if (CopyCatCardChoice == Cards.Dodge.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.Dodge))
            {
                Console.WriteLine($"Copying {Cards.Dodge.Name} Ability.");
                DodgeActions(true);
                break;
            }
        }
        else if (CopyCatCardChoice == "q")
        {
            EndSession(currentPlayer.Name, true);
            break;
        }
    }

    if (!escape) return;
    PlayCard(Cards.CopyCat);
};

void ChangeStanceAction(bool isCopyCat = false)
{
    if (towerOfPower.Cards.Count == 0 || otherPlayer.Hand.Cards.Contains(Cards.Dodge))
    {
        PlayCard(Cards.ChangeStance);
        return;
    }

    var noCardOverFive = true;

    for (int k = 0; k < otherPlayer.Hand.Cards.Count - 1; k++)
    {
        if (otherPlayer.Hand.Cards[k].Value > Cards.ChangeStance.Value)
        {   
            noCardOverFive = false;
            break;
        }
    }

    if (noCardOverFive)
    {
        Console.WriteLine($"{otherPlayer.Name} has no cards over 5 to play.");
        EndSession(otherPlayer.Name);
    } 
    
    if (isCopyCat) return;
    PlayCard(Cards.ChangeStance);
};

void DisarmAction(bool isCopyCat = false)
{
    // Target buries a card at random.
    if (isCopyCat)
    {
        MoveRandomCardBetweenDecks(otherPlayer.Hand, buriedPile, true);
    }
    else
    {
        PlayCard(Cards.Disarm);
        MoveRandomCardBetweenDecks(otherPlayer.Hand, buriedPile, true);
    }
};

void BackstabAction(bool isCopyCat = false)
{
    // Target gives you a card of their choice.
    while (true)
    {
        if (otherPlayer.Hand.Cards.Count == 0)
        {
            Console.WriteLine($"Nothing in the {otherPlayer.Name}'s Hand to take.");
            EndSession(otherPlayer.Name);
            break;
        }

        Console.WriteLine($"{otherPlayer.Name}, Choose a card from your hand to give to {currentPlayer.Name}");
        
        DisplayDeck(otherPlayer.Hand.Cards);

        var BackstabCardChoice = Console.ReadLine();

        if (BackstabCardChoice == Cards.KillingBlow.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.KillingBlow, otherPlayer.Hand, currentPlayer.Hand))
                break;
        }
        else if (BackstabCardChoice == Cards.HiddenStrength.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.HiddenStrength, otherPlayer.Hand, currentPlayer.Hand))
                break;
        }
        else if (BackstabCardChoice == Cards.PrecisionStrike.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.PrecisionStrike, otherPlayer.Hand, currentPlayer.Hand))
                break;
        }
        else if (BackstabCardChoice == Cards.Feint.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Feint, otherPlayer.Hand, currentPlayer.Hand))
                break;
        }
        else if (BackstabCardChoice == Cards.CopyCat.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.CopyCat, otherPlayer.Hand, currentPlayer.Hand))
                break;
        }
        else if (BackstabCardChoice == Cards.ChangeStance.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.ChangeStance, otherPlayer.Hand, currentPlayer.Hand))
                break;
        }
        else if (BackstabCardChoice == Cards.Disarm.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Disarm, otherPlayer.Hand, currentPlayer.Hand))
                break;
        }
        else if (BackstabCardChoice == Cards.Backstab.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Backstab, otherPlayer.Hand, currentPlayer.Hand))
                break;
        }
        else if (BackstabCardChoice == Cards.Dodge.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Dodge, otherPlayer.Hand, currentPlayer.Hand))
                break;
        }
        else if (BackstabCardChoice == "q")
        {
            EndSession(otherPlayer.Name, true);
            break;
        }
    }

    if (isCopyCat) return;
    if (!escape) return;
    PlayCard(Cards.Backstab);
}

void DodgeActions(bool isCopyCat = false)
{
    if (isCopyCat)
    {
        PlayCard(Cards.CopyCat);
        Console.WriteLine($"Copying the {Cards.Dodge.Name} action");
        towerOfPower.Cards.Remove(Cards.Dodge);
        towerOfPower.Cards.Add(Cards.Dodge);
    } 
    else
    {
        PlayCard(Cards.Dodge);
    }
}
#endregion

#region Game
while (escape)
{
    StartGame();
    SwitchPlayers();
    DisplayGameState();
    PlayerInputOptions();
    CheckWinCondition();
}
#endregion

