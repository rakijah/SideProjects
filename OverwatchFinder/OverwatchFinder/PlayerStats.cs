using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverwatchFinder
{
    struct PlayerStats
    {
        public int Kills;
        public int Assists;
        public int Deaths;
        public string Name;
        public string SteamID;

        public PlayerStats(string Name, string SteamID, int k, int a, int d)
        {
            this.Name = Name;
            this.SteamID = SteamID;
            Kills = k;
            Assists = a;
            Deaths = d;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}-{2}-{3}", Name, Kills, Assists, Deaths);
        }
    };
}
