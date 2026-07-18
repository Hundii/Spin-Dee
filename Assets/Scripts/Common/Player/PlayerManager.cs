using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class PlayerManager : MonoBehaviour, INonPersistentManager
    {
        private Dictionary<int, object> players = new();
        public void RegisterPlayer<T>(T player, int index = 0)
        {
            if (!players.TryAdd(index,player))
            {
                CustomLogger.LogError($"Player with index {index} is already registered in player manager");
            }
        }

        public T GetPlayer<T>(int index = 0)
        {
            return (T)players[index];
        }
    }
}
