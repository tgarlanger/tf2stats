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
using tf2stats.Utilities;

namespace tf2stats
{
    class Program
    {
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

            #region HANDLE_ARGS
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

            #endregion HANDLE_ARGS

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
            string tempcommand;
            int numplayers = 0;
            int position = 0;

            User [] users = new User[36];

            while (!sr.EndOfStream)
            {
                tempstr = sr.ReadLine();

                tempcommand = "";

                if (tempstr[25] != '\"')
                {
                    continue;
                }

                position = 26;

                User tempuser = Utils.ReadUser(tempstr, position);

                switch (tempcommand)
                {
                    case "say":
                        continue;
                        break;
                    case "joined":
                        position += tempcommand.Length + 1;
                        string tempcommand2 = Utils.ReadTo(tempstr, position, ' ');
                        position += tempcommand2.Length + 1 + 1;
                        string tempteam2 = Utils.ReadTo(tempstr, position, '\"');
                        //Console.WriteLine(tempteam2);
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
