//using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices.ComTypes;
//using System.Text;
//using System.Threading.Tasks;

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

            Task taskTemplate1 = new Task("Pocetna stranica", "Napraviti početnu stranicu ", Status.Ended, new TimeSpan(0, 20, 0), projectTemplate2);
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



            void TaskMenu(Project parentProject, TaskManager taskList)
            {
                while (true)
                {
                    Console.WriteLine("1 - Ispis zadataka unutar odabranog projekta\n2 - Prikaz detalja odabranog projekta\n3 - Uređivanje statusa projekta\n4 - Dodavanje zadatka unutar projekta\n5 - Brisanje zadatka iz projekta\n6 - Prikaz ukupno očekivanog vremena potrebnog za sve aktivne zadatke u projektu\n7 - Upravljanje pojedinim zadatkom\n8 - Povratak");
                    string userInput = CheckUserInput("Akcija: ");

                    switch (userInput)
                    {

                        case "1":
                            Console.Clear();
                            Console.WriteLine($"Ispis svih zadataka unutar projekta {parentProject.name}");
                            taskList.ShowTasks();
                            break;

                        case "2":
                            Console.WriteLine("Nalazite se unutar projekta: ");
                            parentProject.ShowProjectData();
                            break;

                        case "3":
                            EditProjectStatus(parentProject, taskList);
                            break;

                        case "4":
                            CreateNewTask(parentProject, taskList);
                            break;

                        case "5":
                            DeleteTask(taskList, parentProject);
                            break;

                        case "6":
                            Console.Clear();
                            Console.WriteLine("Vrijeme potrebno za izradu svih zadataka: " + taskList.SumMinutes());
                            break;
                        case "7":
                            if (allProjects[parentProject].CheckAllTasksStatus() || parentProject.status == Status.Ended)
                            {
                                Console.WriteLine("Ne možete dodavati ni uređivati zadatke.\n");
                                break;
                            }

                            Task choosenTask;
                            string taskName;
                            do
                            {
                                taskName = CheckUserInput("Odaberite zadatak koji želite manipulirati: ");
                                choosenTask = taskList.CheckTaskName(taskName);
                                if (choosenTask == null) Console.WriteLine("Zadatak sa tim imenom ne postoji.\n");
                            } while (choosenTask == null);
                            TasksSubMenu(parentProject, taskList, choosenTask);
                            break;

                        case "8": return;

                        default:
                            Console.Clear();
                            Console.WriteLine("Nepoznata akcija, pokušajte ponovno.\n");
                            break;
                    }
                }
            }

            void CreateNewTask(Project parentProject, TaskManager taskList)
            {
                Console.Clear();
                if (allProjects[parentProject].CheckAllTasksStatus() || parentProject.status == Status.Ended)
                {
                    Console.WriteLine("Ne možete dodavati ni uređivati zadatke.\n");
                    return;
                }
                string taskName = CheckTaskName("Unesite ime zadatka: ", taskList);

                int descriptionLimit = 200;
                string taskDescription = LimitUserInput("Unesite opis zadatka (mora biti ispod 200 znakova): ", descriptionLimit);

                TimeSpan taskDuration = TimeConverter("Unesite trajanje zadatka u minutama: ");
                Task newTask = new Task(taskName, taskDescription, Status.Active, taskDuration, parentProject);
                taskList.AddTask(newTask);
                Console.WriteLine("Zadatak uspješno izrađen!");
                newTask.ShowTaskData();
                return;
            }

            string CheckTaskName(string text, TaskManager taskList)
            {
                int nameLimit = 20;
                string taskName;
                do
                {
                    taskName = LimitUserInput(text, nameLimit);
                    if (taskList.CheckTaskName(taskName) != null) Console.WriteLine("Zadatak sa takvim nazivom već postoji");
                } while (taskList.CheckTaskName(taskName) != null);
                Console.Clear();
                return taskName;
            }

            void EditProjectStatus(Project parentProject, TaskManager taskList)
            {
                if (allProjects[parentProject].CheckAllTasksStatus() || parentProject.status == Status.Ended)
                {
                    Console.WriteLine("Ne možete dodavati ni uređivati zadatke.\n");
                    return;
                }
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Trenutni projekt ima status: ", parentProject.status);
                    Status newStatus = CheckStatus("Unesite status (Active, Waiting, Ended): ");
                    bool ok = parentProject.ChangeStatus(newStatus);
                    if (ok)
                    {
                        Console.WriteLine("Status projekta je uspješno izmijenjen");
                        parentProject.ShowProjectData();
                        break;
                    }
                    else Console.WriteLine("Status nije bilo moguće izmijeniti, pokušajte ponovno.\n");
                }
            }

            void DeleteTask(TaskManager taskList, Project parentProject)
            {
                if (allProjects[parentProject].GetLength() == 0)
                {
                    Console.WriteLine("Ne postoji zadatak kojeg možete obrisati, ovaj projekt nema zadataka.\n");
                    return;
                }
                Task deleteTask;
                do
                {
                    string taskName = CheckUserInput("Unesite naziv zadatka kojeg želite obrisati: ");
                    deleteTask = taskList.CheckTaskName(taskName);
                    if (deleteTask == null) Console.WriteLine("Zadatak sa tim nazivom ne postoji, pokušajte ponovno.");
                } while (deleteTask == null);
                Console.Clear();


                Console.WriteLine("Zadatak pronađen: \n");
                deleteTask.ShowTaskData();
                string userInput = CheckUserInput("Želite li usitinu obrisati ovaj zadatak (y/n): ").ToLower();
                if (userInput == "y")
                {
                    taskList.RemoveTask(deleteTask);
                    Console.WriteLine("Zadatak uspješno obrisan.\n");
                }
                else Console.WriteLine("Odbijeno");
                return;

            }

            TimeSpan TimeConverter(string text)
            {
                string userInput;
                int duration;
                do
                {
                    userInput = CheckUserInput(text);
                    if (!int.TryParse(userInput, out duration)) Console.WriteLine("Unesena vrijednost nije broj, pokušajte ponovno.");
                } while (!int.TryParse(userInput, out duration));
                Console.Clear();
                return TimeSpan.FromMinutes(duration);
            }


            void EditTaskStatus(Task task)
            {
                if (!task.CheckParentStatus())
                {
                    Console.WriteLine("Ne možete dodavati ni uređivati zadatke.\n");
                    return;
                }
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Trenutni zadatak ima status: ", task.status);
                    Status newStatus = CheckStatus("Unesite status (Active, Waiting, Ended): ");
                    bool ok = task.ChangeStatus(newStatus);
                    if (ok)
                    {
                        Console.WriteLine("Status zadatka je uspješno izmijenjen");
                        task.ShowTaskData();
                        break;
                    }
                    else Console.WriteLine("Status nije bilo moguće izmijeniti, pokušajte ponovno.\n");
                }
            }

            void TasksSubMenu(Project parentProject, TaskManager taskList, Task chosenTask)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1 - Prikaz detalja odabranog zadatka \n2 - Uređivanje statusa zadatka\n3 - Povratak");
                    string userInput = CheckUserInput("Akcija: ");
                    switch (userInput)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Odabrali ste ovaj zadatak: ");
                            chosenTask.ShowTaskData();
                            break;
                        case "2":
                            EditTaskStatus(chosenTask);
                            break;
                        case "3":
                            return;
                        default:
                            Console.WriteLine("Nepoznata akcija, pokušajte ponovno.\n");
                            break;
                    }
                    if (userInput == "1") { }
                    else if (userInput == "2") { }
                    else if (userInput == "3") return;
                }
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
                Console.Clear();
                return input;
            }

            string LimitUserInput(string text, int limit)
            {
                string userInput;
                do
                {
                    userInput = CheckUserInput(text);
                } while (userInput.Length > limit);
                Console.Clear();
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



            void ProjectMenu()
            {
                while (true)
                {
                    Console.WriteLine("1 - Ispis projekata i pripadajućih zadataka\n2 - Dodavanje novog projekta\n3 - Brisanje projekta\n4 - Prikaz svih zadataka s rokom u slijedećih 7 dana\n5 - Prikaz projekata filtriranih po statusu\n6 - Upravljanje pojedinim projektom\n7 - Izlaz");
                    string userInput = CheckUserInput("Akcija: ");
                    switch (userInput)
                    {
                        case "1":
                            ShowProjectsAndTasks();
                            break;

                        case "2":
                            CreateNewProject();
                            break;

                        case "3":
                            DeleteProject();
                            break;

                        case "4":
                            Console.Clear();
                            DateTime endDate7Days = DateTime.Today.AddDays(7);

                            bool checkIfPrinted = false;
                            foreach (var project in allProjects.Keys)
                            {
                                if (project.CheckEndDate(endDate7Days)) checkIfPrinted |= true;
                            }
                            if (checkIfPrinted) Console.WriteLine("Ne postoji niti jedan zadatak sa tim kriterijem.\n");
                            break;

                        case "5":
                            ChangeProjectStatus();
                            break;

                        case "6":
                            Console.Clear();
                            Project editProject = CheckProjectExists("Unesite naziv projekta kojim želite upravljati: ");
                            if (editProject == null) Console.WriteLine("Trenutno ne postoji projekti sa tim nazivom. Pokušajte opet.\n");
                            else TaskMenu(editProject, allProjects[editProject]);
                            break;

                        case "7":
                            Console.WriteLine("Doviđenja!");
                            return;

                        default:
                            Console.WriteLine("Nepoznata akcija, pokušajte ponovno.\n");
                            break;

                    }
                }
            }

            void CreateNewProject()
            {
                Console.Clear();
                string projectName;
                string projectDescription;
                DateTime endDate;
                DateTime today = DateTime.Today;
                Status projectStatus = Status.Active;

                do
                {
                    projectName = CheckUserInput("Unesite ime projekta: ");
                    if (CheckProjectName(projectName)) Console.WriteLine("Naziv već postoji, odaberite drugi naziv.\n");
                } while (CheckProjectName(projectName));
                Console.Clear();

                do
                {
                    projectDescription = CheckUserInput("Unesite opis projekta (manje od 200 znakova): ");
                    if (projectDescription.Length > 200) Console.WriteLine("Unos je veći od 200 znakova, pokušajte ponovno.\n");
                } while (projectDescription.Length > 200);
                Console.Clear();

                DateTime startDate = CheckForDateTime("Unesite datum početka projekta (format: dd-mm-yyyy): ");

                do
                {
                    endDate = CheckForDateTime("Unesite datum kraja projekta (format: dd-mm-yyyy): ");
                    if (endDate < startDate) Console.WriteLine("Datum kraja projekta ne smije biti manji od datuma počekta projekta.\n");
                } while (endDate < startDate);
                Console.Clear();

                if (endDate < today) projectStatus = Status.Ended;
                Project newProject = new Project(projectName, projectDescription, startDate, endDate, projectStatus);
                allProjects.Add(newProject, new TaskManager());
                Console.WriteLine("Uspješno izrađen projekt.\n");
                newProject.ShowProjectData();
            }

            void DeleteProject()
            {
                Console.Clear();
                Project delete_project = CheckProjectExists("Unesite naziv projekta kojeg želite obrisati: ");
                string userInput = CheckUserInput("Želite li uistinu obristi ovaj projekt zajedno sa svim njegovim zadacima? (y/n): ").ToLower();
                if (userInput == "y")
                {
                    allProjects.Remove(delete_project);
                    Console.WriteLine("Uspješno obrisan projekt.\n");
                }
                else Console.WriteLine("Odbijeno.\n");
                return;
            }

            void ChangeProjectStatus()
            {
                Console.Clear();
                Status userStatusInput = CheckStatus("Unesite status (Active, Waiting, Ended): ");
                Console.WriteLine("Svi projekti sa unesenim statusom: ");
                bool checkIfPrinted = false;
                foreach (var project in allProjects.Keys)
                {
                    if (project.CheckStatus(userStatusInput))
                    {
                        project.ShowProjectData();
                        checkIfPrinted = true;
                    }
                }

                if (!checkIfPrinted) Console.WriteLine("Ne postoje projekti sa traženim statusom");
            }

            void ShowProjectsAndTasks()
            {
                Console.Clear();
                if (allProjects.Count == 0) Console.WriteLine("Trenutno ne postoje projekti, izradite jedan projekt.\n");
                else
                {
                    Console.WriteLine("Popis svih projekata i pripadajućih zadataka: ");
                    foreach (var project in allProjects)
                    {
                        project.Key.ShowProjectData();
                        project.Value.ShowTasks();
                    }
                }
            }

            DateTime CheckForDateTime(string text)
            {
                DateTime date;
                string dateInput;
                DateTime today = DateTime.Today;
                do
                {
                    dateInput = CheckUserInput(text);
                    if (!DateTime.TryParseExact(dateInput, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out date))
                        Console.WriteLine("Niste dobro unijeli podatke. Pokušajte ponovno.");
                    if (today.Year - 40 > date.Year) Console.WriteLine("Zadatak ne može biti više od 40 godina star.");

                } while (!DateTime.TryParseExact(dateInput, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out date) && today.Year - 40 > date.Year);
                Console.Clear();
                return date;

            }

            Status CheckStatus(string text)
            {
                string userInput;
                Status status;
                do
                {
                    userInput = CheckUserInput(text);
                    if (!Enum.TryParse(userInput, out status) || !Enum.IsDefined(typeof(Status), status)) Console.WriteLine("Unešena vrijednost nije validna, pokušajte ponovno.");
                } while (!Enum.TryParse(userInput, out status) || !Enum.IsDefined(typeof(Status), status));
                Console.Clear();
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

            ProjectMenu();
        }
    }
}
