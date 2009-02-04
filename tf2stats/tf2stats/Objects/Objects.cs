using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace tf2stats.Objects
{
    /// <summary>
    /// User Class
    /// </summary>
    class User
    {
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
                if (value != "Blue" || value != "Red")
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
