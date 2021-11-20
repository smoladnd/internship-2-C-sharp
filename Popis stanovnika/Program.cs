using System;
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
                        IspisPoImenu(populationList);
                        break;
                    case "4":
                        AddCitisen(populationList);
                        break;
                    case "5":
                        EraseCitisenByOib(populationList);
                        break;
                    case "6":
                        EraseCitisenByValue(populationList);
                        break;
                    case "7":
                        EraseAllCitisens(populationList);
                        break;
                    case "8":
                        UpdateCitizen(populationList);
                        break;
                    case "9":
                        Statistics(populationList);
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

        static void IspisPoImenu(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {

            bool state, boolBirthDate = false, checkOutput = false;

            do
            {
                state = false;

                Console.WriteLine("Upisite ime i prezime osobe koju trazite.");
                string nameSurname = Console.ReadLine();

                state = CheckNameAndSurname(nameSurname);

                if (state is false)
                {
                    string birthTimeString;
                    do
                    {
                        Console.WriteLine("Upisite datum rodenja trazene osobe, molio bih vas da se drzite ovog formata: YYYY-MM-DD");
                        birthTimeString = (Console.ReadLine());

                        boolBirthDate = CheckDateOfBirth(birthTimeString);

                    } while (boolBirthDate == true);

                    DateTime birthDateTime = Convert.ToDateTime(birthTimeString);

                    if (state is false)
                        foreach (var item in populationList)
                            if (item.Value.nameAndSurname == nameSurname && item.Value.dateOfBirth == birthDateTime)
                            {
                                Console.WriteLine($"{item.Key}");
                                checkOutput = true;
                            }

                    if (checkOutput is true)
                    {
                        Console.WriteLine("Zelite li pretraziti jos neko ime, prezime i datum, ako zelite napisite 'da'.");
                        Console.WriteLine("Ako ne zelite stisnite bilo koji botun.");
                        string useOptionAgain = Console.ReadLine();

                        if (useOptionAgain == "da" || useOptionAgain == "Da")
                            state = true;
                    }

                    else
                    {
                        Console.WriteLine("Ime, prezime i datum rodenja koji ste upisali ne postoje u popisu, ako zelite pokusati ponovno napisite da.");
                        Console.WriteLine("Ako se zelite vratiti u glavni izbornik stisnite bilo koju tipku.");
                        var returnChoice = Console.ReadLine();

                        if (returnChoice == "da" || returnChoice == "Da")
                            state = true;
                    }
                }
            } while (state == true);

        }

        static void AddCitisen(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            bool state;
            string newOib, newNameAndSurname, birthTimeString;

            do
            {
                state = false;

                Console.WriteLine("Ako ste sigurni u ovu odluku napisite 'da', ako niste stisnite bilo koji botun");
                string userChoiceMenu = Console.ReadLine();

                if (userChoiceMenu is "da" || userChoiceMenu is "Da")
                {
                    bool oibState = false;

                    do
                    {
                        state = false;
                        Console.WriteLine("Upisite OIB nove osobe:");
                        newOib = Console.ReadLine();

                        state = CheckOib(newOib);
                    } while (state is true);

                    if (state is false)
                        foreach (var item in populationList)
                            if (item.Key == newOib)
                            {
                                oibState = true;
                                state = true;
                                break;
                            }

                    if (oibState is true)
                    {
                        Console.WriteLine("OIB koji ste upisali vec postoji na popisu!");
                        Console.WriteLine("Ako zelite pokusat unijeti novu osobu napisite 'da'.");
                        Console.WriteLine("Ako se zelite vratiti na pocetni izbornik stisnite bilo koji drugi botun.");
                        string userChoiseOib = Console.ReadLine();

                        if (userChoiseOib is "da" || userChoiseOib is "Da")
                            state = false;
                    }

                    if (state is false)
                    {
                        do
                        {
                            state = false;
                            Console.WriteLine("Upisite ime i prezime nove osobe:");
                            newNameAndSurname = Console.ReadLine();

                            state = false;
                            state = CheckNameAndSurname(newNameAndSurname);
                        } while (state is true);

                        if (state is false)
                        {
                            do
                            {
                                state = false;
                                Console.WriteLine("Upisite datum rodenja nove osobe:");
                                birthTimeString = Console.ReadLine();

                                state = CheckDateOfBirth(birthTimeString);

                            } while (state is true);

                            DateTime newDateOfBirth = Convert.ToDateTime(birthTimeString);

                            if (state is false)
                            {
                                var citizen = (nameAndSurname: newNameAndSurname, dateOfBirth: newDateOfBirth);
                                populationList.Add(newOib, citizen);

                                Console.WriteLine("Stanovnik je uspjesno unesen, ako zelite opet unijet nekoga napisite 'da'.");
                                Console.WriteLine("Ako se zelite vratiti na pocetni izbornik stisnite bilo koju tipku.");
                                string userChoice = Console.ReadLine();

                                if (userChoice is "da" || userChoice is "Da")
                                    state = true;
                                else
                                    break;
                            }
                        }
                    }
                }

                else
                    break;

            } while (state is true);
        }

        static void EraseCitisenByOib(Dictionary<string, (string nameAdnSurname, DateTime dateOfBirth)> populationList)
        {
            bool state = false, checkRemove = false;
            string newOib, userChoice;

            do
            {
                Console.WriteLine("Ako ste sigurni u ovu odluku napisite 'da', ako niste stisnite bilo koji botun");
                string userChoiceMenu = Console.ReadLine();

                if (userChoiceMenu is "da" || userChoiceMenu is "Da")
                {
                    state = false;
                    if (state is false)
                    {
                        do
                        {
                            state = false;
                            Console.WriteLine("Upisite OIB osobe koju zelite obrisat iz popisa:");
                            newOib = Console.ReadLine();

                            state = CheckOib(newOib);
                        } while (state is true);

                        if (state is false)
                        {
                            foreach (var item in populationList)
                                if (item.Key == newOib)
                                {
                                    populationList.Remove(newOib);
                                    checkRemove = true;
                                }

                            if (checkRemove is false)
                            {
                                Console.WriteLine("OIB koji ste unijeli ne postoji u popisu.");
                                Console.WriteLine("AKo zelite pokusat brisat opet napisite 'da'.");
                                Console.WriteLine("Ako zelite natrag u glavni izbornik stisnite bilo koju tipku.");
                                userChoice = Console.ReadLine();

                                if (userChoice is "da" || userChoice is "Da")
                                    state = true;
                                else
                                    break;
                            }
                            else
                            {
                                Console.WriteLine("Stanovnik uspjesno izbrisan.");
                                Console.WriteLine("AKo zelite pokusat brisat opet napisite 'da'.");
                                Console.WriteLine("Ako zelite natrag u glavni izbornik stisnite bilo koju tipku.");
                                userChoice = Console.ReadLine();

                                if (userChoice is "da" || userChoice is "Da")
                                    state = true;
                                else
                                    break;
                            }
                        }
                    }
                    else
                        break;
                }
                
            } while (state is true);

        }

        static void EraseCitisenByValue(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            bool state, boolBirthDate, oibState, erasureCheck = false;
            var countPeople = 0;
            string oib, nameSurname;

            do
            {
                state = false;

                Console.WriteLine("Ako ste sigurni u ovu radnju napisite 'da', ako se zelite vratiti na pocetni izbornik stisnite bilo koju tipku.");
                string userChoice = Console.ReadLine();

                if (userChoice is "da" || userChoice is "Da")
                {
                    do
                    {
                        Console.WriteLine("Upisite ime i prezime osobe koju trazite.");
                        nameSurname = Console.ReadLine();

                        state = CheckNameAndSurname(nameSurname);
                    } while (state is true);

                    if (state is false)
                    {
                        string birthTimeString;
                        do
                        {
                            Console.WriteLine("Upisite datum rodenja trazene osobe, molio bih vas da se drzite ovog formata: YYYY-MM-DD");
                            birthTimeString = (Console.ReadLine());

                            boolBirthDate = CheckDateOfBirth(birthTimeString);

                        } while (boolBirthDate == true);

                        DateTime birthDateTime = Convert.ToDateTime(birthTimeString);

                        foreach (var item in populationList)
                            if (item.Value.nameAndSurname == nameSurname && item.Value.dateOfBirth == birthDateTime)
                                countPeople++;

                        if (countPeople is 0)
                        {
                            Console.WriteLine("Ne postoji osoba sa prilozenim imenom, prezimenom i datumom rodenja u popisu.");
                            Console.WriteLine("Ako zelite pokusat izbrisati drugu osobu napisite 'da'.");
                            Console.WriteLine("Ako se zelite vratiti u pocetni izbornik stisnite bilo koji drugi botun.");
                            userChoice = Console.ReadLine();

                            if (userChoice is "da" || userChoice is "Da")
                                state = true;
                            else
                                break;

                        }
                        else if (countPeople is 1)
                        {
                            foreach (var item in populationList)
                                if (item.Value.nameAndSurname == nameSurname && item.Value.dateOfBirth == birthDateTime)
                                    populationList.Remove(item.Key);

                            Console.WriteLine("Izbrisana je trazena osoba, ako zelite pokusat izbrisati jos jendnu osobu napisite 'da'.");
                            Console.WriteLine("Ako se zelite vratiti u pocetni izbornik stisnite bilo koji drugi botun.");
                            userChoice = Console.ReadLine();

                            if (userChoice is "da" || userChoice is "Da")
                                state = true;
                            else
                                break;
                        }
                        else
                        {
                            Console.WriteLine("Postoji vise osoba sa tim imenom, prezimenom i datumom rodenja.");

                            do
                            {
                                oibState = false;

                                do
                                {
                                    Console.WriteLine("Molim vas prilozeti OIB trazene osobe.");
                                    oib = Console.ReadLine();

                                    oibState = CheckOib(oib);

                                } while (oibState is true);

                                foreach (var item in populationList)
                                    if (item.Key == oib)
                                    {
                                        populationList.Remove(item.Key);
                                        erasureCheck = true;

                                        Console.WriteLine("Izbrisana je trazena osoba, ako zelite pokusat izbrisati jos jendnu osobu napisite 'da'.");
                                        Console.WriteLine("Ako se zelite vratiti u pocetni izbornik stisnite bilo koji drugi botun.");
                                        userChoice = Console.ReadLine();

                                        if (userChoice is "da" || userChoice is "Da")
                                            state = true;
                                        else
                                            state = false;
                                    }

                                if (erasureCheck is false)
                                {
                                    Console.WriteLine("Trazeni OIB ne postoji u popisu, ako zelite pokusat ponovno upisat OIB napisite 'OIB'.");
                                    Console.WriteLine("Ako zelite pokusat izbrisati drugu osobu napisite 'da'.");
                                    Console.WriteLine("Ako se zelite vratiti u pocetni izbornik stisnite bilo koji drugi botun.");
                                    userChoice = Console.ReadLine();

                                    if (userChoice is "da" || userChoice is "Da")
                                        state = true;
                                    else if (userChoice is "OIB" || userChoice is "oib")
                                        oibState = true;
                                    else
                                        state = false;
                                }
                            } while (oibState is true);
                        }
                    }
                }
            } while (state is true);

        }

        static void EraseAllCitisens(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            bool state;

            do
            {
                state = false;

                Console.WriteLine("Ako ste sigurni u ovu radnju napisite 'da', ako se zelite vratiti na pocetni izbornik stisnite bilo koju tipku.");
                string userChoice = Console.ReadLine();

                if (userChoice is "da" || userChoice is "Da")
                {
                    foreach (var item in populationList)
                        populationList.Remove(item.Key);

                    Console.WriteLine("Svi stanovnici su uspjesno izbrisani.");
                }

            } while (state is true);
        }

        static void UpdateCitizen(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            bool check;

            do
            {
                check = false;
                Console.WriteLine("1 - Uredi OIB stanovnika");
                Console.WriteLine("2 - Uredi ime i prezime stanovnika");
                Console.WriteLine("3 - Uredi datum rođenja");
                Console.WriteLine("4 - Vracanje na glavni izbornik");
                var switchChoice = Console.ReadLine();

                switch (switchChoice)
                {
                    case "1":
                        check = UpdateCitisenOib(check, populationList);
                        break;
                    case "2":
                        check = UpdateCitisenNameAndSurname(check, populationList);
                        break;
                    case "3":
                        check = UpdateCitizenDateOfBirth(check, populationList);
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("Unijeli ste nevaljanu opciju, pokusajte ponovno.");
                        check = true;
                        break;
                }
            } while (check is true);
        }

        static bool UpdateCitisenOib(bool check, Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            bool state, updateCheck, newOibReedo, mainMenuCheck;
            string oldOib, newOib, remeberNamAndSurname, userChoice;
            DateTime rememberDateOfBirth;

            do
            {
                state = false;
                updateCheck = false;
                mainMenuCheck = false;

                Console.WriteLine("Ako ste sigurni u ovu odluku napisite 'da'.");
                Console.WriteLine("Ako se zelite vratiti na izbornik za uredivanje stanovnika stisnite bilo koju tipku.");
                userChoice = Console.ReadLine();

                if (userChoice is "da" || userChoice is "Da")
                {
                    do
                    {
                        Console.WriteLine("Upisite OIB trazenog stanovnika.");
                        oldOib = Console.ReadLine();

                        state = CheckOib(oldOib);
                    } while (state is true);

                    foreach (var item in populationList)
                        if (item.Key == oldOib)
                        {
                            do
                            {
                                newOibReedo = false;
                                do
                                {
                                    Console.WriteLine("Upisite novi OIB:");
                                    newOib = Console.ReadLine();

                                    state = CheckOib(newOib);
                                } while (state is true);

                                foreach (var thing in populationList)
                                    if (thing.Key == newOib)
                                    {
                                        Console.WriteLine("Novo upisani OIB vec postoji u popisu.");
                                        Console.WriteLine("Ako zelite pokusat ponovno promjenit OIB iste osobe napisite 'da'.");
                                        Console.WriteLine("Ako zelite pokusat mijenjat neki drugi OIB napisite 'oib'.");
                                        Console.WriteLine("Ako se zelite vratiti na izbornik za uredivanje stisnite bilo koji botun.");
                                        userChoice = Console.ReadLine();

                                        if (userChoice is "da" || userChoice is "Da")
                                            newOibReedo = true;
                                        else if (userChoice is "oib" || userChoice is "OIB")
                                            state = true;
                                        else
                                        {
                                            mainMenuCheck = true;
                                            check = true;
                                        }
                                    }
                            } while (newOibReedo is true);

                            if (state is false && mainMenuCheck is false)
                            {
                                remeberNamAndSurname = item.Value.nameAndSurname;
                                rememberDateOfBirth = item.Value.dateOfBirth;

                                populationList.Remove(item.Key);

                                var citizenValue = (nameAndSurname: remeberNamAndSurname, dateOfBirth: rememberDateOfBirth);
                                populationList.Add(newOib, citizenValue);

                                Console.WriteLine("OIB stanovnika uspjesno ureden, ako zelite uredit jos jednog stanovnika napisite 'da'.");
                                Console.WriteLine("Ako se zelite vratiti na izbornik uredivanja stisnite bilo koji botun.");
                                userChoice = Console.ReadLine();

                                if (userChoice is "da" || userChoice is "Da")
                                    state = true;
                                else
                                    check = true;

                                updateCheck = true;
                                break;
                            }
                        }

                    if (updateCheck is false && state is false && mainMenuCheck is false)
                    {
                        Console.WriteLine("OIB koji pretrazujete ne postoji na popisu.");
                        Console.WriteLine("Ako zelite pokusat ponovno upisat neki OIB napisite 'da'.");
                        Console.WriteLine("Ako se zelite vratiti na izbornik Uredivanja stisnite bilo koji drugi botun.");
                        userChoice = Console.ReadLine();

                        if (userChoice is "da" || userChoice is "Da")
                            state = true;
                        else
                            check = true;
                    }
                }
                else
                    check = true;
            } while (state is true);

            return check;
        }

        static bool UpdateCitisenNameAndSurname(bool check, Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            bool state;
            string userChoice, oib, newNameAndSurname;
            DateTime holdDateOfBirth;

            do
            {
                state = false;

                Console.WriteLine("Ako ste sigurni u ovu odluku napisite 'da'.");
                Console.WriteLine("Ako se zelite vratiti na izbornik za uredivanje stanovnika stisnite bilo koju tipku.");
                userChoice = Console.ReadLine();

                if (userChoice is "da" || userChoice is "Da")
                {
                    do
                    {
                        Console.WriteLine("Upisite OIB trazenog stanovnika.");
                        oib= Console.ReadLine();

                        state = CheckOib(oib);
                    } while (state is true);

                    foreach (var item in populationList)
                    {
                        if (item.Key == oib)
                        {
                            do
                            {
                                state = false;
                                Console.WriteLine("Upisite ime i prezime nove osobe:");
                                newNameAndSurname = Console.ReadLine();

                                state = CheckNameAndSurname(newNameAndSurname);
                            } while (state is true);

                            holdDateOfBirth = item.Value.dateOfBirth;

                            populationList.Remove(item.Key);

                            var citizenValue = (nameAndSurname: newNameAndSurname, dateOfBirth: holdDateOfBirth);

                            populationList.Add(item.Key, citizenValue);

                            Console.WriteLine("Uspjesno uredeno ime stanovnika.");
                            Console.WriteLine("Ako zelite urediti jos nekog stanovnika napisite 'da'.");
                            Console.WriteLine("Ako se zelite vratiti na izbornik za uredivanje stanovnika stisnite bilo koju tipku.");
                            userChoice = Console.ReadLine();

                            if (userChoice is "da" || userChoice is "Da")
                                state = true;
                            else
                                check = true;

                            break;
                        }
                    }

                    if (state is false && check is false)
                    {
                        Console.WriteLine("OIB trazenog stanovnika ne postoji u popisu.");
                        Console.WriteLine("Ako zelite pokusat ponovno uredit necije ime i prezime stisnite 'da'.");
                        Console.WriteLine("Ako se zelite vratiti na izbornik za uredivanje stanovnika stisnite bilo koju tipku.");
                        userChoice = Console.ReadLine();

                        if (userChoice is "da" || userChoice is "Da")
                            state = true;
                        else
                            check = true;
                    }
                }

                else
                    check = true;
            
            } while (state is true);

            return check;
        }
    
        static bool UpdateCitizenDateOfBirth(bool check, Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            bool state;
            string userChoice, oib, holdNameAndSurname, newDateOfBirth;

            do
            {
                state = false;

                Console.WriteLine("Ako ste sigurni u ovu odluku napisite 'da'.");
                Console.WriteLine("Ako se zelite vratiti na izbornik za uredivanje stanovnika stisnite bilo koju tipku.");
                userChoice = Console.ReadLine();

                if (userChoice is "da" || userChoice is "Da")
                {
                    do
                    {
                        Console.WriteLine("Upisite OIB trazenog stanovnika.");
                        oib = Console.ReadLine();

                        state = CheckOib(oib);
                    } while (state is true);

                    foreach (var item in populationList)
                    {
                        if (item.Key == oib)
                        {
                            do
                            {
                                state = false;
                                Console.WriteLine("Upisite novi datum rodenja osobe:");
                                newDateOfBirth = Console.ReadLine();

                                state = CheckDateOfBirth(newDateOfBirth);
                            } while (state is true);

                            DateTime newDateTime = Convert.ToDateTime(newDateOfBirth);

                            holdNameAndSurname = item.Value.nameAndSurname;

                            populationList.Remove(item.Key);

                            var personValue = (nameAndSurname: holdNameAndSurname, dateOfBirth: newDateTime);

                            populationList.Add(item.Key, personValue);

                            Console.WriteLine("Uspjesno ureden datum rodenja stanovnika.");
                            Console.WriteLine("Ako zelite urediti jos nekog stanovnika napisite 'da'.");
                            Console.WriteLine("Ako se zelite vratiti na izbornik za uredivanje stanovnika stisnite bilo koju tipku.");
                            userChoice = Console.ReadLine();

                            if (userChoice is "da" || userChoice is "Da")
                                state = true;
                            else
                                check = true;

                            break;
                        }
                    }

                    if (state is false && check is false)
                    {
                        Console.WriteLine("OIB trazenog stanovnika ne postoji u popisu.");
                        Console.WriteLine("Ako zelite pokusat ponovno uredit necije ime i prezime stisnite 'da'.");
                        Console.WriteLine("Ako se zelite vratiti na izbornik za uredivanje stanovnika stisnite bilo koju tipku.");
                        userChoice = Console.ReadLine();

                        if (userChoice is "da" || userChoice is "Da")
                            state = true;
                        else
                            check = true;
                    }
                }

                else
                    check = true;

            } while (state is true);

            return check;
        }
    
        static void Statistics(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            bool check;

            do
            {
                check = false;
                Console.WriteLine("1 - Postotak nezaposlenih (od 0 do 23 godine i od 65 do 100 godine) i postotak zaposlenih (od 23 do 65 godine)");
                Console.WriteLine("2 - Ispis najčešćeg imena i koliko ga stanovnika ima");
                Console.WriteLine("3 - Ispis najčešćeg prezimena i koliko ga stanovnika ima");
                Console.WriteLine("4 - Ispis datum na koji je rođen najveći broj ljudi i koji je to datum");
                Console.WriteLine("5 - Ispis broja ljudi rođenih u svakom od godišnjih doba (poredat godišnja doba s obzirom na broj ljudi rođenih u istim)");
                Console.WriteLine("6 - Ispis najmlađeg stanovnika");
                Console.WriteLine("7 - Ispis najstarijeg stanovnika");
                Console.WriteLine("8 - Prosječan broj godina (na 2 decimale)");
                Console.WriteLine("9 - Medijan godina");
                Console.WriteLine("10 - vracanje a glavni izbornik");
                var switchChoice = Console.ReadLine();

                switch (switchChoice)
                {
                    case "1":
                        check = WorkForcePersentage(check, populationList);
                        break;
                    case "2":
                        check = MostCommonName(check, populationList);
                        break;
                    case "3":
                        check = MostCommonSurname(check, populationList);
                        break;
                    case "4":
                        check = MostCommonDateOfBirth(check, populationList);
                        break;
                    case "5":
                        check = SeasonalDateOfBirth(check, populationList);
                        break;
                    case "6":
                        var sortedDictionaryDescending = from entry in populationList orderby entry.Value.dateOfBirth descending select entry;
                        foreach (var item in sortedDictionaryDescending)
                        {
                            Console.WriteLine("Najmladi stanovnik je:");
                            Console.WriteLine($"{item.Key} {item.Value}");
                            break;
                        }
                        check = true;
                        break;
                    case "7":
                        var sortedDictionaryAscending = from entry in populationList orderby entry.Value.dateOfBirth ascending select entry;
                        foreach (var item in sortedDictionaryAscending)
                        {
                            Console.WriteLine("Najstariji stanovnik je:");
                            Console.WriteLine($"{item.Key} {item.Value}");
                            break;
                        }
                        check = true;
                        break;
                    case "8":
                        check = AverageAge(check, populationList);
                        break;
                    case "9":
                        break;
                    case "10":
                        break;
                    default:
                        Console.WriteLine("Unijeli ste nevaljanu opciju, pokusajte ponovno.");
                        check = true;
                        break;
                }
            } while (check is true);
        }

        static bool WorkForcePersentage(bool check, Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            float countAll = 0, countMinor = 0, countMiddle = 0, countSenior = 0, result;
            DateTime timeNow = DateTime.Now;
            DateTime minorAge = timeNow.AddYears(-23);
            DateTime middleAge = timeNow.AddYears(-65);
            DateTime seniorAge = timeNow.AddYears(-100);

            foreach (var item in populationList)
            {
                result = DateTime.Compare(item.Value.dateOfBirth, minorAge);

                if (result > 0)
                {
                    countMinor++;
                }
                else
                {
                    result = DateTime.Compare(item.Value.dateOfBirth, middleAge);

                    if (result < 0)
                    {
                        countSenior++;
                    }
                    else
                    {
                        countMiddle++;
                    }
                }
                countAll++;
            }

            Console.WriteLine("Postotak nezaposlenih od 0 do 23 godine je " + countMinor / countAll * 100 + "%");
            Console.WriteLine("Postotak zaposlenih od 23 do 65 godine je " + countMiddle / countAll * 100 + "%");
            Console.WriteLine("Postotak nezaposlenih od 65 do 100 godine je " + countSenior / countAll * 100 + "%");

            return check = true;
        }

        static bool MostCommonName(bool check, Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            int counterMostUsedName = 0;
            var nameCounter = new Dictionary<string, int> { { "name", 0 } };
            string nameHolder;

            foreach (var item in populationList)
            {
                nameHolder = item.Value.nameAndSurname.Substring(0, item.Value.nameAndSurname.IndexOf(" "));

                if (nameCounter.ContainsKey(nameHolder))
                    nameCounter[nameHolder]++;
                else
                    nameCounter.Add(nameHolder, 1);
            }

            foreach (var item in nameCounter)
            {
                if (item.Value > counterMostUsedName)
                    counterMostUsedName = item.Value;
            }

            foreach (var item in nameCounter)
                if (item.Value == counterMostUsedName)
                {
                    Console.WriteLine($"{item.Key} {item.Value}");
                }

            return check = true;
        }
    
        static bool MostCommonSurname(bool check, Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            int counterMostUsedSurname = 0;
            var surnameCounter = new Dictionary<string, int> { { "surame", 0 } };
            string surnameHolder;

            foreach (var item in populationList)
            {
                surnameHolder = item.Value.nameAndSurname.Substring(item.Value.nameAndSurname.IndexOf(" "));

                if (surnameCounter.ContainsKey(surnameHolder))
                    surnameCounter[surnameHolder]++;
                else
                    surnameCounter.Add(surnameHolder, 1);
            }

            foreach (var item in surnameCounter)
            {
                if (item.Value > counterMostUsedSurname)
                    counterMostUsedSurname = item.Value;
            }

            foreach (var item in surnameCounter)
                if (item.Value == counterMostUsedSurname)
                {
                    Console.WriteLine($"{item.Key} {item.Value}");
                }

            return check = true;
        }

        static bool MostCommonDateOfBirth(bool check, Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            int counterMostUsedDateOfBirth = 0;
            var dateOfBirthCounter = new Dictionary<string, int> { { "surame", 0 } };
            DateTime dateOfBirthHolder;

            foreach (var item in populationList)
            {
                dateOfBirthHolder = item.Value.dateOfBirth;
                string holdDate = Convert.ToString(dateOfBirthHolder);

                if (dateOfBirthCounter.ContainsKey(holdDate))
                    dateOfBirthCounter[holdDate]++;
                else
                    dateOfBirthCounter.Add(holdDate, 1);
            }

            foreach (var item in dateOfBirthCounter)
            {
                if (item.Value > counterMostUsedDateOfBirth)
                    counterMostUsedDateOfBirth = item.Value;
            }

            foreach (var item in dateOfBirthCounter)
                if (item.Value == counterMostUsedDateOfBirth)
                {
                    Console.WriteLine($"{item.Key} {item.Value}");
                }

            return check = true;
        }

        static bool SeasonalDateOfBirth(bool check, Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            int month, day;
            var seasons = new Dictionary<string, int>
            {
                {"Proljece", 0},
                {"Ljeto", 0},
                {"Jesen", 0},
                {"Zima", 0}
            };

            foreach (var item in populationList)
            {
                month = item.Value.dateOfBirth.Month;
                day = item.Value.dateOfBirth.Day;

                if ((month is 3 && day >= 21) || month is 4 || month is 5 || (month is 6 && day < 21))
                    seasons["Proljece"]++;
                else if ((month is 6 && day >= 21) || month is 7 || month is 8 || (month is 9 && day < 23))
                    seasons["Ljeto"]++;
                else if ((month is 9 && day >= 23) || month is 10 || month is 11 || (month is 12 && day < 21))
                    seasons["Jesen"]++;
                else
                    seasons["Zima"]++;
            }

            var sortedDictionaryDescending = from entry in seasons orderby entry.Value descending select entry;

            foreach (var item in sortedDictionaryDescending)
                Console.WriteLine($"{item.Key} {item.Value}");

            return check = true;
        }

        static bool AverageAge(bool check, Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)> populationList)
        {
            int year, yearSum = 0, thisYear;
            decimal  peopleCounter = 0, averageAge;
            DateTime now = DateTime.Now;

            thisYear = now.Year;
            foreach (var item in populationList)
            {
                year = item.Value.dateOfBirth.Year;

                yearSum += (thisYear - year);

                peopleCounter++;
            }

            averageAge = yearSum / peopleCounter;
            string twoDecimals = averageAge.ToString("00.00");

            Console.WriteLine("Prosjek godina ljudi u popisu je: " + twoDecimals);

            return check = true;
        }
    }
}
