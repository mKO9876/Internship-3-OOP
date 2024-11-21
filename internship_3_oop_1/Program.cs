using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace internship_3_oop_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dobrodošli!");
            Dictionary<Project, TaskManager> all_projects = new Dictionary<Project, TaskManager>();
            void App()
            {
                while (true)
                {
                    Console.WriteLine("1 - Ispis projekata i pripadajućih zadataka\n2 - Dodavanje novog projekta\n3 - Brisanje projekta\n4 - Prikaz svih zadataka s rokom u slijedećih 7 dana\n5 - Prikaz projekata filtriranih po statusu\n6 - Upravljanje pojedinim projektom\n7 -Izlaz");
                    string user_input = CheckUserInput("Akcija: ");
                    if (user_input == "1")
                    {
                        if (all_projects.Count == 0) Console.WriteLine("Trenutno ne postoje projekti, izradite jedan projekt.\n");
                        else
                        {
                            foreach (var project in all_projects)
                            {
                                project.Key.ShowProjectData();
                                project.Value.ShowTasks();
                            }
                        }
                    }
                    else if (user_input == "2")
                    {
                        string project_name;
                        string project_description;
                        DateTime start_date;
                        DateTime end_date;
                        DateTime today = DateTime.Today;
                        Status project_status = Status.Active;

                        do
                        {
                            project_name = CheckUserInput("Unesite ime projekta (mora biti manje od 20 znakova): ");
                            if (project_name.Length > 20) Console.WriteLine("Naziv je dug. Unesite ponovno.\n");
                            if (CheckForSameProjectName(project_name)) Console.WriteLine("Naziv već postoji, odaberite drugi naziv.\n");
                        } while (CheckForSameProjectName(project_name) || project_name.Length > 20);

                        do
                        {
                            project_description = CheckUserInput("Unesite opis projekta (manje od 200 znakova): ");
                            if (project_description.Length > 200) Console.WriteLine("Unos je veći od 200 znakova, pokušajte ponovno.\n");
                        } while (project_description.Length > 200);

                        start_date = CheckForDateTime("Unesite datum početka projekta (format: dd-mm-yyyy): ");

                        do
                        {
                            end_date = CheckForDateTime("Unesite datum kraja projekta (format: dd-mm-yyyy): ");
                            if (end_date < start_date) Console.WriteLine("Datum kraja projekta ne smije biti manji od datuma počekta projekta.\n");
                        } while (end_date < start_date);

                        if (end_date < today) project_status = Status.Ended;
                        Project new_project = new Project(project_name, project_description, start_date, end_date, project_status);
                        all_projects.Add(new_project, new TaskManager());
                        Console.WriteLine("Uspješno izrađen projekt.\n");
                    }

                    else if (user_input == "3")
                    {

                    }

                    else if (user_input == "4")
                    {

                    }

                    else if (user_input == "5")
                    {

                    }

                    else if (user_input == "6")
                    {

                    }

                    else if (user_input == "7")
                    {
                        Console.WriteLine("Odabrali ste: Izlazak iz aplikacije");
                        break;
                    }

                    else Console.WriteLine("Nepoznata akcija, pokušajte ponovno");
                }
            }

            bool CheckForSameProjectName(string possible_name)
            {
                foreach (var project in all_projects)
                {
                    if (project.Key.name == possible_name) return true;
                }
                return false;
            }

            //bool CheckForSameTaskName(string possible_name, Project project_parent)
            //{
            //    foreach (var task in all_projects[project_parent])
            //    {
            //        if (task.name == possible_name) return true;
            //    }
            //    return false;
            //}

            string CheckUserInput(string text)
            {
                string input;
                do
                {
                    Console.Write(text);
                    input = Console.ReadLine();
                    if (String.IsNullOrEmpty(input)) Console.WriteLine("Unos ne smije biti prazan.");
                } while (String.IsNullOrEmpty(input));

                return input;
            }

            DateTime CheckForDateTime(string text)
            {
                DateTime date;
                string date_input;
                DateTime today = DateTime.Today;
                do
                {
                    date_input = CheckUserInput(text);

                } while (!DateTime.TryParseExact(date_input, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out date));
                return date;

            }

            App();
        }
    }
}
