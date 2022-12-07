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

#region Game Session Actions
void QuittingSession()
{
    Console.WriteLine("Your quitting session.");
    escape = false;
}

void StartGame()
{
    if (isGameStarted)
    {
        players.ForEach(player =>
        {
            MoveRandomCardBetweenDecks(player.Hand, buriedPile, true);
        });

    }

    isGameStarted = false;
}

void CheckWinCondition()
{
    if (towerOfPower.Cards.Count == 0 || players[i].Hand.Cards.Contains(Cards.Dodge))
    {
        return;
    }
    
    var lastCardPlayed = towerOfPower.Cards.Last();

    for (int j = 0; j < players[i].Hand.Cards.Count - 1; j++)
    {

        if (players[i].Hand.Cards[j].Value < lastCardPlayed.Value)
        {
            break;
        }
        else
        {
            Console.WriteLine($"");
            Console.WriteLine($"{players[i].Name} has lost. Game over!");
            Console.WriteLine($"");
            QuittingSession();
            break;
        }
    }
}

void DisplayHand()
{
    Console.WriteLine();
    Console.WriteLine($"{players[i].Name}'s Turn.");

    if (players[i].Hand.Cards.Count == 0)
        QuittingSession();

    DisplayDeck(players[i].Hand.Cards);
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

    if (towerOfPower.Cards.Count == 0)
    {
        Console.WriteLine();
        Console.WriteLine("Nothing in the Tower");
    }
    else
    {
        var card = towerOfPower?.Cards.Last();
        Console.WriteLine();
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
        if (CheckCardAgainstTowerOfPower(Cards.KillingBlow))
        {
            Console.WriteLine($"{players[i].Name} played '{Cards.KillingBlow.Name}'");
            KillingBlowAction();
            PlayCard(Cards.KillingBlow);
        }
    }

    if (userInput == Cards.HiddenStrength.Value.ToString())
    {
        if (CheckCardAgainstTowerOfPower(Cards.HiddenStrength))
        {
            Console.WriteLine($"{players[i].Name} played '{Cards.HiddenStrength.Name}'");
            HiddenStrengthAction();
            PlayCard(Cards.HiddenStrength);
        }
    }

    if (userInput == Cards.PrecisionStrike.Value.ToString())
    {
        if (CheckCardAgainstTowerOfPower(Cards.PrecisionStrike))
        {
            Console.WriteLine($"{players[i].Name} played '{Cards.PrecisionStrike.Name}'");
            PrecisionStrikeAction();
            PlayCard(Cards.PrecisionStrike);
        }
    }

    if (userInput == Cards.Feint.Value.ToString())
    {
        if (CheckCardAgainstTowerOfPower(Cards.Feint))
        {
            Console.WriteLine($"{players[i].Name} played '{Cards.Feint.Name}'");
            PlayCard(Cards.Feint);
            FeintAction();
        }
    }

    if (userInput == Cards.CopyCat.Value.ToString())
    {
        if (CheckCardAgainstTowerOfPower(Cards.CopyCat))
        {
            Console.WriteLine($"{players[i].Name} played '{Cards.CopyCat.Name}'");
            CopyCatAction();
        }
    }

    if (userInput == Cards.ChangeStance.Value.ToString())
    {
        if (CheckCardAgainstTowerOfPower(Cards.ChangeStance))
        {
            Console.WriteLine($"{players[i].Name} played '{Cards.ChangeStance.Name}'");
            ChangeStanceAction();
            PlayCard(Cards.ChangeStance);
        }
    }

    if (userInput == Cards.Disarm.Value.ToString())
    {
        if(CheckCardAgainstTowerOfPower(Cards.Disarm))
        {
            Console.WriteLine($"{players[i].Name} played '{Cards.Disarm.Name}'");
            DisarmAction();
            PlayCard(Cards.Disarm);
        }
    }

    if (userInput == Cards.Backstab.Value.ToString())
    {
        if (CheckCardAgainstTowerOfPower(Cards.Backstab))
        {
            Console.WriteLine($"{players[i].Name} played '{Cards.Backstab.Name}'");
            BackstabAction();
            PlayCard(Cards.Backstab);
        }
    }

    if (userInput == Cards.Dodge.Value.ToString())
    {
        if (CheckCardAgainstTowerOfPower(Cards.Dodge))
        {
            Console.WriteLine($"{players[i].Name} played '{Cards.Dodge.Name}'");
            PlayCard(Cards.Dodge);
        }
    }

    if (userInput == "q")
        QuittingSession();
}

