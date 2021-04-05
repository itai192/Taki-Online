using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Elo
    {
        /// <summary>
        /// score in this context means probability to win
        /// where 1 is an expected win
        /// 0.5 is an expected draw
        /// and 0 is an expected loss
        /// </summary>
        const int KFACTOR = 32;
        const int EXPONENTDIVISOR = 400;
        const int BASEOFEXPONENT = 10;
        /// <summary>
        /// Calculates and returns the expected score of a player against an opponent using their elo rating
        /// </summary>
        /// <param name="PlayerElo">The elo rating of the player who we calculate his expected score</param>
        /// <param name="OponentElo">The elo rating of the opponent of the player who we calculate his expected score</param>
        /// <returns>the expected score of the player</returns>
        public static double ExpectedScore(int PlayerElo, int OponentElo)
        {
            return 1 / (1 + Math.Pow(BASEOFEXPONENT, (double)(OponentElo - PlayerElo) /400));
        }
        /// <summary>
        /// Calculates the change in elo rating of a player against an oppenent using their elos and the player's score
        /// </summary>
        /// <param name="PlayerElo">The elo rating of the player who we calculate his change in elo rating</param>
        /// <param name="OponentElo">The elo rating of the opponent of the player who we calculate his change in elo rating</param>
        /// <param name="score">the score of the player in a game between the two players</param>
        /// <returns>the change in elo rating of the player</returns>
        public static int ChangeInRating(int PlayerElo, int OponentElo, double score)
        {
            return (int)Math.Round(KFACTOR * (score - ExpectedScore(PlayerElo, OponentElo)));
        }
    }
}
