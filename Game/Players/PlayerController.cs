using AuroraEmu.DI.Game.Players;
using System.Collections.Concurrent;
using System.Data;

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
            
            DataRow result = Engine.MainDI.PlayerDao.GetPlayerById(id);
            
            if (result != null)
            {
                player = new Player(result);
                _playersById.TryAdd(player.Id, player);
                _playerNamesById.TryAdd(player.Id, player.Username);

                return player;
            }

            return null;
        }

        public Player GetPlayerBySSO(string sso)
        {
            DataRow result = Engine.MainDI.PlayerDao.GetPlayerBySSO(sso);

            if (result != null)
            {
                Player player = new Player(result);
                _playersById.AddOrUpdate(player.Id, player, (oldkey, oldvalue) => player);
                _playerNamesById.AddOrUpdate(player.Id, player.Username, (oldkey, oldvalue) => player.Username);
                _playersByName.AddOrUpdate(player.Username, player, (oldkey, oldvalue) => player);
                return player;
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

            DataRow result = Engine.MainDI.PlayerDao.GetPlayerByName(name);
            if (result != null)
            {
                player = new Player(result);
                _playersByName.TryAdd(player.Username, player);

                return player;
            }

            return null;
        }
    }
}
