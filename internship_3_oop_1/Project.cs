using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internship_3_oop_1
{
    public class Project
    {
        public string name;
        public string description;
        DateTime start;
        DateTime end;
        Status status;
       
        public Project(string name, string description, DateTime start, DateTime end, Status status)
        {
            this.name = name;
            this.description = description;
            this.start = start;
            this.end = end;
            this.status = status;
        }
        public void ShowProjectData()
        {
            Console.WriteLine($"Name: {this.name}\nDescription: {this.description}\nStatus: {this.status}\nDuration: {this.start} - {this.end}");
        }

        public void AddDescription(Project project)
        {

        }
    }


    //Funkcija koja vraća projekte na temelju određenog roka
    //Dodavanje projekta (Konstruktor)
    //Filtriranje po statusu
    //Uređivanje projekta?
    //Ispis zadataka unutar jednog projekta
    //uređivanje statusa projekta


}
