# Sean McCoy's Duel Instructions
2 - 4 player card game.

## Dev Notes
Written in C# as a console app. To run it download and run via visual Studio 2022 and press F5.

Orginal Game creator is Sean McCoy - please check out his work at [here](https://www.tuesdayknightgames.com/)

## Introduction
You’ve been walking for days in an endless plain. You vaguely remember a life before this, but
now the golden fields are all that remain. In the distance, you spot your first traveller in this
strange land, a stranger to you in all ways, save one ­ their sword.

Duel is a 2 player card game, where the object of the game is to be the last man standing. You
take turns playing cards and you lose if you can no longer play a card.

## Setup
**Two player:** Each player chooses a color and takes the 0­ - 7 cards and the trump (*) card to
create their hand. To determine who goes first, each player reveals a random card from their
hand. Lowest card picks who goes first. Break a tie by drawing another card. Players then
shuffle their hands and place a random card face­down in the bury pile (they may look at what
card they buried). Then the game begins.

## How to Play
Players take turns playing a card from their hand to the play tower. Cards played must have a
lower number than the previous card (unless a card ability says otherwise). After the card is
played, the card’s ability occurs. Cards’ abilities cannot affect themselves (Example: a 0 card
cannot bury itself). Finally, the turn ends, play proceeds clockwise, and the next player plays a
card.

## Winning
If a player ever cannot play a card, either because they do not have a card lower than the
previously played card, because a card’s ability prevents them from playing a card, or because
they are out of cards, they lose the round. 

During setup for the next round, after players bury a card, the first player to lose chooses who 
goes first. The winner is the last player still in the round. Games are typically best of five rounds.

## Cards
### 0: The Killing Blow ­ 
**Bury a face up card.**
The 0 card is the lowest playable card. The only card that can be played after a zero is the trump (*) card.
the player must bury a face up card. You may not play a 0 card immediately after a trump
(*) card is played. Killing Blow may not be used on itself, meaning you cannot bury the card you
just played.

### 1: Hidden Strength
**Draw a buried card.**
The player must draw a buried card from the buried pile. Their opponent may shuffle the buried pile before 
the player draws.

### 2: Precision Strike
**Put a face up card in your hand.**
The player must put a face up card into their hand.

### 3: “Feint
*­*Play an opponent’s random card and ignore the ability.**
Choose a random card from your opponent and play it. The opponent’s card’s ability does not take affect. Play now
continues with the opponent responding to the power (#) of that card.

### 4: Copycat
**Copy the ability of any face up card.**
The card gains the ability of any face up card and the ability immediately takes effect. 
(Note: the card does not gain the power (#) of the face up card, only the ability.) You may not use Copycat on itself.

### 5: Change Stance
**The next card played must be greater than 5.**
If opponent cannot play a card greater than 5, they lose the round. Trump (*) cards and X cards can both be played in
response to a 5. In the case of the X card, the next player will still need to play a card greater
than 5. Note: This card only affects the next card. After a card greater than 5 has been played,
play returns to normal, with lower cards being played every turn.

### 6: Disarm
**Opponent buries a card at random.**
Choose a random card from your opponent’s hand; they place that card face down in the buried pile.

### 7: Backstab 
**Opponent gives you a card of their choice.**
Opponent chooses a card from their hand and gives it to you.

### *: “Dodge
**Trumps the last card played. Then counts as an 8.**
Can be played after any card; the next card played now plays as if an 8 was just played. 
(Note: you cannot play a 0 aftera Trump (*) card.)