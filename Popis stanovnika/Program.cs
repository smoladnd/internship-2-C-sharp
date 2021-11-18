﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Popis_stanovnika
{
    class Program
    {
        static void Main(string[] args)
        {
            var populationList = new Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)>()
            {
                {"20234601029", ("Barbara Jugovac", new DateTime (2005, 10, 28)) },
                {"02526111191", ("Andrea Perković", new DateTime(1942, 03, 19)) },
                {"69670149194", ("Lucija Milić", new DateTime (1999, 04, 25)) },
                {"95305346132", ("Dario Babić", new DateTime (1947, 01, 20)) },
                {"61750696186", ("Lana Marić", new DateTime (1997, 09, 07)) },
                {"60329579242", ("Mandica Perko",new DateTime (1948, 01, 21)) },
                {"02368243560", ("Rozalija Babić", new DateTime (1961, 03, 03)) },
                {"50453718074", ("Bogdan Stojanović", new DateTime (1965, 09, 15)) },
                {"17354691849", ("Martina Tomić", new DateTime (2012,  10, 30)) },
                {"05908685970", ("Igor Pavlović", new DateTime (1983, 06, 03)) },
                {"06075173965", ("Danica Vinković", new DateTime (1950, 07, 05)) },
                {"26458101029", ("Barbara Jugovac", new DateTime (2005, 10, 28)) },
                {"21347951029", ("Barbara Jugovac", new DateTime (2005, 10, 28)) }

            };

            string choice = "0";
            bool check = false;

            do
            {
                var userChoice = MainMenuChoice(choice);

                switch (userChoice)
                {
                    case "1":
                        IspisStanovnistva(populationList);
                        break;
                    case "2":
                        IspisPoOibu(populationList);
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
                    case "0":
                        check = true;
                        break;
                    default:
                        Console.WriteLine("Unijeli ste nevaljanu opciju, molim vas odaberite jednu od dolje navedenih opcija!");
                        break;
                }

            } while (check == false);
        }

        static string MainMenuChoice(string choice)
        {
            Console.WriteLine("Odaberite akciju:");
            Console.WriteLine("1 - Ispis stanovnistva");
            Console.WriteLine("2 - Ispis stanovnika po OIB-u");
            Console.WriteLine("3 - Ispis OIB-a po unosu imena i prezimena");
            Console.WriteLine("4 - Unos novog stanovnika");
            Console.WriteLine("5 - Brisanje stanovnika po OIB-u");
            Console.WriteLine("6 - Brisanje stanovnika po imenu i prezimenu te datumu rodenja");
            Console.WriteLine("7 - Brisanje svih stanovnika");
            Console.WriteLine("8 - Uredivanje stanovnika");
            Console.WriteLine("9 - Statiskika");
            Console.WriteLine("0 - Izlaz iz aplikacije");

            choice = Console.ReadLine();

            return choice;
         }

        static bool CheckOib(string oib)
        {
            bool OIBState = false;

            if (oib.Length is not 11 && OIBState is false)
            {
                Console.WriteLine("Duljina OIB-a nije tocna!");
                OIBState = true;
            }

            bool isNumber = long.TryParse(oib, out _);
            if (isNumber is false && OIBState is false)
            {
                Console.WriteLine("OIB vam mora imati samo brojeve u sebi!");
                OIBState = true;
            }

            return OIBState;
        }

        static bool CheckNameAndSurname(string nameSurname)
        {
            bool state = false;
            int countSpace = 0;

            for (int i = 0; i < nameSurname.Length; i++)
            {
                if (nameSurname[i] >= 'a' && nameSurname[i] <= 'z' || nameSurname[i] == ' ' || nameSurname[i] >= 'A' && nameSurname[i] <= 'Z')
                    state = false;
                else
                {
                    state = true;
                    Console.WriteLine("Molim vas pri upisu imena i prezime koristitie samo latinska slova!");
                    break;
                }
            }

            if (state is false)
            {
                for (int i = 0; i < nameSurname.Length; i++)
                    if (nameSurname[i] == ' ')
                        countSpace++;

                if (countSpace is 0)
                {
                    state = true;
                    Console.WriteLine("Molim vas kada pisete ime i prezime, pazite da stavite razmak izmedu svakog imena i prezimena.");
                }
            }

            return state;
        }

        static bool CheckDateOfBirth(string birthTimeString)
        {
            int countSpace = 0;
            bool boolBirthDate = false;

            for (var i = 0; i < birthTimeString.Length; i++)
            {
                if (birthTimeString[i] is ' ')
                    countSpace++;
                if (birthTimeString[i] >= '0' && birthTimeString[i] <= '9' || birthTimeString[i] is ' ')
                    boolBirthDate = false;
                else
                {
                    boolBirthDate = true;
                    Console.WriteLine("Molim vas kada pisete datum rodenja korisite samo arapske brojeve!");
                    break;
                }
            }
            if (countSpace != 2 && boolBirthDate is false)
            {
                Console.WriteLine("Niste datum rodenja upisali u tocnom formatu!");
                boolBirthDate = true;
            }

            return boolBirthDate;
        }

        static void IspisStanovnistva(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            string returnChoice;
            bool state = false;
            do
            {
                Console.WriteLine("Odaberite akciju:");
                Console.WriteLine("1 - Onako kako su spremljeni");
                Console.WriteLine("2 - Po datumu rođenja uzlazno");
                Console.WriteLine("3 - Po datumu rođenja silazn");
                Console.WriteLine("AKo se zelite vratiti na glavni izbornik stisnite bilo koji drugi botun");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        foreach (var item in populationList)
                            Console.WriteLine($"{item.Key} {item.Value}");
                        break;
                    case "2":
                        var sortedDictionaryAscending = from entry in populationList orderby entry.Value.dateOfBirth ascending select entry;
                        foreach (var item in sortedDictionaryAscending)
                            Console.WriteLine($"{item.Key} {item.Value}");
                        break;
                    case "3":
                        var sortedDictionaryDescending = from entry in populationList orderby entry.Value.dateOfBirth descending select entry;
                        foreach (var item in sortedDictionaryDescending)
                            Console.WriteLine($"{item.Key} {item.Value}");
                        break;
                    default:
                        state = true;
                        break;
                }

                if (state == false)
                {
                    Console.WriteLine("AKo se zelite vratiti na glavni izbornik stisnite da.");
                    Console.WriteLine("Ako zelite koristiti jos jednu opciju u Ispisu stanovnistva stisnite bilo koji botun.");
                    returnChoice = Console.ReadLine();

                    if (returnChoice == "da" || returnChoice == "Da")
                        state = true;
                }

            } while (state == false);

        }

        static void IspisPoOibu(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            bool OIBState, x = false;
          
                do
                {
                    OIBState = false;
                    
                    Console.WriteLine("Upisite OIB trazene osobe:");
                    string oib = Console.ReadLine();

                    OIBState = CheckOib(oib);

                    if (OIBState is false)
                        foreach (var item in populationList)
                        {
                            if (item.Key == oib)
                            {
                                Console.WriteLine($"{item.Key} {item.Value}");
                                Console.WriteLine("Zelite li pretraziti jos jednu osobu? Ako zelite upisite da!");
                                Console.WriteLine("Ako ne zelite stisnite bilo koju tipku.");
                                string useOptionAgain = Console.ReadLine();

                                if (useOptionAgain == "da" || useOptionAgain == "Da")
                                    OIBState = true;
                                else
                                    x = true;
                            }                            
                        }

                    if (OIBState is false && x is false)
                    {
                        Console.WriteLine("OIB koji ste upisali ne postoji u popisu, ako zelite pokusati ponovno napisite da.");
                        Console.WriteLine("Ako se zelite vratiti u glavni izbornik stisnite bilo koju tipku.");
                        var returnChoice = Console.ReadLine();

                        if (returnChoice == "da" || returnChoice == "Da")
                            OIBState = true;
                    }
                
                } while (OIBState is true);

        }

        
    }

}
