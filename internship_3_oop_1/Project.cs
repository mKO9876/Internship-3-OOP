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
        readonly DateTime start;
        readonly DateTime end;
        public Status status;

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
            Console.WriteLine($"Name: {this.name}\nDescription: {this.description}\nStatus: {this.status}\nDuration: {this.start.ToString("dd/MM/yyyy")} - {this.end.ToString("dd/MM/yyyy")}\n");
        }

        public bool CheckEndDate(DateTime dueDate)
        {
            DateTime today = DateTime.Today;
            if (this.end.Date >= today.Date && this.end.Date <= dueDate.Date)
            {
                this.ShowProjectData();
                return true;
            }
            return false;

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
