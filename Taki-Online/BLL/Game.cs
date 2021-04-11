using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DAL;
[assembly: InternalsVisibleTo("Card.cs")]
[assembly: InternalsVisibleTo("Cards.cs")]
namespace BLL
{
    public partial class Game
    {
        //a boolean describing whether this game has ended
        public bool GameEnded
        {
            get;
            private set;
        }
        //the game room object containg this game
        public GameRoom gameRoom
        {
            get;
            private set;
        }
        //number of cards in hand
        const int NUMCARDS=8;
        //a stack of cards describing the deck
        private Stack<Card> deck;
        //a stack of cards describing the pile
        private Stack<Card> pile;
        //the list of players in order of turns when order is true
        private List<Player> players;
        //the current turn of the game
        public int turn { get; private set; }
        //the active card, can be null, if not null, processes player's actions
        private Card activeCard;
        //a boolean which describes whether the last player has put a card yet
        public bool hasLastPlayerPutCard
        { get; internal set;}
        //the game's leading card, top of the pile
        public Card leadingCard
        {
            get { return pile.Peek(); }
        }
        //the order of game, true means the next player is the next in the list and false, previous
        public bool order {get; internal set; }
        //extra cards when you draw
        public int penelty {get; internal set;}
        /// <summary>
        /// changes the active card of the game to the leading card and trigger's it's start ability
        /// </summary>
        internal void ChangeActiveCard()
        {
            activeCard = leadingCard;
            if(activeCard!=null)
            {
                activeCard.StartAbility(this);
            }
        }
        /// <summary>
        /// clears the active card, and if game has ended, does the end progression
        /// </summary>
        internal void ClearActiveCard()
        {
            activeCard = null;
            if(IsEnded())
            {
                EndProgression();
            }
        }
        /// <summary>
        /// returns whether there is a winner to this game
        /// </summary>
        private bool IsEnded()
        {
            foreach(Player p in players)
            {
                if(p.numberOfCards==0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// a method which ends the game
        /// </summary>
        private void EndProgression()
        {
            //change room status
            gameRoom.status = GameStatus.Ended;
            //change elo
            List<Player> sortedPlayers = SortedPlayerList();
            List<int> eloChanges = new List<int>();
            for(int i =0;i<sortedPlayers.Count;i++)
            {
                eloChanges.Add(0);
            }
            //SME algorithm
            for(int i =0;i<sortedPlayers.Count-1;i++)
            {
                //winner part
                if (sortedPlayers[i].numberOfCards != sortedPlayers[i + 1].numberOfCards)
                    eloChanges[i] += Elo.ChangeInRating(sortedPlayers[i].user.elo, sortedPlayers[i + 1].user.elo, 1);
                else//if draw
                {
                    eloChanges[i] += Elo.ChangeInRating(sortedPlayers[i].user.elo, sortedPlayers[i + 1].user.elo, 0.5);
                }
                //loser part
                if (sortedPlayers[sortedPlayers.Count - 1 - i].numberOfCards != sortedPlayers[sortedPlayers.Count - 2 - i].numberOfCards)
                    eloChanges[sortedPlayers.Count - 1 - i] += Elo.ChangeInRating(sortedPlayers[sortedPlayers.Count - 1 - i].user.elo, sortedPlayers[sortedPlayers.Count - 2 - i].user.elo, 0);
                else//if draw
                {
                    eloChanges[sortedPlayers.Count - 1 - i] += Elo.ChangeInRating(sortedPlayers[sortedPlayers.Count - 1 - i].user.elo, sortedPlayers[sortedPlayers.Count - 2 - i].user.elo, 0.5);
                }
            }
            //changeXp
            List<int> xpChanges = new List<int>();
            for(int i =0;i<sortedPlayers.Count;i++)
            {
                xpChanges.Add(50 / (sortedPlayers[i].numberOfCards + 1));
            }
            //updateAll
            for(int i =0;i<sortedPlayers.Count;i++)
            {
                Player p = sortedPlayers[i];
                UsersInGamesDal.UpdateUserInGame(p.user.username, gameRoom.GameID, xpChanges[i], eloChanges[i], i == 0,p.numberOfCards);
                p.user.xp += xpChanges[i];
            }
            //End Game For All
            Broadcast(new EndGameBroadcast());
        }
        /// <summary>
        /// returns the player list sorted by card amount
        /// </summary>
        private List<Player> SortedPlayerList()
        {
            List<Player> ret = new List<Player>(players);
            ret.Sort(ComparePlayerCards);
            return ret;
        }
        /// <summary>
        /// compares two player's card amount, used for sorting
        /// </summary>
        private int ComparePlayerCards(Player x, Player y)
        {
            return x.numberOfCards - y.numberOfCards;
        }
        /// <summary>
        /// returns the player whose it is it's turn now
        /// </summary>
        internal Player GetPlayerTurn()
        {
            return players[turn];
        }
        /// <summary>
        /// a game constructor, gets the game room which contains it
        /// </summary>
        public Game(GameRoom gameRoom)
        {
            this.gameRoom = gameRoom;
            order = true;
            this.players = new List<Player>();
            activeCard = null;
            turn = 0;
            pile = new Stack<Card>();
            deck = new Stack<Card>();
            hasLastPlayerPutCard = false;
            //set up deck
            foreach(Color c in Enum.GetValues(typeof(Color)))
            {
                if(c!=Color.none)
                {
                    for(int i = 1;i<=9;i++)
                    {
                        if (i != 2)
                        {
                            deck.Push(new NumberCard(c, i));
                            deck.Push(new NumberCard(c, i));
                        }
                    }
                }
            }
            //shuffle deck
            this.ShuffleDeck();
            //put first card on pile
            pile.Push(deck.Pop());
        }
        //takes cards from deck to player 
        internal void TakeCardsFromDeck(Player p)
        {
            //for each penelty point
            for(int i = 0; i<=penelty;i++)
            {
                //if deck is over
                if(deck.Count==0)
                {
                    Reshuffle();
                }
                if (deck.Count > 0)
                {
                    //take a card
                    Card c = deck.Pop();
                    p.AddCardToHand(c);
                    BroadcastAction(new Action(ActionType.DrawCard, c, p));
                }
                else
                {
                    //very rare if not enough cards in deck
                    throw new Exception("not enough cards in deck");
                }
            }
            //resets the penelty and goes to next turn
            penelty = 0;
            NextTurn();
        }
        /// <summary>
        /// tries to use end ability using player
        /// </summary>
        public void UseEndAbility(Player player)
        {
            //if the turn of player
            if(GetPlayerTurn().Equals(player))
            {
                if (activeCard != null)//if exists active card
                {
                    activeCard.EndAbility(this);
                }
                else
                {
                    throw new Exception("There is no card to end it's ability");
                }
            }
            else
            {
                throw new Exception("You can't end an ability not in your turn");
            }
        }
        /// <summary>
        /// goes to next turn according to game order
        /// </summary>
        internal void NextTurn()
        {
            //turn cycles, if order true, to next player in list, else to previous
            turn = (order ? (turn + 1) : (turn - 1 + players.Count)) % players.Count;
            hasLastPlayerPutCard = false;
            //broadcasts the change in turn
            Broadcast(new ChangeTurnBroadcast(turn));
        }
        /// <summary>
        /// a method that broadcasts broadcasts to all players in game
        /// </summary>
        private void Broadcast(IPlayerBroadcast broadcast)
        {
            foreach(Player p in players)
            {
                p.AddBroadcast(broadcast);
            }
        }
        /// <summary>
         /// a method that broadcasts action broadcasts to all players in game
         /// </summary>
        private void BroadcastAction(Action action)
        {
            ActionBroadcast broadcast = new ActionBroadcast(action);
            ActionBroadcast lessInfoBroadcast=null;//for actions which players don't need to know about
            if(action.type==ActionType.DrawCard)
            {
                lessInfoBroadcast =new ActionBroadcast(new Action(action.type, null, action.player));
            }
            foreach (Player p in players)
            {
                if (action.type == ActionType.DrawCard && p!= action.player)
                    p.AddBroadcast(lessInfoBroadcast);
                else
                {
                    p.AddBroadcast(broadcast);
                }
            }
        }
        /// <summary>
        /// method that does action if leagal
        /// </summary>
        public void TryDoAction(Action action)
        {
            if(action.player==GetPlayerTurn())
            {
                //if exists active card to process actions
                if (activeCard != null)
                {
                    activeCard.ProcessPlayerAction(this, action);
                    if (action.type == ActionType.putCard)
                    {
                        BroadcastAction(action);
                        hasLastPlayerPutCard = true;
                    }
                }
                else 
                {
                    //if action is to draw
                    if (action.type == ActionType.DrawCard)
                    {
                        TakeCardsFromDeck(action.player);
                    }
                    //if action is to put
                    if (action.type==ActionType.putCard)
                    {
                        if(action.card.CanBePutOn(leadingCard))
                        {
                            PutCard(action);
                        }
                        else//if its ilegal to put card
                        {
                            throw action.card.WhereCanPutCard();
                        }
                    }
                }
            }
            //if not player's turn
            else
            {
                throw new Exception("You can't play in another player's turn");
            }
        }
        /// <summary>
        /// puts a card using action
        /// </summary>
        private void PutCard(Action action)
        {
            if (action.type == ActionType.putCard)
            {
                Player player = action.player;
                if (player.HasCard(action.card))
                {
                    pile.Push(player.TakeCard(action.card));
                    BroadcastAction(action);
                    if (activeCard == null)
                    {
                        ChangeActiveCard();
                    }
                }
                else
                    throw new Exception("Player doesn't have that card");
            }
            else
                throw new Exception("Action is not put card");

        }
        //reshuffles deck using cards in pile
        private void Reshuffle()
        {
            Card top = pile.Pop();
            List<Card> list = new List<Card>();
            
            while(pile.Count!=0)
            {
                list.Add(pile.Pop());
            }

            foreach(Card c in list)
            {
                deck.Push(c);
            }
            ShuffleDeck();
            pile.Push(top);
        }
        /// <summary>
        /// shuffles deck
        /// </summary>
        private void ShuffleDeck()
        {
            List<Card> l = new List<Card>();
            while(deck.Count !=0)
            {
                l.Add(deck.Pop());
            }
            Random random = new Random();
            while (l.Count!=0)
            {
                int num = random.Next(l.Count);
                deck.Push(l[num]);
                l.RemoveAt(num);
            }
        }
        /// <summary>
        /// returns whether a user is in game
        /// </summary>
        private bool IsUserInGame(User user)
        {
            foreach(Player p in players)
            {
                if(p.user.Equals(user))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// adds player to game, and returns it
        /// </summary>
        public Player AddPlayer(User user)
        {
            //returns already existing player
            if(IsUserInGame(user))
            {
                foreach(Player p in players)
                {
                    if(p.user.Equals(p))
                    {
                        return p;
                    }
                }
            }
            Player player = new Player(this,user);
            players.Add(player);
            //adds cards to player hand
            for(int i = 0;i<NUMCARDS;i++)
            {
                if (deck.Count > 0)
                    player.AddCardToHand(deck.Pop());
                else
                {
                    Reshuffle();
                    if (deck.Count > 0)
                        player.AddCardToHand(deck.Pop());
                    else
                    {
                        throw new Exception("not enough cards in deck");
                    }
                }
            }
            // updates player lists in players
            UpdatePlayerListsInAllPlayers();
            return player;
        }
        /// <summary>
        /// returns player list as simple player list
        /// </summary>
        public List<SimplePlayer> GetSimplePlayerList()
        {
            List<SimplePlayer> list = new List<SimplePlayer>();
            foreach(Player p in players)
            {
                list.Add(p.ToSimplePlayer());
            }
            return list;
        }
        /// <summary>
        /// updates userlists in all players
        /// </summary>
        public void UpdatePlayerListsInAllPlayers()
        {
            foreach(Player p in players)
            {
                p.UpdatePlayerList(GetSimplePlayerList());
            }
        }
        
    }
    
}
