using System;
using System.Collections.Generic;
using Pipeline.Tasks;
using UnityEngine;

namespace Pipeline
{
    public abstract class Pipeline
    {
        private readonly List<EventTask> _tasks = new();
        
        private int _currentIndex = -1;

        public event Action OnFinished;

        public void AddTask(EventTask task)
        {
            _tasks.Add(task);
        }

        public void ClearAll()
        {
            _tasks.Clear();
            Reset();
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        public void RunNextTask()
        {
            _currentIndex++;

            if (_currentIndex >= _tasks.Count)
            {
                OnFinished?.Invoke();
                return;
            }
            
            Debug.Log($"Running {_tasks[_currentIndex].GetType()}");
            _tasks[_currentIndex].Run(OnTaskFinished);
        }

        private void OnTaskFinished()
        {
            Debug.Log($"Finished {_tasks[_currentIndex].GetType()}");
            RunNextTask();
        }
    }
}