bool CheckCardAgainstTowerOfPower(Card card)
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

int setCurrentPlayerIndex () => i == 0 ? 1 : 0;

void PlayCard(Card card)
{
    MoveCardBetweenDecks(card, players[i].Hand, towerOfPower);
    i = setCurrentPlayerIndex();
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
    if (fromDeck.Cards.Contains(card))
    {
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

    Console.WriteLine($"{card.Value}: {card.Name}, is not an available choice.");
    return false;
}
#endregion

#region Card Actions
void KillingBlowAction()
{
    // Bury a face up card.
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
            QuittingSession();
    }
}

void HiddenStrengthAction()
{
    // Draw a random buried card.
    MoveRandomCardBetweenDecks(buriedPile, players[i].Hand, true);
}

void PrecisionStrikeAction()
{
    // Put a face up card in your hand.
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
            if (MoveCardBetweenDecks(Cards.KillingBlow, towerOfPower, players[i].Hand))
            {
                precisionStrikeChoice = false;
            };
            return;
        }

        if (precisionStrikeCardChoice == Cards.HiddenStrength.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.HiddenStrength, towerOfPower, players[i].Hand));
            {
                precisionStrikeChoice = false;
            }
            return;
        }

        if (precisionStrikeCardChoice == Cards.PrecisionStrike.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.PrecisionStrike, towerOfPower, players[i].Hand))
            {
                precisionStrikeChoice = false;
            }
            return;
        }

        if (precisionStrikeCardChoice == Cards.Feint.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Feint, towerOfPower, players[i].Hand))
            {
                precisionStrikeChoice = false;
            };
            return;
        }

        if (precisionStrikeCardChoice == Cards.CopyCat.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.CopyCat, towerOfPower, players[i].Hand))
            {
                precisionStrikeChoice = false;
            };
            return;
        }

        if (precisionStrikeCardChoice == Cards.ChangeStance.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.ChangeStance, towerOfPower, players[i].Hand))
            {
                precisionStrikeChoice = false;
            };
            return;
        }

        if (precisionStrikeCardChoice == Cards.Disarm.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Disarm, towerOfPower, players[i].Hand))
            {
                precisionStrikeChoice = false;
            };
            return;
        }

        if (precisionStrikeCardChoice == Cards.Backstab.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Backstab, towerOfPower, players[i].Hand))
            {
                precisionStrikeChoice = false;
            };
            return;
        }

        if (precisionStrikeCardChoice == Cards.Dodge.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Dodge, towerOfPower, players[i].Hand))
            {
                precisionStrikeChoice = false;
            };
            return;
        }

        if (precisionStrikeCardChoice == "q")
            QuittingSession();
    }
}

void FeintAction()
{
    // Play a random card from your target's hand. Ignore the ability.
    MoveRandomCardBetweenDecks(players[i].Hand, towerOfPower, true);
};

void CopyCatAction()
{
    // Copy the ability of any face up card.
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

        if (CopyCatCardChoice == Cards.HiddenStrength.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.HiddenStrength))
            {
                HiddenStrengthAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }

        if (CopyCatCardChoice == Cards.PrecisionStrike.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.PrecisionStrike))
            {
                PrecisionStrikeAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }

        if (CopyCatCardChoice == Cards.Feint.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.Feint))
            {
                FeintAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }

        if (CopyCatCardChoice == Cards.CopyCat.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.CopyCat))
            {
                Console.WriteLine("You can't pick CopyCat to Copy!");
            }
        }

        if (CopyCatCardChoice == Cards.ChangeStance.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.ChangeStance))
            {
                ChangeStanceAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }

        if (CopyCatCardChoice == Cards.Disarm.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.Disarm))
            {
                DisarmAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }

        if (CopyCatCardChoice == Cards.Backstab.Value.ToString())
        {
            if (towerOfPower.Cards.Contains(Cards.Backstab))
            {
                BackstabAction();
                CopyCatChoice = false;
                PlayCard(Cards.CopyCat);
                return;
            }
        }

        if (CopyCatCardChoice == Cards.Dodge.Value.ToString())
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

    }
};

