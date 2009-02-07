/// C# namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

/// MySQL namespaces
using MySql.Data.MySqlClient;

/// tf2stats namespaces
using tf2stats.Objects;

namespace tf2stats
{
    class Program
    {
        /// <summary>
        /// Enum for Determing which field to search on
        /// </summary>
        enum SEARCH_VALUE
        {
            SEARCH_ID, SEARCH_NAME
        }

        /// <summary>  
        /// Gets whether the specified path is a valid absolute file path.  
        /// </summary>  
        /// <param name="path">Any path. OK if null or empty.</param>  
        static public bool IsValidPath(string path)
        {
            Regex r = new Regex(@"^(([a-zA-Z]\:)|(\\))(\\{1}|((\\{1})[^\\]([^/:*?<>""|]*))+)$");
            return r.IsMatch(path);
        }

        /// <summary>
        /// Accepts error code and displays appropriate message.
        /// </summary>
        /// <param name="errnum">Number of error to display.</param>
        static void Error(int errnum)
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
        static int Find(User[] users, string searchstr, SEARCH_VALUE sv)
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
                    for ( int i = 0; i < users.Length; i++ )
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

        /// <summary>
        /// Gets a section of a string
        /// </summary>
        /// <param name="input">String to get a sub-string from</param>
        /// <param name="start">Index in the input string to start at</param>
        /// <param name="delim">Character to end the sub-string</param>
        /// <returns>Sub-string from the input string</returns>
        static string ReadTo(string input, int start, char delim)
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

        static User ReadUser(string input, int start)
        {
            
        }

        /// <summary>
        /// Main Entry Point of the program
        /// </summary>
        /// <param name="args">Command Line Arugments</param>
        static void Main(string[] args)
        {
            if (args.Length <= 1)
            {
                //Error(001);

                //return;
            }

            StreamReader sr = new StreamReader("..\\..\\Logs\\CTF_example.log");
            StreamWriter sw = new StreamWriter("output.txt");
            /*
            for (int i = 1; i < args.Length; i++)
            {
                switch (args[i])
                {
                case "-i":
                    if (i + 1 == args.Length)
                    {
                        Error(001);
                    }
                    if (!IsValidPath(args[i + 1]))
                    {
                        Error(002);
                    }

                    try
                    {
                        sr = new StreamReader(args[i + 1]);
                    }
                    catch (Exception e)
                    {
                        
                    }

                    i++;
                    break;
                case "-o":
                    if (i + 1 == args.Length)
                    {
                        Error(001);
                    }
                    if (!IsValidPath(args[i + 1]))
                    {
                        Error(002);
                    }

                    try
                    {
                        sw = new StreamWriter(args[i + 1]);
                    }
                    catch (Exception e)
                    {

                    }

                    i++;
                    break;
                case "-d":
                    if (i + 1 == args.Length)
                    {
                        Error(001);
                    }
                    if ( args[i] != "false" || args[i] != "true" )
                    {
                        Error(003);
                    }
                }
            }
            */
            MySqlConnection con = new MySqlConnection();

            con.ConnectionString = "Server=localhost;Database=tf2stats;Uid=root;Pwd=password;";

            try
            {
                con.Open();
            }
            catch (MySqlException e)
            {
                Debug.Fail(e.ToString());
            }

            string tempstr;
            string tempusername;
            string tempsteamid;
            string tempteam;
            string tempint;
            string tempcommand;
            int tempnumber;
            int i, j, k, l;
            int numplayers = 0;
            int position = 0;

            User [] users = new User[36];

            while (!sr.EndOfStream)
            {
                tempstr = sr.ReadLine();

                tempusername = "";
                tempsteamid = "";
                tempteam = "";
                tempint = "";
                tempcommand = "";

                if (tempstr[25] != '\"')
                {
                    continue;
                }

                /// GET USER NAME
                tempusername = ReadTo(tempstr, 26, '<');

                position = tempusername.Length + 26 + 1;

                /// GET NUMBER
                tempint = ReadTo(tempstr, position, '>');

                /// Update read position
                position += tempint.Length + 1 + 1;

                /// CONVERT FROM STRING TO INTEGER
                tempnumber = Convert.ToInt32(tempint);

                /// GET STEAM ID
                tempsteamid = ReadTo(tempstr, position, '>');

                position += tempsteamid.Length + 1 + 1;

                /// GET TEAM NAME
                tempteam = ReadTo(tempstr, position, '>');

                position += tempteam.Length + 1 + 1 + 1;

                tempcommand = ReadTo(tempstr, position, ' ');

                switch (tempcommand)
                {
                    case "say":
                        continue;
                        break;
                    case "joined":
                        position += tempcommand.Length + 1;
                        string tempcommand2 = ReadTo(tempstr, position, ' ');
                        position += tempcommand2.Length + 1 + 1;
                        string tempteam2 = ReadTo(tempstr, position, '\"');
                        Console.WriteLine(tempteam2);
                        break;
                }

                //Console.WriteLine("break!");

                /*
                int index = Find(users, tempusername, SEARCH_VALUE.SEARCH_NAME);

                if (index == -1)
                {
                    /// User was not found by name
                    index = Find(users, tempsteamid, SEARCH_VALUE.SEARCH_ID);

                    if (index == -1)
                    {
                        /// User was not found by STEAM_ID
                        /// CREATE a NEW user:
                        users[numplayers] = new User(tempusername, tempsteamid, tempteam, tempnumber);
                    }
                }
                else
                {
                    if (users[index].SteamID == "STEAM_ID_PENDING" && tempsteamid != "STEAM_ID_PENDING")
                    {

                    }
                }
                 * */
            }// Read File

            Console.WriteLine("DONE!!!!");

        }// Main
    }// Program
}// Namespace
