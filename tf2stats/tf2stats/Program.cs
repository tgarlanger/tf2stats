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
        #region GLOBAL_VARIABLES

        /// <summary>
        /// List of All weapons used
        /// </summary>
        public static Weapon[] Weapons = new Weapon [3];

        /// <summary>
        /// The Number of weapons used
        /// </summary>
        public static int numweapons = 3;

        #endregion

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
                User tempuser2 = new User();

                int index = Utils.FindOrCreateUser(users, tempuser, numplayers);

                if (index == numplayers)
                {
                    numplayers++;
                }

                position += tempuser.ToString().Length;

                tempcommand = Utils.ReadTo(tempstr, position, ' ');

                //Console.WriteLine("Command: " + tempcommand);

                switch (tempcommand)
                {
                    /// Did someone say something?  Who cares...
                    case "say":
                        continue;
                    /// Someone Joined a team
                    case "joined":
                        /// Update Position in log line
                        position += tempcommand.Length + 1;

                        /// Read the secondary command: "team" in this case
                        string tempcommand2 = Utils.ReadTo(tempstr, position, ' ');

                        /// Update position in log line
                        position += tempcommand2.Length + 1 + 1;

                        /// Read Team name, surrounded by ""
                        string tempteam2 = Utils.ReadTo(tempstr, position, '\"');

                        /// Assign user to team
                        users[index].Team = tempteam2;
                        
                        /// Display Message about team assignment if in debug mode
                        //Console.WriteLine("User " + index + ": " + users[index].UserName + " " + tempcommand + " " + tempcommand2 + " " + tempteam2 + '\n' );

                        break;
                    /// Someone Killed someone else
                    case "killed":
                        /// Update Position in log line
                        position += tempcommand.Length + 1 + 1;

                        /// Read the victim name
                        tempuser2 = Utils.ReadUser(tempstr, position);

                        /// Find the victim in the user list
                        int index_victim = Utils.FindOrCreateUser(users, tempuser2, numplayers);

                        /// Update Kills
                        users[index].Kills++;

                        /// Update Victim Deaths
                        users[index_victim].Deaths++;

                        //Console.WriteLine("User " + index + ": " + users[index].UserName + " " + tempcommand + " " + users[index_victim].UserName + '\n');
                        break;
                    case "changed":
                        position += tempcommand.Length + 1;

                        tempcommand2 = Utils.ReadTo(tempstr, position, ' ');

                        position += tempcommand2.Length + 1;

                        tempcommand2 = Utils.ReadTo(tempstr, position, ' ');

                        position += tempcommand2.Length + 1 + 1;

                        string temprole = Utils.ReadTo(tempstr, position, '"');

                        Console.WriteLine("User " + index + ": " + users[index].UserName + " " + tempcommand + " role to " + temprole + '\n');

                        users[index].UserRole = new Role(temprole);

                        break;
                }

                //Console.WriteLine("break!");

            }// Read File

            Console.WriteLine("DONE!!!!");

        }// Main
    }// Program
}// Namespace
