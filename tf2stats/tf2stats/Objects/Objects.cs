using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace tf2stats.Objects
{
    /// <summary>
    /// Enum for Determing which field to search on
    /// </summary>
    enum SEARCH_VALUE
    {
        SEARCH_ID, SEARCH_NAME
    }

    /// <summary>
    /// User Class
    /// </summary>
    class User
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public User()
        {
            username = "";
            steamid = "";
            team = "";
            number = -1;
        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="_username">User Name of the Player</param>
        /// <param name="_steamid">Steam ID of the Player</param>
        /// <param name="_team">Team of the Player</param>
        /// <param name="num">Number of the Player</param>
        public User(string _username, string _steamid, string _team, int num)
        {
            username = _username;
            steamid = _steamid;
            team = _team;
            number = num;
        }

        /// <summary>
        /// User Name of the Player
        /// </summary>
        private string username;
        /// <summary>
        /// Gets or Sets the User Name of the Player
        /// </summary>
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        /// <summary>
        /// Steam ID of the Player
        /// </summary>
        private string steamid;
        /// <summary>
        /// Gets or Sets the Steam ID of the Player
        /// </summary>
        public string SteamID
        {
            get
            {
                return steamid;
            }
            set
            {
                steamid = value;
            }
        }

        /// <summary>
        /// Team the Player is Currently Assigned to
        /// </summary>
        private string team;
        /// <summary>
        /// Gets or Sets the Team of the Player
        /// </summary>
        public string Team
        {
            get
            {
                return team;
            }
            set
            {
                if (value != "Blue" && value != "Red")
                {
                    value = "Unassigned";
                }
                team = value;
            }
        }

        /// <summary>
        /// Number of the Player
        /// </summary>
        private int number;
        /// <summary>
        /// Gets or Sets the Player Number
        /// </summary>
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        private int kills;

        public int Kills
        {
            get
            {
                return kills;
            }
            set
            {
                kills = value;
            }
        }

        private int deaths;

        public int Deaths
        {
            get
            {
                return deaths;
            }
            set
            {
                deaths = value;
            }
        }

        /// <summary>
        /// Convert instance to a string
        /// </summary>
        /// <returns>String representation of instatnce</returns>
        public override string ToString()
        {
            return "\"" + username + "<" + number + "><" + steamid + "><" + team + ">\"";
        }
    }
}
