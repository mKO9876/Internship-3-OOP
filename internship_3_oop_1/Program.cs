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
            Dictionary<Project, TaskManager> allProjects = new Dictionary<Project, TaskManager>();
            Project projectTemplate1 = new Project("C# projekt", "Izrada domaćeg rada", new DateTime(2023, 12, 12), new DateTime(2025, 11, 28), Status.Waiting);
            Project projectTemplate2 = new Project("Web aplikacija", "Prekrasna web aplikacija koju sam sama osmislila", new DateTime(2019, 01, 01), new DateTime(2020, 05, 03), Status.Ended);
            Project projectTemplate3 = new Project("3D modeli", "Modeli svakidašnjih stvari, izrađeno u Blenderu", new DateTime(2023, 09, 13), new DateTime(2024, 11, 23), Status.Active);

            Task taskTemplate3 = new Task("Instalirati VS", "Taman instalirala", Status.Ended, new TimeSpan(0, 70, 0), projectTemplate1);
            Task taskTemplate4 = new Task("Pocetna stranica", "Napraviti početnu stranicu ", Status.Active, new TimeSpan(0, 70, 0), projectTemplate1);

            Task taskTemplate1 = new Task("Pocetna stranica", "Napraviti početnu stranicu ", Status.Ended, new TimeSpan(0 ,20, 0), projectTemplate2);
            Task taskTemplate2 = new Task("Baza podataka", "Moram prvo naučiti kako koristiti bazu", Status.Active, new TimeSpan(0, 120, 0), projectTemplate2);
            
            Task taskTemplate5 = new Task("Izraditi bocu", "Pogledati tutorial kako izraditi najobičniju bocu na YT", Status.Ended, new TimeSpan(0, 10, 0), projectTemplate3);
            Task taskTemplate6 = new Task("Izrada lika u blenderu", "Potrebno je izraditi lika za video igru", Status.Waiting, new TimeSpan(0, 100, 0), projectTemplate3);

            TaskManager taskManagerTemplate1 = new TaskManager();
            TaskManager taskManagerTemplate2 = new TaskManager();
            TaskManager taskManagerTemplate3 = new TaskManager();

            taskManagerTemplate1.AddTask(taskTemplate3);
            taskManagerTemplate1.AddTask(taskTemplate4);

            taskManagerTemplate2.AddTask(taskTemplate1);
            taskManagerTemplate2.AddTask(taskTemplate2);

            taskManagerTemplate3.AddTask(taskTemplate5);
            taskManagerTemplate3.AddTask(taskTemplate6);

            allProjects.Add(projectTemplate1, taskManagerTemplate1);
            allProjects.Add(projectTemplate2, taskManagerTemplate2);
            allProjects.Add(projectTemplate3, taskManagerTemplate3);
            void ProjectMenu()
            {
                while (true)
                {
                    Console.WriteLine("1 - Ispis projekata i pripadajućih zadataka\n2 - Dodavanje novog projekta\n3 - Brisanje projekta\n4 - Prikaz svih zadataka s rokom u slijedećih 7 dana\n5 - Prikaz projekata filtriranih po statusu\n6 - Upravljanje pojedinim projektom\n7 -Izlaz");
                    string user_input = CheckUserInput("Akcija: ");
                    if (user_input == "1")
                    {
                        Console.Clear();
                        if (allProjects.Count == 0) Console.WriteLine("Trenutno ne postoje projekti, izradite jedan projekt.\n");
                        else
                        {
                            Console.WriteLine("Popis svih zadataka: ");
                            foreach (var project in allProjects)
                            {
                                project.Key.ShowProjectData();
                                project.Value.ShowTasks();
                            }
                            Console.ReadKey();
                        }
                    }
                    else if (user_input == "2")
                    {
                        Console.Clear();
                        string projectName;
                        string projectDescription;
                        DateTime endDate;
                        DateTime today = DateTime.Today;
                        Status projectStatus = Status.Active;

                        do
                        {
                            projectName = CheckUserInput("Unesite ime projekta (mora biti manje od 20 znakova): ");
                            if (projectName.Length > 20) Console.WriteLine("Naziv je dug. Unesite ponovno.\n");
                            if (CheckProjectName(projectName)) Console.WriteLine("Naziv već postoji, odaberite drugi naziv.\n");
                        } while (CheckProjectName(projectName) || projectName.Length > 20);

                        do
                        {
                            projectDescription = CheckUserInput("Unesite opis projekta (manje od 200 znakova): ");
                            if (projectDescription.Length > 200) Console.WriteLine("Unos je veći od 200 znakova, pokušajte ponovno.\n");
                        } while (projectDescription.Length > 200);

                        DateTime startDate = CheckForDateTime("Unesite datum početka projekta (format: dd-mm-yyyy): ");

                        do
                        {
                            endDate = CheckForDateTime("Unesite datum kraja projekta (format: dd-mm-yyyy): ");
                            if (endDate < startDate) Console.WriteLine("Datum kraja projekta ne smije biti manji od datuma počekta projekta.\n");
                        } while (endDate < startDate);

                        if (endDate < today) projectStatus = Status.Ended;
                        Project new_project = new Project(projectName, projectDescription, startDate, endDate, projectStatus);
                        allProjects.Add(new_project, new TaskManager());
                        Console.WriteLine("Uspješno izrađen projekt.\n");
                    }

                    else if (user_input == "3")
                    {
                        while (true)
                        {
                            Console.Clear();
                            Project delete_project = CheckProjectExists("Unesite naziv projekta kojeg želite obrisati: ");
                            user_input = CheckUserInput("Želite li uistinu obristi ovaj projekt zajedno sa svim njegovim zadacima? (y/n): ").ToLower();
                            if (user_input == "y")
                            {
                                allProjects.Remove(delete_project);
                                Console.WriteLine("Uspješno obrisan projekt.\n");
                            }
                            else Console.WriteLine("Odbijeno.\n");
                            break;
                        }
                    }

                    else if (user_input == "4")
                    {
                        Console.Clear();
                        DateTime end_date_in_7_days = DateTime.Today.AddDays(7);
                        foreach (var project in allProjects.Keys) project.CheckEndDate(end_date_in_7_days);
                    }

                    else if (user_input == "5")
                    {
                        Console.Clear();
                        Status user_input_status = CheckStatus("Unesite status (Active, Waiting, Ended): ");
                        Console.WriteLine("Svi projekti sa unesenim statusom: ");
                        foreach (var project in allProjects.Keys) project.CheckStatus(user_input_status);
                    }

                    else if (user_input == "6")
                    {
                        Console.Clear();

                        Project edit_project = CheckProjectExists("Unesite naziv projekta kojim želite upravljati: ");
                        if (edit_project == null) Console.WriteLine("Trenutno ne postoji projekti sa tim nazivom. Pokušajte opet.\n");
                        else TaskMenu(edit_project, allProjects[edit_project]);
                    }

                    else if (user_input == "7")
                    {
                        Console.WriteLine("Doviđenja!");
                        break;
                    }

                    else Console.WriteLine("Nepoznata akcija, pokušajte ponovno.\n");
                }
            }

            void TaskMenu(Project parent_project, TaskManager task_list)
            {
                while (true)
                {
                    Console.WriteLine("1 - Ispis zadataka unutar odabranog projekta\n2 - Prikaz detalja odabranog projekta\n3 - Uređivanje statusa projekta\n4 - Dodavanje zadatka unutar projekta\n5 - Brisanje zadatka iz projekta\n6 - Prikaz ukupno očekivanog vremena potrebnog za sve aktivne zadatke u projektu\n7 - Upravljanje pojedinim zadatkom\n8 - Povratak");
                    string user_input = CheckUserInput("Akcija: ");
                    if (user_input == "1")
                    {
                        Console.Clear();
                        Console.WriteLine($"Ispis svih zadataka unutar projekta {parent_project.name}");
                        task_list.ShowTasks();
                    }
                    else if (user_input == "2")
                    {
                        Console.Clear();
                        Console.WriteLine("Nalazite se unutar projekta: ");
                        parent_project.ShowProjectData();
                    }
                    else if (user_input == "3")
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Trenutni projekt ima status: ", parent_project.status);
                            Status new_status = CheckStatus("Unesite status (Active, Waiting, Ended): ");
                            bool ok = parent_project.ChangeStatus(new_status);
                            if (ok)
                            {
                                Console.WriteLine("Status projekta je uspješno izmijenjen");
                                parent_project.ShowProjectData();
                                break;
                            }
                            else Console.WriteLine("Status nije bilo moguće izmijeniti, pokušajte ponovno.\n");
                        }

                    }

                    else if (user_input == "4")
                    {
                        Console.Clear();
                        string task_name = CheckTaskName("Unesite ime zadatka: ", task_list);

                        int description_limit = 200;
                        string task_description = LimitUserInput("Unesite opis zadatka (mora biti ispod 200 znakova): ", description_limit);

                        TimeSpan task_duration = TimeConverter("Unesite trajanje zadatka u minutama");
                        Task new_task = new Task(task_name, task_description, Status.Active, task_duration, parent_project);
                        task_list.AddTask(new_task);
                        Console.WriteLine("Zadatak uspješno izrađen!");
                        new_task.ShowTaskData();
                        break;
                    }
                    else if (user_input == "5")
                    {
                        while (true)
                        {
                            string task_name = CheckUserInput("Unesite naziv zadatka kojeg želite obrisati: ");
                            Task delete_task = task_list.CheckTaskName(task_name);
                            if (delete_task != null)
                            {
                                Console.WriteLine("Zadatak pronađen.");
                                delete_task.ShowTaskData();
                                user_input = CheckUserInput("Želite li usitinu obrisati ovaj zadatak (y/n): ").ToLower();
                                if (user_input == "y") task_list.RemoveTask(delete_task);
                                else Console.WriteLine("Odbijeno");
                                break;
                            }
                            else Console.WriteLine("Zadatak sa tim nazivom ne postoji, pokušajte ponovno.");
                        }
                    }
                    else if (user_input == "6")
                    {
                        Console.Clear();
                        Console.WriteLine($"Vrijeme potrebno za izradu svih zadataka: ", task_list.SumTime());
                    }
                    else if (user_input == "7")
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("1 - Prikaz detalja odabranog zadatka \n2 - Uređivanje statusa zadatka\n3 - Povratak");
                            user_input = CheckUserInput("Akcija: ");
                            if (user_input == "1") { }
                            else if (user_input == "2") { }
                            else if (user_input == "3") break;
                            else Console.WriteLine("Nepoznata akcija, pokušajte ponovno.\n");
                        }
                    }
                    else if (user_input == "8") break;
                    else Console.WriteLine("Nepoznata akcija, pokušajte ponovno.\n");
                }
            }

            string LimitUserInput(string text, int limit)
            {
                string userInput;
                do
                {
                    userInput = CheckUserInput(text);
                } while (userInput.Length > limit);
                return userInput;
            }

            bool CheckProjectName(string possibleName)
            {
                foreach (var project in allProjects.Keys)
                {
                    if (project.name == possibleName) return true;
                }
                return false;
            }

            string CheckTaskName(string text, TaskManager task_list)
            {
                int nameLimit = 20;
                string taskName;
                do
                {
                    taskName = LimitUserInput(text, nameLimit);
                    if (task_list.CheckTaskName(taskName) != null) Console.WriteLine("Zadatak sa takvim nazivom već postoji");
                } while (task_list.CheckTaskName(taskName) != null);
                return taskName;
            }

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
                string dateInput;
                DateTime today = DateTime.Today;
                do
                {
                    dateInput = CheckUserInput(text);

                } while (!DateTime.TryParseExact(dateInput, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out date) && today.Year - 40 < date.Year);
                return date;

            }

            Status CheckStatus(string text)
            {
                string userInput;
                Status status;
                do
                {
                    userInput = CheckUserInput(text);
                } while (!Enum.TryParse(userInput, out status) || !Enum.IsDefined(typeof(Status), status));
                return status;
            }

            Project CheckProjectExists(string text)
            {
                string userInput;
                while (true)
                {
                    userInput = CheckUserInput(text);
                    foreach (var project in allProjects)
                    {
                        if (project.Key.name == userInput)
                        {
                            Console.WriteLine("Projekt uspješno nađen.\n");
                            project.Key.ShowProjectData();
                            Console.WriteLine("Zadaci unutar projekta:\n");
                            project.Value.ShowTasks();
                            return project.Key;
                        }
                    }
                }
            }

            TimeSpan TimeConverter(string text)
            {
                string userInput;
                int duration;
                do
                {
                    userInput = CheckUserInput(text);
                    if (!int.TryParse(userInput, out duration)) Console.WriteLine("Unesena vrijednost nije broj, pokušajte ponovno.");
                } while (int.TryParse(userInput, out duration));
                return TimeSpan.FromMinutes(duration);
            }

            ProjectMenu();
        }
    }
}
