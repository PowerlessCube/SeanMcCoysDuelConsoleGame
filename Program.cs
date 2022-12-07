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
    {
        return;
    }
    
    var lastCardPlayed = towerOfPower.Cards.Last();

    for (int j = 0; j < otherPlayer.Hand.Cards.Count - 1; j++)
    {

        if (otherPlayer.Hand.Cards[j].Value < lastCardPlayed.Value)
        {
            break;
        }
        else
        {
            Console.WriteLine($"{otherPlayer.Name} has no cards below {lastCardPlayed.Value} to play.");
            EndSession(otherPlayer.Name);
            break;
        }
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
}

void PlayerInputOptions()
{
    Console.WriteLine();

    var userInput = Console.ReadLine();

    Console.WriteLine();

    if (userInput == Cards.KillingBlow.Value.ToString())
    {
        if (!ValidateCardAgainstTowerOfPower(Cards.KillingBlow)) return;
        KillingBlowAction();
        PlayCard(Cards.KillingBlow);
    }
    else if (userInput == Cards.HiddenStrength.Value.ToString())
    {
        if (!ValidateCardAgainstTowerOfPower(Cards.HiddenStrength)) return;
        HiddenStrengthAction();
        PlayCard(Cards.HiddenStrength);
    }
    else if (userInput == Cards.PrecisionStrike.Value.ToString())
    {
        if (!ValidateCardAgainstTowerOfPower(Cards.PrecisionStrike)) return;
        PrecisionStrikeAction();
        PlayCard(Cards.PrecisionStrike);
    }
    else if (userInput == Cards.Feint.Value.ToString())
    {
        if (!ValidateCardAgainstTowerOfPower(Cards.Feint)) return;
        FeintAction();
        PlayCard(Cards.Feint);
    }
    else if (userInput == Cards.CopyCat.Value.ToString())
    {
        if (!ValidateCardAgainstTowerOfPower(Cards.CopyCat)) return;
        
        CopyCatAction();
    }
    else if (userInput == Cards.ChangeStance.Value.ToString())
    {
        if (!ValidateCardAgainstTowerOfPower(Cards.ChangeStance)) return;
        ChangeStanceAction();
        PlayCard(Cards.ChangeStance);
    }
    else if (userInput == Cards.Disarm.Value.ToString())
    {
        if (!ValidateCardAgainstTowerOfPower(Cards.Disarm)) return;
        DisarmAction();
        PlayCard(Cards.Disarm);
    }
    else if (userInput == Cards.Backstab.Value.ToString())
    {
        if (!ValidateCardAgainstTowerOfPower(Cards.Backstab)) return;
        BackstabAction();
        PlayCard(Cards.Backstab);
    }
    else if (userInput == Cards.Dodge.Value.ToString())
    {
        if (!ValidateCardAgainstTowerOfPower(Cards.Dodge)) return;
        Console.WriteLine($"{currentPlayer.Name} played '{Cards.Dodge.Name}'");
        PlayCard(Cards.Dodge);
    }
    else if (userInput == "q")
        EndSession(currentPlayer.Name, true);
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
        Console.WriteLine($"Nothing in the {fromDeck.Name} to take.");
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
        Console.WriteLine($"{card.Value}: {card.Name}, is not an available choice.");
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
void KillingBlowAction()
{
    // Bury a face up card.
    Console.WriteLine($"{currentPlayer.Name} played '{Cards.KillingBlow.Name}'");
    if (towerOfPower.Cards.Count == 0)
    {
        Console.WriteLine($"Nothing in the Tower of Power to bury.");
        return;
    }
        
    var killingBlowChoice = true;

    while (killingBlowChoice)
    {
        DisplayDeck(towerOfPower.Cards);

        Console.WriteLine();

        var killingBlowCardChoice = Console.ReadLine();

        if (killingBlowCardChoice == Cards.KillingBlow.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.KillingBlow, towerOfPower, buriedPile))
            {
                killingBlowChoice = false;
                return;
            };
        }

        if (killingBlowCardChoice == Cards.HiddenStrength.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.HiddenStrength, towerOfPower, buriedPile))
            {
                killingBlowChoice = false;
                return;
            };
        }

        if (killingBlowCardChoice == Cards.PrecisionStrike.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.PrecisionStrike, towerOfPower, buriedPile))
            {
                killingBlowChoice = false;
                return;
            };
        }

        if (killingBlowCardChoice == Cards.Feint.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Feint, towerOfPower, buriedPile))
            {
                killingBlowChoice = false;
                return;
            };
        }

        if (killingBlowCardChoice == Cards.CopyCat.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.CopyCat, towerOfPower, buriedPile))
            {
                killingBlowChoice = false;
                return;
            };
        }

        if (killingBlowCardChoice == Cards.ChangeStance.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.ChangeStance, towerOfPower, buriedPile))
            {
                killingBlowChoice = false;
                return;
            };
        }

        if (killingBlowCardChoice == Cards.Disarm.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Disarm, towerOfPower, buriedPile))
            {
                killingBlowChoice = false;
                return;
            };
        }

        if (killingBlowCardChoice == Cards.Backstab.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Backstab, towerOfPower, buriedPile))
            {
                killingBlowChoice = false;
                return;
            };
        }

        if (killingBlowCardChoice == Cards.Dodge.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Dodge, towerOfPower, buriedPile))
            {
                killingBlowChoice = false;
                return;
            };
        }

        if (killingBlowCardChoice == "q")
        {
            killingBlowChoice = false;
            EndSession(currentPlayer.Name, true);
        }
    }
}

