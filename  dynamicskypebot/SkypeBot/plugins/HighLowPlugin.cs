using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Windows.Forms;
using SKYPE4COMLib;
using System.Runtime.Serialization;
using log4net;

namespace SkypeBot.plugins {
    public class HighLowPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Random random;
        private HashSet<HighLowGame> games;

        public String name() { return "High-Low Plugin"; }

        public String help() { return "!highlow <start/high/low/equal/highscore>"; }

        public String description() { return "Lets people play High-Low. Stores highscores on a per-chat basis."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public HighLowPlugin() {
            if (PluginSettings.Default.HighlowScores == null)
                PluginSettings.Default.HighlowScores = new Hashtable();

            random = new Random();
            games = new HashSet<HighLowGame>();
        }

        public void load() {
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match trigger = Regex.Match(message.Body, @"^!highlow (start|highscore|high|low|equal)", RegexOptions.IgnoreCase);
            if (trigger.Success) {
                String triggerText = trigger.Groups[1].Value.ToLower();

                if (triggerText.Equals("start")) {
                    HighLowGame game = new HighLowGame(message.Sender.Handle, message.Chat.Name);
                    String output = "";

                    if (games.Contains(game)) {
                        game = games.Single<HighLowGame>((hlg) => { return hlg.Equals(game); });
                        output += "You're already in a game!\n";
                    } else {
                        games.Add(game);
                    }

                    output += game.Status;
                    output += "Is the next card high, low or do you dare guess equal (scores 5x)?\nType !highlow <guess> to guess.";

                    message.Chat.SendMessage(output);
                    return;
                }
                else if (triggerText.Equals("highscore")) {
                    if (!PluginSettings.Default.HighlowScores.Contains(message.Chat.Name))
                        message.Chat.SendMessage("No highscore exists for this chat.");
                    else {
                        HighScoreEntry score = PluginSettings.Default.HighlowScores[message.Chat.Name] as HighScoreEntry;
                        message.Chat.SendMessage(
                            String.Format(
                                "High-Low highscore: {0} points by {1}.",
                                score.Score,
                                score.Name
                            )
                        );
                    }
                }
                else {
                    HighLowGame game = new HighLowGame(message.Sender.Handle, message.Chat.Name);

                    if (!games.Contains(game)) {
                        message.Chat.SendMessage("You aren't currently in a High-Low game! Start one with !highlow start.");
                        return;
                    }

                    game = games.Single<HighLowGame>((hlg) => { return hlg.Equals(game); });

                    Boolean success;

                    switch (triggerText) {
                        case "high":
                            success = game.GuessHigh();
                            break;
                        case "low":
                            success = game.GuessLow();
                            break;
                        case "equal":
                            success = game.GuessEqual();
                            break;
                        default:
                            log.Error("Reached default case? This shouldn't happen!");
                            return;
                    }

                    String output = "";

                    if (!success) {
                        output += String.Format("Incorrect! You got a total of {0} points.", game.Score);

                        Boolean isHighscore = false;
                        if (!PluginSettings.Default.HighlowScores.Contains(message.Chat.Name))
                            isHighscore = true;
                        else {
                            HighScoreEntry score = PluginSettings.Default.HighlowScores[message.Chat.Name] as HighScoreEntry;
                            if (score.Score < game.Score)
                                isHighscore = true;
                        }

                        if (isHighscore) {
                            output += "\nYou've set a new high score for this chat!";
                            PluginSettings.Default.HighlowScores[message.Chat.Name] = new HighScoreEntry(game.Score, message.Sender.Handle);
                            PluginSettings.Default.Save();
                        }

                        games.Remove(game);
                    }
                    else {
                        output += "Correct!\n";

                        game.nextCard();
                        game.Score += (triggerText.Equals("equal") ? 5 : 1);

                        output += game.Status;
                        output += "Is the next card high, low or do you dare guess equal (scores 5x)?\nType !highlow <guess> to guess.";
                    }

                    message.Chat.SendMessage(output);
                }
            }
        }

        private class HighLowGame {
            private String player;
            private String chat;
            private int streak;
            private LinkedList<Card> deck;
            private Card lastCard;

            public HighLowGame(String player, String chat) {
                this.player = player;
                this.chat = chat;
                this.streak = 0;

                Card[] tmpDeck = new Card[52];
                int i = 0;

                foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit))) {
                    foreach (Card.Value value in Enum.GetValues(typeof(Card.Value))) {
                        tmpDeck[i++] = new Card(suit, value);
                    }
                }

