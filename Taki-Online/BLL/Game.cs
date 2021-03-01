using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Card.cs")]
[assembly: InternalsVisibleTo("Cards.cs")]
namespace BLL
{
    public partial class Game
    {
        const int NUMCARDS=8;
        private Stack<Card> deck;
        private Stack<Card> pile;
        private List<GamePlayer> players;
        public int turn { get; private set; }
        private Card activeCard;
        public bool hasLastPlayerPutCard
        { get; internal set;}
        public Card leadingCard
        {
            get { return pile.Peek(); }//temporery
        }
        internal Player GetPlayerTurn()
        {
            return players[turn];
        }
        public bool order {get; internal set; }//positive or negative relative to the order
        public int penelty {get; internal set;}//extra cards when you draw
        internal void ChangeActiveCard(Card card)
        {
            activeCard = card;
            if(card!=null)
            {
                activeCard.StartAbility(this);
            }
        }
        public Game()
        {
            order = true;
            this.players = new List<GamePlayer>();
            activeCard = null;
            turn = 0;
            pile = new Stack<Card>();
            hasLastPlayerPutCard = false;
            foreach(Color c in Enum.GetValues(typeof(Color)))
            {
                if(c!=Color.none)
                {
                    for(int i = 1;i<=9;i++)
                    {
                        Card card = new NumberCard(c, i);
                    }
                }
            }
            pile.Push(new BLL.NumberCard(Color.yellow, 5));//temporery
        }
        internal void TakeCardsFromDeck(Player p)
        {
            for(int i = 0; i<=penelty;i++)
            {
                if(deck.Count==0)
                {
                    Reshuffle();
                }
                Card c = deck.Pop();
                if(p is GamePlayer)
                {
                    GamePlayer gp=(GamePlayer)p;
                    gp.AddCardToHand(c);
                }
                BroadcastAction(new Action(ActionType.DrawCard,c,p));
            }
            penelty = 0;
            NextTurn();
        }
        internal void NextTurn()
        {
            turn = (order ? (turn + 1) : (turn - 1 + players.Count)) % players.Count;
            hasLastPlayerPutCard = false;
        }
        private void BroadcastAction(Action action)
        {
            Action lessInfoAction=null;//for actions which players don't need to know about
            if(action.type==ActionType.DrawCard)
            {
                lessInfoAction = new Action(action.type, null, action.player);
            }
            foreach (GamePlayer p in players)
            {
                if (action.type == ActionType.DrawCard && p!= action.player)
                    p.AddAction(lessInfoAction);
                else
                {
                    p.AddAction(action);
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
                GamePlayer player = (GamePlayer)action.player;
                if (player.HasCard(action.card))
                {
                    pile.Push(player.TakeCard(action.card));
                    BroadcastAction(action);
                    if (activeCard == null)
                    {
                        ChangeActiveCard(leadingCard);
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
        public Player AddPlayer()
        {
            GamePlayer player = new GamePlayer(this);
            players.Add(player);
            for(int i = 0;i<NUMCARDS;i++)
            {
                TakeCardsFromDeck(player);
            }
            return player;
        }
        
    }
    
}
