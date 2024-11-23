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
            this.task_list.Add(task);
            Console.WriteLine($"Zadatak uspješno dodan.");
        }

        public void ShowTasks()
        {
            foreach (Task task in this.task_list) task.ShowTaskData();
        }

        //// Method to remove a task by name (or other identifiers)
        public void RemoveTask(Task task)
        {

            this.task_list.Remove(task);
            Console.WriteLine($"Zadatak '{task.name}' obrisan.");
            return;
        }

        public Task CheckTaskName(string task_name)
        {
            foreach (var task in this.task_list)
            {
                if (task.name == task_name) return task;
            }
            return null;
        }

        public TimeSpan SumTime()
        {
            TimeSpan sum_all_tasks = TimeSpan.Zero;
            foreach (var task in this.task_list)
            {
                sum_all_tasks += task.durationMin;
            }
            return sum_all_tasks;
        }
    }
}
