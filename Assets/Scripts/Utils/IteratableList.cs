using System.Collections.Generic;

namespace Utils
{
    public class IteratableList<T>
    {
        private readonly List<T> _team;
        private int _currentIndex = -1;

        public int Count => _team.Count;

        public IteratableList(List<T> team)
        {
            _team = team;
        }

        public IteratableList()
        {
            _team = new();
        }

        public bool Remove(T item)
        {
            int index = _team.IndexOf(item);
            
            if (index == -1)
                return false;
            
            if (index < _currentIndex)
                _currentIndex--;
            
            _team.RemoveAt(index);
            return true;
        }

        public void Add(T item)
        {
            _team.Add(item);
        }

        public T GetNext()
        {
            _currentIndex++;
                
            if (_currentIndex >= _team.Count)
                _currentIndex = 0;
                
            return _team[_currentIndex];
        }

        public IEnumerable<T> GetAllNonIteratable() => _team;
        
        public T GetRandom()
        {
            return _team[UnityEngine.Random.Range(0, _team.Count)];
        }
    }
}