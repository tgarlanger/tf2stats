using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tf2stats
{
    class Program
    {
        static void Error(int errnum)
        {
            string msg = "";

            switch (errnum)
            {
                case 001:
                    msg = "Error 001:  Too Few Arguments!";
                    break;
            }

            Console.WriteLine(msg);
        }

        static void Main(string[] args)
        {
            if (args.Length <= 1)
            {
                Error(001);

                return;
            }
        }
    }
}
