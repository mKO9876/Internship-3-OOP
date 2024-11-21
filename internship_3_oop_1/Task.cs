using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internship_3_oop_1
{
    public class Task
    {
        public string name;
        private string description;
        private Status status;
        private DateTime duration_min;
        private string project_parent;

        public Task()
        {

        }


        public void ShowTaskData()
        {
            Console.WriteLine($"Name: {this.name}\nDescription: {this.description}\nStatus: {this.status}\nParent Project: {project_parent}\nDuration (min): {duration_min}");
        }
    }



    

    //Uređivanje statusa zadatka
    //Prikaz detalja zadatka
}
