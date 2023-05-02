using System.Collections.Generic;

namespace Service.Network
{
    public class Match 
    {
        private readonly List<Player.Player> _players = new();
        
        public Match(Player.Player player) => _players.Add(player);

        public IReadOnlyList<Player.Player> Players => _players; 

        public void AddPlayer(Player.Player player) => _players.Add(player);

        public void StartGame() => _players.ForEach(player => player.TargetLoadGame());
    }
}