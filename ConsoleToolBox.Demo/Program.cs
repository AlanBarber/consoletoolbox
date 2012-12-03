using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleToolBox.Containers;


namespace ConsoleToolBox.Demo
{
    class Program
    {
        public struct OptionList
        {
            public string Value;
            public string Title;
        }

        static void Test()
        {
            var panel = new Panel(1, 1, 25, 3, 0, 15);
            panel.Write("Hello, World!" + Environment.NewLine + "BOB!");
            panel.Visible = true;
            panel.SetPanelSize(panel.Height + 1, panel.Width + 1);
            panel.Write("My name is alan paul barber and i'm a very interesting and exciting person to be around!");
            for (int x = 0; x <= 50; x++)
            {
                panel.WriteLine(x.ToString());
                panel.MovePanel(x,5);
                Thread.Sleep(25);
            }
        }

        static void Main()
        {
            ConsoleToolBox.SetConsoleTitle("ConsoleToolBox Demo Program");
            
            Test();

            return;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ConsoleToolBox Demo Program");
                Console.WriteLine("ConsoleToolBox library ver " + ConsoleToolBox.GetVersion());
                Console.WriteLine("");

                var options = new List<OptionList>
                                   {
                                       new OptionList {Value = "0", Title = "Quit Demo"},
                                       new OptionList {Value = "1", Title = "ConsoleToolBox.Common.ProgressBar()"},
                                       new OptionList {Value = "2", Title = "ConsoleToolBox.Common.ProgressSpinner()"},
                                       new OptionList {Value = "3", Title = "ConsoleToolBox.Containers.Panel()"},
                                       new OptionList {Value = "4", Title = "ConsoleToolBox.Dialogs.MessageBox()"}
                                   };

                String sel = OptionSelector(options);

                switch (sel)
                {
                    case "0":
                        return;
                    case "1":
                        ProgressBarDemo();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        break;
                    case "8":
                        break;
                    case "9":
                        break;
                }

            }
        }

        static string OptionSelector(List<OptionList> options)
        {
            int cursorTop = Console.CursorTop;

            while (true)
            {
                Console.SetCursorPosition(0, cursorTop);
                foreach (var option in options)
                {
                    Console.WriteLine(option.Value.PadLeft(3) + ") " + option.Title);
                }
                Console.Write(" Option>");
                String sel = Console.ReadLine();
                if (options.Exists(item => item.Value == sel))
                {
                    return sel;
                }
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.BufferWidth));
            }
        }

        #region DEMOS

        static void ProgressBarDemo()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ConsoleToolBox Demo Program - ConsoleToolBox.Common.ProgressBar()");
                Console.WriteLine("ConsoleToolBox library ver " + ConsoleToolBox.GetVersion());
                Console.WriteLine("");


            
                Console.WriteLine();
                String sel = Console.ReadLine();
                switch(sel)
                {
                    case "0":
                        return;
                }
            }

        }

        #endregion
    }
}