void HiddenStrengthAction()
{
    // Draw a random buried card.
    Console.WriteLine($"{currentPlayer.Name} played '{Cards.HiddenStrength.Name}'");
    MoveRandomCardBetweenDecks(buriedPile, currentPlayer.Hand, true);
}

void PrecisionStrikeAction()
{
    // Put a face up card in your hand.
    Console.WriteLine($"{currentPlayer.Name} played '{Cards.PrecisionStrike.Name}'");
    if (towerOfPower.Cards.Count == 0)
    {
        Console.WriteLine($"Nothing in the Tower of Power to take.");
        return;
    }

    var precisionStrikeChoice = true;

    while (precisionStrikeChoice)
    {
        DisplayDeck(towerOfPower.Cards);

        Console.WriteLine();

        var precisionStrikeCardChoice = Console.ReadLine();

        if (precisionStrikeCardChoice == Cards.KillingBlow.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.KillingBlow, towerOfPower, currentPlayer.Hand))
            {
                precisionStrikeChoice = false;
                return;
            };
        }
        else if (precisionStrikeCardChoice == Cards.HiddenStrength.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.HiddenStrength, towerOfPower, currentPlayer.Hand));
            {
                precisionStrikeChoice = false;
                return;
            }
        }
        else if (precisionStrikeCardChoice == Cards.PrecisionStrike.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.PrecisionStrike, towerOfPower, currentPlayer.Hand))
            {
                precisionStrikeChoice = false;
                return;
            }
        }
        else if (precisionStrikeCardChoice == Cards.Feint.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Feint, towerOfPower, currentPlayer.Hand))
            {
                precisionStrikeChoice = false;
                return;
            };
        }
        else if (precisionStrikeCardChoice == Cards.CopyCat.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.CopyCat, towerOfPower, currentPlayer.Hand))
            {
                precisionStrikeChoice = false;
                return;
            };
        }
        else if (precisionStrikeCardChoice == Cards.ChangeStance.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.ChangeStance, towerOfPower, currentPlayer.Hand))
            {
                precisionStrikeChoice = false;
                return;
            };
        }
        else if (precisionStrikeCardChoice == Cards.Disarm.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Disarm, towerOfPower, currentPlayer.Hand))
            {
                precisionStrikeChoice = false;
                return;
            };
        }
        else if (precisionStrikeCardChoice == Cards.Backstab.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Backstab, towerOfPower, currentPlayer.Hand))
            {
                precisionStrikeChoice = false;
                return;
            };
        }
        else if (precisionStrikeCardChoice == Cards.Dodge.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Dodge, towerOfPower, currentPlayer.Hand))
            {
                precisionStrikeChoice = false;
                return;
            };
        }
        else if (precisionStrikeCardChoice == "q")
        {
            precisionStrikeChoice = false;
            EndSession(currentPlayer.Name, true);
        }
    }
}

void FeintAction()
{
    // Play a random card from your target's hand. Ignore the ability.
    Console.WriteLine($"{currentPlayer.Name} played '{Cards.Feint.Name}'");
    PlayCard(Cards.Feint);
    MoveRandomCardBetweenDecks(otherPlayer.Hand, towerOfPower);
};

