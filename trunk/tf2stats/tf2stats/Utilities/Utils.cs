using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

using tf2stats.Objects;

namespace tf2stats.Utilities
{
    class Utils
    {
        /// <summary>  
        /// Gets whether the specified path is a valid absolute file path.  
        /// </summary>  
        /// <param name="path">Any path. OK if null or empty.</param>  
        public static bool IsValidPath(string path)
        {
            Regex r = new Regex(@"^(([a-zA-Z]\:)|(\\))(\\{1}|((\\{1})[^\\]([^/:*?<>""|]*))+)$");
            return r.IsMatch(path);
        }

        /// <summary>
        /// Accepts error code and displays appropriate message.
        /// </summary>
        /// <param name="errnum">Number of error to display.</param>
        public static void Error(int errnum)
        {
            string msg = "Error " + errnum.ToString() + ":  ";

            switch (errnum)
            {
                case 001:
                    msg += "Too Few Arguments!";
                    break;
                case 002:
                    msg += "Invalid File Path!";
                    break;
                case 003:
                    msg += "Invaid Arguments!";
                    break;
            }

            Debug.Fail(msg, errnum.ToString());
        }

        /// <summary>
        /// Finds a User by Steam ID
        /// </summary>
        /// <param name="users">List of Users</param>
        /// <param name="steamid">Steam ID to Find</param>
        /// <returns>Index of Steam ID, -1 if Not Found</returns>
        public static int Find(User[] users, string searchstr, SEARCH_VALUE sv)
        {
            switch (sv)
            {
                case SEARCH_VALUE.SEARCH_NAME:
                    for (int i = 0; i < users.Length; i++)
                    {
                        if (users[i].UserName == searchstr)
                        {
                            return i;
                        }
                    }
                    break;
                case SEARCH_VALUE.SEARCH_ID:
                default:
                    for (int i = 0; i < users.Length; i++)
                    {
                        if (users[i].SteamID == searchstr)
                        {
                            return i;
                        }
                    }
                    break;
            }


            return -1;
        }

        public static int FindOrCreateUser(User[] users, User user, int numplayers)
        {
            int index = Find(users,user.UserName,SEARCH_VALUE.SEARCH_NAME);

            if (index == -1)
            {
                if (user.SteamID != "STEAM_ID_PENDING")
                {
                    index = Find(users, user.SteamID, SEARCH_VALUE.SEARCH_ID);

                    if (index == -1)
                    {
                        users[numplayers] = user;

                        return numplayers;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a section of a string
        /// </summary>
        /// <param name="input">String to get a sub-string from</param>
        /// <param name="start">Index in the input string to start at</param>
        /// <param name="delim">Character to end the sub-string</param>
        /// <returns>Sub-string from the input string</returns>
        public static string ReadTo(string input, int start, char delim)
        {
            string tempstr = "";

            for (int i = start; i < input.Length; i++)
            {
                if (input[i] == delim)
                {
                    return tempstr;
                }

                tempstr += input[i];
            }

            return null;
        }

        /// <summary>
        /// Read a user from a input line, given the starting index
        /// </summary>
        /// <param name="input">String to read the User From</param>
        /// <param name="start">Starting Index</param>
        /// <returns>User object read from String.</returns>
        public static User ReadUser(string input, int start)
        {
            string tempusername;
            string tempsteamid;
            string tempteam;
            string tempint;

            int tempnumber;

            int position = start;

            /// GET USER NAME
            tempusername = ReadTo(input, 26, '<');

            position += tempusername.Length + 1;

            /// GET NUMBER
            tempint = ReadTo(input, position, '>');

            /// Update read position
            position += tempint.Length + 1 + 1;

            /// CONVERT FROM STRING TO INTEGER
            tempnumber = Convert.ToInt32(tempint);

            /// GET STEAM ID
            tempsteamid = ReadTo(input, position, '>');

            position += tempsteamid.Length + 1 + 1;

            /// GET TEAM NAME
            tempteam = ReadTo(input, position, '>');

            return new User(tempusername, tempsteamid, tempteam, tempnumber);
        }
    }
}
