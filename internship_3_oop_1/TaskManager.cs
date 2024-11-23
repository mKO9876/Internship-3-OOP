using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internship_3_oop_1
{
    class TaskManager
    {
        List<Task> taskList = new List<Task>();

        public void AddTask(Task task)
        {
            this.taskList.Add(task);
        }

        public void ShowTasks()
        {
            foreach (Task task in this.taskList)
            {
                task.ShowTaskData();
                Console.WriteLine("-------------------------------");
            }
        }

        public int GetLength()
        {
            return this.taskList.Count;
        }

        public bool CheckAllTasksStatus()
        {
            foreach (Task task in this.taskList)
            {
                if (task.status != Status.Ended) return true;
            }
            return false;
        }

        //// Method to remove a task by name (or other identifiers)
        public void RemoveTask(Task task)
        {

            this.taskList.Remove(task);
            return;
        }

        public Task CheckTaskName(string taskName)
        {
            foreach (var task in this.taskList)
            {
                if (task.name == taskName) return task;
            }
            return null;
        }

        public string SumMinutes()
        {
            double totalMinutes = 0;
            foreach (var task in this.taskList)
            {
                totalMinutes += task.durationMin.TotalMinutes;
            }
            return totalMinutes.ToString();
        }

       
    }
}
