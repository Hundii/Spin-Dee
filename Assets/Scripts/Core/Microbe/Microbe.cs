using Common;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class Microbe : MonoBehaviour, IHarvesterStatsHolder, IStatsHandlerHolder
    {
        [SerializeField] private MicrobeStatsSO microbeStats;
        private StatsHandler statsHandler;
        private void Awake()
        {
            statsHandler = new(new(microbeStats.stats.Select(x => x.stat), microbeStats.stats.Select(x => x.value)));
        }

        public HarvesterStatsSO GetHarvesterStats()
        {
            return microbeStats.harvesterStats;
        }

        public StatsHandler GetStatsHandler()
        {
            return statsHandler;
        }
    }
}
