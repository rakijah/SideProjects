using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGSI;
using CSGSI.Nodes;

namespace OverwatchFinder
{
    class SuspectFinder
    {
        private static GameStateListener _gsl;
        private static GameState _currentGameState;
        public static bool SuspectFound = false;
        public static string SuspectName, SuspectSteamURL;

        public static void Start()
        {
            _gsl = new GameStateListener(3002);
            _gsl.NewGameState += Gsl_NewGameState;
            _gsl.Start();
            Console.WriteLine("Listening for GameState...");
        }

        private static void Gsl_NewGameState(GameState gs)
        {
            if (_currentGameState == null)
            {
                _currentGameState = gs;
            }

            else if (gs.Player.Activity != PlayerActivity.Playing || //Check if in game
                gs.Map.Round < 1 || //Skip pistol rounds because everyone starts at 0-0-0
               gs.Player.Team == PlayerTeam.Undefined ||
               gs.Map.Phase != MapPhase.Live || //skip warmup
               gs.Round.Phase != RoundPhase.Live || //new round triggers too soon, wait for live
               _currentGameState.Map.Round == gs.Map.Round)
            {
                return;
            }
            _currentGameState = gs;

            //Console.WriteLine("Received GameState (Round {0}), suspect: {1}-{2}-{3}", gs.Map.Round, gs.Player.MatchStats.Kills, gs.Player.MatchStats.Assists, gs.Player.MatchStats.Deaths);

            if (!DemoAnalyzer.DemoStats.ContainsKey(gs.Map.Round))
            {
                //Console.WriteLine("Stats for round not found, skipping...");
                return;
            }

            bool found = false;
            PlayerStats suspect = new PlayerStats("ERRROR", "ERRROR", -1, -1, -1);
            foreach (var info in DemoAnalyzer.DemoStats[gs.Map.Round])
            {
                if (info.Kills == gs.Player.MatchStats.Kills &&
                    info.Deaths == gs.Player.MatchStats.Deaths &&
                    info.Assists == gs.Player.MatchStats.Assists)
                {
                    if (!found)
                    {
                        suspect = info;
                        found = true;
                    }
                    else
                    {
                        //Console.WriteLine("Multiple players with the same score found, continuing...");
                        found = false;
                        return;
                    }
                }
            }

            if (found)
            {
                _gsl.NewGameState -= Gsl_NewGameState;
                Console.WriteLine("Found suspect: ");
                SuspectSteamURL = string.Format("http://steamcommunity.com/profiles/{0}/", suspect.SteamID);
                Console.WriteLine("{0} - {1}", suspect.Name, SuspectSteamURL);
                SuspectName = suspect.Name;
                SuspectFound = true;
                string outputFile = "output.txt";
                File.AppendAllText(outputFile, suspect.Name + " - " + SuspectSteamURL + Environment.NewLine);
                //_gsl.Stop();
            }
            else
            {
                //Console.WriteLine("Suspect not found, continuing...");
            }
        }

        public static void StopGSL()
        {
            if(_gsl != null)
                _gsl.Stop();
        }
    }
}
