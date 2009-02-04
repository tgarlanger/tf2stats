/// C# namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

/// tf2stats namespaces
using tf2stats.Objects;

namespace tf2stats
{
    class Program
    {
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
            }

            Debug.Fail(msg, errnum.ToString());
        }

        static void Main(string[] args)
        {
            if (args.Length <= 1)
            {
                Error(001);

                return;
            }

            StreamReader sr;
            StreamWriter sw = new StreamWriter("output.txt");

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
                }
            }
        }
    }
}