void CopyCatAction()
{
    // Copy the ability of any face up card.
    Console.WriteLine($"{currentPlayer.Name} played '{Cards.CopyCat.Name}'");

    if (towerOfPower.Cards.Count == 0)
    {
        Console.WriteLine($"Nothing in the Tower of Power to copy.");
        PlayCard(Cards.CopyCat);
        return;
    }

    var CopyCatChoice = true;

    while (CopyCatChoice)
    {
        DisplayDeck(towerOfPower.Cards);

        Console.WriteLine();

        var CopyCatCardChoice = Console.ReadLine();

        if (CopyCatCardChoice == Cards.KillingBlow.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.KillingBlow))
            {
                KillingBlowAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }
        else if (CopyCatCardChoice == Cards.HiddenStrength.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.HiddenStrength))
            {
                HiddenStrengthAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }
        else if (CopyCatCardChoice == Cards.PrecisionStrike.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.PrecisionStrike))
            {
                PrecisionStrikeAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }
        else if (CopyCatCardChoice == Cards.Feint.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.Feint))
            {
                FeintAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }
        else if (CopyCatCardChoice == Cards.CopyCat.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.CopyCat))
            {
                Console.WriteLine("You can't pick CopyCat to Copy!");
            }
        }
        else if (CopyCatCardChoice == Cards.ChangeStance.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.ChangeStance))
            {
                ChangeStanceAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }
        else if (CopyCatCardChoice == Cards.Disarm.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.Disarm))
            {
                DisarmAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }
        else if (CopyCatCardChoice == Cards.Backstab.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.Backstab))
            {
                BackstabAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }
        else if (CopyCatCardChoice == Cards.Dodge.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.Dodge))
            {
                towerOfPower.Cards.Remove(Cards.Dodge);
                PlayCard(Cards.CopyCat);
                towerOfPower.Cards.Add(Cards.Dodge);
                CopyCatChoice = false;
                return;
            }
        }
        else if (CopyCatCardChoice == "q")
        {
            CopyCatChoice = false;
            EndSession(currentPlayer.Name, true);
        }
    }
};

void ChangeStanceAction()
{
    Console.WriteLine($"{currentPlayer.Name} played '{Cards.ChangeStance.Name}'");

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
};

void DisarmAction()
{
    // Target buries a card at random.
    Console.WriteLine($"{currentPlayer.Name} played '{Cards.Disarm.Name}'");
    MoveRandomCardBetweenDecks(otherPlayer.Hand, buriedPile, true);
};

void BackstabAction()
{
    // Target gives you a card of their choice.
    Console.WriteLine($"{currentPlayer.Name} played '{Cards.Backstab.Name}'");

    if (otherPlayer.Hand.Cards.Count == 0)
    {
        Console.WriteLine($"Nothing in the {otherPlayer.Name}'s Hand to take.");
        return;
    }

    var backstabChoice = true;

    while (backstabChoice)
    {
        Console.WriteLine($"{otherPlayer.Name}, Choose a card from your hand to give to {currentPlayer.Name}");
        DisplayDeck(otherPlayer.Hand.Cards);

        Console.WriteLine();

        var BackstabCardChoice = Console.ReadLine();

        if (BackstabCardChoice == Cards.KillingBlow.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.KillingBlow, otherPlayer.Hand, currentPlayer.Hand))
            {
                backstabChoice = false;
                return;
            };
        }
        else if (BackstabCardChoice == Cards.HiddenStrength.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.HiddenStrength, otherPlayer.Hand, currentPlayer.Hand))
            {
                backstabChoice = false;
                return;
            };
        }
        else if (BackstabCardChoice == Cards.PrecisionStrike.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.PrecisionStrike, otherPlayer.Hand, currentPlayer.Hand))
            {
                backstabChoice = false;
                return;
            };
        }
        else if (BackstabCardChoice == Cards.Feint.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Feint, otherPlayer.Hand, currentPlayer.Hand))
            {
                backstabChoice = false;
                return;
            };
        }
        else if (BackstabCardChoice == Cards.ChangeStance.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.ChangeStance, otherPlayer.Hand, currentPlayer.Hand))
            {
                backstabChoice = false;
                return;
            };
        }
        else if (BackstabCardChoice == Cards.Disarm.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Disarm, otherPlayer.Hand, currentPlayer.Hand))
            {
                backstabChoice = false;
                return;
            };
        }
        else if (BackstabCardChoice == Cards.Backstab.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Backstab, otherPlayer.Hand, currentPlayer.Hand))
            {
                backstabChoice = false;
                return;
            };
        }
        else if (BackstabCardChoice == Cards.Dodge.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Dodge, otherPlayer.Hand, currentPlayer.Hand))
            {
                backstabChoice = false;
                return;
            };
        }
        else if (BackstabCardChoice == "q")
        {
            backstabChoice = false;
            EndSession(otherPlayer.Name, true);
        }
    }
}
#endregion

#region Game
while (escape)
{
    StartGame();
    SwitchPlayers();
    DisplayGameState();
    DisplayHand();
    PlayerInputOptions();
    CheckWinCondition();
}
#endregion

