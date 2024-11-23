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
        public string description;
        public Status status;
        public TimeSpan durationMin;
        Project parentProject;

        public Task(string n, string d, Status s, TimeSpan duration, Project parent )
        {
            this.name = n; 
            this.description = d; 
            this.status = s;
            this.durationMin = duration;
            this.parentProject = parent;
        }

        public bool CheckParentStatus() //provjeri možeš li uopće mijenjati išta ako je roditelj gotov
        {
            if (this.parentProject.status == Status.Ended) return false;
            else return true;
        }

        public void ShowTaskData()
        {
            Console.WriteLine($"Name: {this.name}\nDescription: {this.description}\nStatus: {this.status}\nParent Project: {parentProject.name}\nDuration (min): {durationMin.TotalMinutes}\n");
        }

        public bool CheckStatus(Status status)
        {
            if (this.status == status) return true;
            return false;
        }

        public bool ChangeStatus(Status status)
        {
            if (this.status != status) return true;
            return false;
        }
    }
}
