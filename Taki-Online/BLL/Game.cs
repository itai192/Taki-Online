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
        public bool GameEnded
        {
            get;
            private set;
        }
        public GameRoom gameRoom
        {
            get;
            private set;
        }
        const int NUMCARDS=8;
        private Stack<Card> deck;
        private Stack<Card> pile;
        private List<Player> players;
        public int turn { get; private set; }
        private Card activeCard;
        public bool hasLastPlayerPutCard
        { get; internal set;}
        public Card leadingCard
        {
            get { return pile.Peek(); }//temporery
        }
        public bool order {get; internal set; }//positive or negative relative to the order
        public int penelty {get; internal set;}//extra cards when you draw
        internal void ChangeActiveCard()
        {
            activeCard = leadingCard;
            if(activeCard!=null)
            {
                activeCard.StartAbility(this);
            }
        }
        internal void ClearActiveCard()
        {
            activeCard = null;
            if(IsEnded())
            {
                EndProgression();
            }
        }
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
        private List<Player> SortedPlayerList()
        {
            List<Player> ret = new List<Player>(players);
            ret.Sort(ComparePlayerCards);
            return ret;
        }
        private int ComparePlayerCards(Player x, Player y)
        {
            return x.numberOfCards - y.numberOfCards;
        }
        internal Player GetPlayerTurn()
        {
            return players[turn];
        }
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
            this.ShuffleDeck();
            pile.Push(deck.Pop());
        }
        internal void TakeCardsFromDeck(Player p)
        {
            for(int i = 0; i<=penelty;i++)
            {
                if(deck.Count==0)
                {
                    Reshuffle();
                }
                if (deck.Count > 0)
                {
                    Card c = deck.Pop();
                    p.AddCardToHand(c);
                    BroadcastAction(new Action(ActionType.DrawCard, c, p));
                }
                else
                {
                    throw new Exception("not enough cards in deck");
                }
            }
            penelty = 0;
            NextTurn();
        }
        public void UseEndAbility(Player player)
        {
            if(GetPlayerTurn().Equals(player))
            {
                if (activeCard != null)
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
        internal void NextTurn()
        {
            turn = (order ? (turn + 1) : (turn - 1 + players.Count)) % players.Count;
            hasLastPlayerPutCard = false;
            Broadcast(new ChangeTurnBroadcast(turn));
        }
        private void Broadcast(IPlayerBroadcast broadcast)
        {
            foreach(Player p in players)
            {
                p.AddBroadcast(broadcast);
            }
        }
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
        public void TryDoAction(Action action)
        {
            if(action.player==GetPlayerTurn())
            {
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
                    if (action.type == ActionType.DrawCard)
                    {
                        TakeCardsFromDeck(action.player);
                    }
                    if(action.type==ActionType.putCard)
                    {
                        if(action.card.CanBePutOn(leadingCard))
                        {
                            PutCard(action);
                        }
                        else
                        {
                            throw action.card.WhereCanPutCard();
                        }
                    }
                }
            }
            else
            {
                throw new Exception("You can't play in another player's turn");
            }
        }
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
        public Player AddPlayer(User user)
        {
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
            UpdatePlayerListsInAllPlayers();
            return player;
        }
        public List<SimplePlayer> GetSimplePlayerList()
        {
            List<SimplePlayer> list = new List<SimplePlayer>();
            foreach(Player p in players)
            {
                list.Add(p.ToSimplePlayer());
            }
            return list;
        }
        public void UpdatePlayerListsInAllPlayers()
        {
            foreach(Player p in players)
            {
                p.UpdatePlayerList(GetSimplePlayerList());
            }
        }
        
    }
    
}
