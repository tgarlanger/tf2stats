using System;
using System.Collections;
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

    enum ADD_RESPONSE
    {
        ADDED, ESISTS, ERROR
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

        private Role[] roles;

        private Role userrole;

        public Role UserRole
        {
            get
            {
                return userrole;
            }
            set
            {
                userrole = value;
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

    /// <summary>
    /// Role Class
    /// </summary>
    class Role
    {
        /// <summary>
        /// Name of the Role
        /// </summary>
        private string name;

        /// <summary>
        /// Gets or sets the name of the Role
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Paramaterized constructor
        /// </summary>
        /// <param name="newname">Value to set the Role name to</param>
        public Role(string newname)
        {
            name = newname;
        }

        private WeaponTable weapons;

        public WeaponTable Weapons
        {
            get
            {
                return weapons;
            }
            set
            {
                weapons = value;
            }
        }

        /// <summary>
        /// Overrides the ToString function
        /// </summary>
        /// <returns>String representation of the Role, ie the name</returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// Weapon Class
    /// </summary>
    class Weapon
    {
        /// <summary>
        /// Name of the Weapon
        /// </summary>
        private string name;

        /// <summary>
        /// Gets or sets the name of the Weapon
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private int kills;

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
        /// Overrides the ToString Function
        /// </summary>
        /// <returns>String representation of the Weapon, ie the name</returns>
        public override string ToString()
        {
            return name;
        }
    }

    class RoleTable
    {
        private Role[] roles;

        public Role this[string index]
        {
            get
            {
                for ( int a = 0; a < 9; a++ )
                {
                    if (roles[a].Name == index)
                    {
                        return roles[a];
                    }
                }

                throw new MissingFieldException();
            }
            set
            {
                bool flag = true;

                for (int a = 0; a < 9; a++)
                {
                    if (roles[a].Name == index)
                    {
                        roles[a] = value;
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    throw new MissingFieldException();
                }
            }
        }

        public RoleTable()
        {
            roles = new Role[9];
        }
    }

    class WeaponTable
    {
        private Weapon[] weapons;

        public Weapon[] Weapons
        {
            get
            {
                return weapons;
            }
        }

        private int numweapons;

        public int NumWeapons
        {
            get
            {
                return numweapons;
            }
        }

        public WeaponTable()
        {
            weapons = new Weapon[10];
        }

        public ADD_RESPONSE AddWeapon(Weapon w)
        {
            if (numweapons == 0)
            {
                weapons[0] = w;

                numweapons++;

                return ADD_RESPONSE.ADDED;
            }
            for (int a = 0; a < numweapons; a++)
            {
                if (weapons[a].Name == w.Name)
                {
                    return ADD_RESPONSE.ESISTS;
                }
            }

            weapons[numweapons] = w;

            numweapons++;

            return ADD_RESPONSE.ADDED;
        }
    }
}
