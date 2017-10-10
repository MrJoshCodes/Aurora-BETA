using AuroraEmu.DI.Game.Players;
using System.Collections.Concurrent;

namespace AuroraEmu.Game.Players
{
    public class PlayerController : IPlayerController
    {
        private readonly ConcurrentDictionary<int, Player> _playersById;
        private readonly ConcurrentDictionary<int, string> _playerNamesById;
        private readonly ConcurrentDictionary<string, Player> _playersByName;

        public PlayerController()
        {
            _playersById = new ConcurrentDictionary<int, Player>();
            _playerNamesById = new ConcurrentDictionary<int, string>();
            _playersByName = new ConcurrentDictionary<string, Player>();
        }

        public Player GetPlayerById(int id)
        {
            if (_playersById.TryGetValue(id, out Player player))
                return player;

            Player result = Engine.MainDI.PlayerDao.GetPlayerById(id);
            if (result != null)
            {
                _playersById.TryAdd(player.Id, player);
                _playerNamesById.TryAdd(player.Id, player.Username);
                return result;
            }
            return null;
        }

        public Player GetPlayerBySSO(string sso)
        {
            Player newPlayer = Engine.MainDI.PlayerDao.GetPlayerBySSO(sso);
            if (newPlayer != null)
            {
                _playersById.AddOrUpdate(newPlayer.Id, newPlayer, (oldkey, oldvalue) => newPlayer);
                _playerNamesById.AddOrUpdate(newPlayer.Id, newPlayer.Username, (oldkey, oldvalue) => newPlayer.Username);
                _playersByName.AddOrUpdate(newPlayer.Username, newPlayer, (oldkey, oldvalue) => newPlayer);
                return newPlayer;
            }
            return null;
        }

        public string GetPlayerNameById(int id)
        {
            if (_playerNamesById.TryGetValue(id, out string name))
                return name;

            Engine.MainDI.PlayerDao.GetPlayerNameById(id, out name);
            return name;
        }

        public Player GetPlayerByName(string name)
        {
            if (_playersByName.TryGetValue(name, out Player player))
                return player;

            Player newPlayer = Engine.MainDI.PlayerDao.GetPlayerByName(name);
            if (newPlayer != null)
                return newPlayer;
            return null;
        }
    }
}