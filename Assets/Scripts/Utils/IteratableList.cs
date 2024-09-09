using System.Collections.Generic;

namespace Utils
{
    public class IteratableList<T>
    {
        private readonly IReadOnlyList<T> _team;
        private int _currentIndex = -1;

        public IteratableList(IReadOnlyList<T> team)
        {
            _team = team;
        }

        public T GetNext()
        {
            _currentIndex++;
                
            if (_currentIndex >= _team.Count)
                _currentIndex = 0;
                
            return _team[_currentIndex];
        }
    }
}