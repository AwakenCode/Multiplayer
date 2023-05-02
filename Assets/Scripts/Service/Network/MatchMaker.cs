using System.Collections.Generic;

namespace Service.Network
{
    public class MatchMaker : IService
    {
        private readonly Dictionary<string, Match> _matches = new();
        
        public void JoinMatch(string matchId, Player.Player player, out bool isSuccess)
        {
            if (_matches.ContainsKey(matchId) == false)
            {
                isSuccess = false;
                return;
            }

            _matches[matchId].AddPlayer(player);
            isSuccess = true;
        }
        
        public void CreateMatch(string matchId, Player.Player player, out bool isSuccess)
        {
            if (_matches.ContainsKey(matchId))
            {
                isSuccess = false;
                return;
            }

            var match = new Match(player);
            _matches.Add(matchId, match);
            isSuccess = true;
        }

        public void StartGame(string matchId) => _matches[matchId].StartGame();
    }
}