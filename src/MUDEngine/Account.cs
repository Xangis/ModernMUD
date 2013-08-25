using System;
using System.Collections.Generic;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// An account structure, used for keeping track of all of a player's characters.
    /// This should also assist in preventing cross-racewar logins.
    /// </summary>
    public class Account
    {
        public string LoginName { get; set; }
        public string EmailAddress { get; set; }
        public List<string> Characters { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public int LastCharacter { get; set; }

        /// <summary>
        /// Last racewar side that the player logged in on.
        /// </summary>
        public Race.RacewarSide LastRacewarSide { get; set; }
        public DateTime LastLogout { get; set; }

        public Account()
        {
            LoginName = String.Empty;
            EmailAddress = String.Empty;
            Characters = new List<String>();
            Password = String.Empty;
            LastLogin = new DateTime();
            LastCharacter = 0;
            LastRacewarSide = Race.RacewarSide.neutral;
            LastLogout = new DateTime();
        }
    }
}