                Random random = new Random();

                for (i = 51; i > 0; i--) {
                    int n = random.Next(0, i);
                    Card tmp = tmpDeck[i];
                    tmpDeck[i] = tmpDeck[n];
                    tmpDeck[n] = tmp;
                }

                this.deck = new LinkedList<Card>(tmpDeck);
                this.lastCard = deck.First.Value;
                deck.RemoveFirst();
            }

            public String Player {
                get { return player; }
            }

            public String Chat {
                get { return chat; }
            }

            public int Score {
                get { return streak; }
                set { streak = value; }
            }

            public Card LastCard {
                get { return lastCard; }
            }

            public override int GetHashCode() {
                return (chat + player).GetHashCode();
            }

            public Boolean GuessHigh() {
                return deck.First.Value.isHigherThan(lastCard);
            }

            public Boolean GuessLow() {
                return deck.First.Value.isLowerThan(lastCard);
            }

            public Boolean GuessEqual() {
                return deck.First.Value.isEqualTo(lastCard);
            }

            public void nextCard() {
                lastCard = deck.First.Value;
                deck.RemoveFirst();

                if (deck.Count == 0) {
                    Card[] tmpDeck = new Card[52];
                    int i = 0;

                    foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit))) {
                        foreach (Card.Value value in Enum.GetValues(typeof(Card.Value))) {
                            tmpDeck[i++] = new Card(suit, value);
                        }
                    }

                    Random random = new Random();

                    for (i = 51; i > 0; i--) {
                        int n = random.Next(0, i);
                        Card tmp = tmpDeck[i];
                        tmpDeck[i] = tmpDeck[n];
                        tmpDeck[n] = tmp;
                    }

                    deck = new LinkedList<Card>(tmpDeck);
                }
            }

            public String Status {
                get {
                    return String.Format(
                        "Current card: {0}\n{1}",
                        lastCard.ToString(),
                        streak > 0 ? String.Format("You've got {0} points.\n", streak) : ""
                    );
                }
            }

            public override bool Equals(object obj) {
                // If parameter is null return false.
                if (obj == null) {
                    return false;
                }

                // If parameter cannot be cast to Point return false.
                HighLowGame p = obj as HighLowGame;
                if ((System.Object)p == null) {
                    return false;
                }

                // Return true if the fields match:
                return (player == p.Player) && (chat == p.Chat);
            }
        }

        private class Card {
            public enum Suit { Clubs, Diamonds, Hearts, Spades };
            public enum Value { Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };

            private Suit _suit;
            private Value _value;

            public Card(Suit suit, Value value) {
                _suit = suit;
                _value = value;
            }

            public Suit suit {
                get { return _suit; }
            }

            public Value value {
                get { return _value; }
            }

            public Boolean isHigherThan(Card card) {
                return card.value <= _value;
            }

            public Boolean isLowerThan(Card card) {
                return card.value >= _value;
            }

            public Boolean isEqualTo(Card card) {
                return card.value == _value;
            }

            public override String ToString() {
                return value.ToString() + " of " + suit.ToString();
            }
        }

        [Serializable]
        private class HighScoreEntry {
            private int score;
            private String name;

            public HighScoreEntry(int score, String name) {
                this.score = score;
                this.name = name;
            }

            public int Score {
                get { return score; }
            }

            public String Name {
                get { return name; }
            }
        }
    }
}   