﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ManagingSkiTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Skis> skis = InitializeSkiList();

            DisplayWelcomeScreen();
            DisplayMenuScreen(skis);
            DisplayClosingScreen();

        }

        #region LIST OF SKIS
        /// <summary>
        /// Initialize ski list
        /// </summary>
        /// <returns></returns>
        static List<Skis> InitializeSkiList()
        {
            List<Skis> skis = new List<Skis>()
            {

                new Skis()
                {
                    Brand = "fischer",
                    Length = 150
                },

                new Skis()
                {
                    Brand = "volkl",
                    Length = 145
                },

                new Skis()
                {
                    Brand = "nordica",
                    Length = 175
                }

            };

            return skis;
        }
        #endregion

        #region MAIN MENU
        static void DisplayMenuScreen(List<Skis> skis)
        {
            bool quitApplication = false;
            ConsoleKeyInfo menuChoiceKey;
            char menuChoice;

            do
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;

                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) List All Skis");
                Console.WriteLine("\tb) View Ski Detail");
                Console.WriteLine("\tc) Add Skis");
                Console.WriteLine("\td) Delete Skis");
                Console.WriteLine("\te) Update Skis");
                Console.WriteLine("\tf) Write skis to file");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoiceKey = Console.ReadKey();
                menuChoice = menuChoiceKey.KeyChar;

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case 'a':
                        DisplayAllSkis(skis);
                        break;

                    case 'b':
                        DisplaySkiDetail(skis);
                        break;
           
                    case 'c':
                        DisplayAddSkis(skis);
                        break;

                    case 'd':
                        DisplayDeleteSkis(skis);
                        break;

                    case 'e':
                        DisplayUpdateSkis(skis);
                        break;

                    case 'f':
                        DisplayWriteToDataFile(skis);
                        break;

                    case 'q':
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitApplication);

            Console.Clear();
        }
        #endregion

        #region UTILITY METHODS
        static void DisplayAllSkis(List<Skis> skis)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;

            DisplayScreenHeader("All Skis");

            Console.WriteLine("\t***************************");
            foreach (Skis ski in skis)
            {
                SkiInfo(ski);
                Console.WriteLine();
                Console.WriteLine("\t***************************");
            }
          
            DisplayContinuePrompt();
            Console.Clear();
        }

        static void DisplaySkiDetail(List<Skis> skis)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            DisplayScreenHeader("Ski Detail");

            Console.WriteLine("\tSki Brands");
            Console.WriteLine("\t-------------");
            foreach (Skis ski in skis)
            {
                Console.WriteLine("\t" + ski.Brand);
            }


            Console.WriteLine();
            Console.Write("\tEnter Brand:");
            string skiBrand = Console.ReadLine().ToLower();

            Skis selectedSkis = null;
            foreach (Skis ski in skis)
            {
                if (ski.Brand == skiBrand)
                {
                    selectedSkis = ski;
                    break;
                }
            }

            //
            // display monster detail
            //
            Console.WriteLine();
            Console.WriteLine("\t*********************");
            SkiInfo(selectedSkis);
            Console.WriteLine("\t*********************");

            DisplayContinuePrompt();
            Console.Clear();
        }

        static void DisplayAddSkis(List<Skis> skis)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;

            bool validResponse = false;

            Skis newSkis = new Skis();

            DisplayScreenHeader("Add Skis");

            do
            {
                validResponse = false;
                Console.Write("\tBrand: ");
                newSkis.Brand = Console.ReadLine();
                if (newSkis.Brand == "")
                {
                    Console.WriteLine("Please enter a Brand");
                }
                else
                {
                    validResponse = true;
                }

            } while (!validResponse);

            do
            {
                Console.Write("\tLength: ");

                if (!int.TryParse(Console.ReadLine(), out int length))
                {
                    Console.WriteLine("Please enter a valid length");
                }
                else
                {
                    if (length <= 0)
                    {
                        Console.WriteLine("Please enter a positive value");
                    }
                    else
                    {
                        validResponse = true;
                        newSkis.Length = length;
                    }

                }
            } while (!validResponse);

            Console.WriteLine();
            Console.WriteLine("\tNew Ski Properties");
            Console.WriteLine("\t-------------");
            SkiInfo(newSkis);
            Console.WriteLine("\t-------------");

            DisplayContinuePrompt();

            skis.Add(newSkis);
            Console.Clear();
        }

        static void DisplayDeleteSkis(List<Skis> skis)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            DisplayScreenHeader("Delete Skis");


            Console.WriteLine("\tSki Brands");
            Console.WriteLine("\t-------------");
            foreach (Skis ski in skis)
            {
                Console.WriteLine("\t" + ski.Brand);
            }

            Console.WriteLine();
            Console.Write("\tEnter Brand:");
            string skiBrand = Console.ReadLine();

            Skis selectedSki = null;
            foreach (Skis ski in skis)
            {
                if (ski.Brand == skiBrand)
                {
                    selectedSki = ski;
                    break;
                }
            }

            if (selectedSki != null)
            {
                skis.Remove(selectedSki);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedSki.Brand} deleted");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{skiBrand} not found");
            }

            DisplayContinuePrompt();
            Console.Clear();
        }

        static void DisplayUpdateSkis(List<Skis> skis)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;

            bool validResponse = false;
            Skis selectedSki = null;

            do
            {
                DisplayScreenHeader("Update Skis");

                Console.WriteLine("\tSki Brands");
                Console.WriteLine("\t-------------");
                foreach (Skis ski in skis)
                {
                    Console.WriteLine("\t" + ski.Brand);
                }

                Console.WriteLine();
                Console.Write("\tEnter Brand:");
                string skiBrand = Console.ReadLine().ToLower();


                foreach (Skis ski in skis)
                {
                    if (ski.Brand == skiBrand)
                    {
                        selectedSki = ski;
                        validResponse = true;
                        break;
                    }

                }

                if (!validResponse)
                {
                    Console.WriteLine("\tPlease select a correct Brand.");
                    DisplayContinuePrompt();
                }

            } while (!validResponse);


            //
            // update monster properties
            //
            string userResponse;
            Console.WriteLine();
            Console.WriteLine("\tReady to Update. [Press Enter to keep the current info]");
            Console.WriteLine();

            do
            {
                validResponse = false;
                Console.Write($"\tCurrent Brand: {selectedSki.Brand} New Brand: ");
                userResponse = Console.ReadLine();

                if (userResponse == "")
                {
                    Console.WriteLine("Please Enter a Valid Brand");
                }
                else
                {
                    validResponse = true;
                    selectedSki.Brand = userResponse;
                }

            } while (!validResponse);

            do
            {
                validResponse = false;
                Console.Write($"\tCurrent Length: {selectedSki.Length} New Length: ");
                userResponse = Console.ReadLine();

                if (userResponse == "" || !int.TryParse(userResponse, out int length))
                {
                    Console.WriteLine("Please Try Again");
                }
                else
                {
                    if (length <= 0)
                    {
                        Console.WriteLine("Please enter a positive value");
                    }
                    else
                    {
                        validResponse = true;
                        selectedSki.Length = length;
                    }

                }
            } while (!validResponse);

            Console.WriteLine();
            Console.WriteLine("\tNew Ski Properties");
            Console.WriteLine("\t-------------");
            SkiInfo(selectedSki);
            Console.WriteLine();
            Console.WriteLine("\t-------------");

            DisplayContinuePrompt();
            Console.Clear();
        }

        static void WriteSkisToDataFile(List<Skis> skis)
        {
            string[] skisString = new string[skis.Count];

            for (int index = 0; index < skis.Count; index++)
            {
                string skiString =
                    skis[index].Brand + "," +
                    skis[index].Length;

                skisString[index] = skiString;
            }

            File.WriteAllLines("SkiData\\SkiData.txt", skisString);
        }

        static void DisplayWriteToDataFile(List<Skis> skis)
        {
            DisplayScreenHeader("Write to Ski List Data File");

            Console.WriteLine("\tTReady to write the list to the file.");
            Console.Write("\tEnter 'y' to continue or 'n' to cancel.");
            if (Console.ReadLine().ToLower() == "y")
            {
                DisplayContinuePrompt();
                WriteSkisToDataFile(skis);

                Console.WriteLine();
                Console.WriteLine("\tList written to data the file.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("\tList not written to the data file.");
            }

            DisplayContinuePrompt();
        }

        #endregion

        #region HELPER METHODS

        static void SkiInfo(Skis skis)
        {
            Console.WriteLine($"\tBrand: {skis.Brand}");
            Console.WriteLine($"\tAge: {skis.Length}");
        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThe Ski Manager");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using the Ski Manager!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.Write("\tPress any key to continue.");
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }
        #endregion
    }
}
