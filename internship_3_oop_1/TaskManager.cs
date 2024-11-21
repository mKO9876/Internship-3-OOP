using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internship_3_oop_1
{
    class TaskManager
    {
        List<Task> task_list = new List<Task>();

        public void AddTask(Task task)
        {
            task_list.Add(task);
            Console.WriteLine($"Zadatak'{task.name}' uspješno dodan.");
        }

        public void ShowTasks()
        {
            foreach (Task task in task_list) task.ShowTaskData();
        }

        //// Method to remove a task by name (or other identifiers)
        public void RemoveTask(string task_name)
        {
            foreach (var task in task_list)
            {
                if(task.name == task_name)
                {
                    bool removed = task_list.Remove(task);
                    Console.WriteLine($"Task '{task_name}' removed.");
                    return;
                }
            }
            Console.WriteLine($"Task '{task_name}' not found.");
        }

        public bool CheckTaskName(string task_name)
        {
            foreach (var task in task_list)
            {
                if (task.name == task_name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