void ChangeStanceAction()
{
    var otherPlayerIndex = setCurrentPlayerIndex();

    var noCardOverFive = true;

    for (int k = 0; k < players[otherPlayerIndex].Hand.Cards.Count - 1; k++)
    {
        if (players[otherPlayerIndex].Hand.Cards[k].Value > Cards.ChangeStance.Value)
        {   
            noCardOverFive = false;
            break;
        }
    }
    if (noCardOverFive)
    {
        Console.WriteLine($"");
        Console.WriteLine($"{players[i].Name} has lost. Game over!");
        Console.WriteLine($"");
        QuittingSession();
    }
};

void DisarmAction()
{
    // Target buries a card at random.
    var otherPlayerIndex = setCurrentPlayerIndex();
    MoveRandomCardBetweenDecks(players[otherPlayerIndex].Hand, buriedPile, true);
};

void BackstabAction()
{
    // Target gives you a card of their choice.
    var otherPlayerIndex = setCurrentPlayerIndex();

    if (players[otherPlayerIndex].Hand.Cards.Count == 0)
    {
        Console.WriteLine($"Nothing in the {players[otherPlayerIndex].Name}'s Hand to take.");
        return;
    }

    var backstabChoice = true;

    while (backstabChoice)
    {
        Console.WriteLine($"Choose a card from your hand to give to {players[i].Name}");
        DisplayDeck(players[otherPlayerIndex].Hand.Cards);

        Console.WriteLine();

        var BackstabCardChoice = Console.ReadLine();

        if (BackstabCardChoice == Cards.KillingBlow.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.KillingBlow, players[otherPlayerIndex].Hand, players[i].Hand))
            {
                backstabChoice = false;
                return;
            };
        }

        if (BackstabCardChoice == Cards.HiddenStrength.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.HiddenStrength, players[otherPlayerIndex].Hand, players[i].Hand))
            {
                backstabChoice = false;
                return;
            };
        }

        if (BackstabCardChoice == Cards.PrecisionStrike.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.PrecisionStrike, players[otherPlayerIndex].Hand, players[i].Hand))
            {
                backstabChoice = false;
                return;
            };
        }

        if (BackstabCardChoice == Cards.Feint.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Feint, players[otherPlayerIndex].Hand, players[i].Hand))
            {
                backstabChoice = false;
                return;
            };
        }

        if (BackstabCardChoice == Cards.ChangeStance.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.ChangeStance, players[otherPlayerIndex].Hand, players[i].Hand))
            {
                backstabChoice = false;
                return;
            };
        }

        if (BackstabCardChoice == Cards.Disarm.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Disarm, players[otherPlayerIndex].Hand, players[i].Hand))
            {
                backstabChoice = false;
                return;
            };
        }

        if (BackstabCardChoice == Cards.Backstab.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Backstab, players[otherPlayerIndex].Hand, players[i].Hand))
            {
                backstabChoice = false;
                return;
            };
        }

        if (BackstabCardChoice == Cards.Dodge.Value.ToString())
        {
            if (MoveCardBetweenDecks(Cards.Dodge, players[otherPlayerIndex].Hand, players[i].Hand))
            {
                backstabChoice = false;
                return;
            };
        }

        if (BackstabCardChoice == "q")
            QuittingSession();
    }
}
#endregion

#region Game
while (escape)
{
    StartGame();
    DisplayGameState();
    DisplayHand();
    PlayerInputOptions();
    CheckWinCondition();
}
#endregion

