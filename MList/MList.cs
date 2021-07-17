using System;
using System.Collections;
using System.Collections.Generic;

namespace MList
{
    public class MList<T> : IEnumerable<T>
    {
        public MList()
        {
            _array = new T[32];
        }

        private T[] _array;
        private int _maxIndex = 0;

        public T this[int index]
        {
            get
            {
                if (index > _maxIndex)
                    throw new ArgumentNullException(nameof(index));

                return _array[index];
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (index > _maxIndex)
                    throw new IndexOutOfRangeException();

                _array[index] = value;
            }
        }
        public void Add(T value)
        {
            if (_array.Length == _maxIndex + 1)
                AddLength();
            _array[_maxIndex] = value;
            _maxIndex++;
        }

        public void Remove(T value)
        {
            bool replace = false;
            for (int i = 0; i < _maxIndex; i++)
            {
                T item = _array[i];
                if (!replace && item.Equals(value))
                {
                    Remove(i);
                    break;
                }
            }
        }

        public bool Any(Func<T, bool> expression)
        {
            for (int i = 0; i < _maxIndex; i++)
            {
                T item = _array[i];

                if (expression(item))
                    return true;
            }
            return false;
        }

        public void RemoveAll(Func<T, bool> expression)
        {
            for (int i = _maxIndex - 1; i >= 0; i--)
            {
                var item = _array[i];
                if (expression(item))
                    Remove(i);
            }
        }
        private void Remove(int index)
        {
            for (int i = index; i < _maxIndex; i++)
            {
                _array[i] = _array[i + 1];
            }
            _maxIndex--;
            Clean();

        }
        public MList<T> Where(Func<T, bool> expression)
        {
            MList<T> list = new MList<T>();
            for (int i = 0; i < _maxIndex; i++)
            {
                T item = _array[i];
                if (expression(item))
                    list.Add(item);
            }
            return list;
        }
        public void ForEach(Action<T> action)
        {
            for (int i = 0; i < _maxIndex; i++)
            {
                T item = _array[i];
                action(item);
            }
        }

        public T First(Func<T, bool> expression)
        {
            for (int i = 0; i < _maxIndex; i++)
            {
                var item = _array[i];
                if (expression(item))
                {
                    return item;
                }
            }
            return default;
        }

        public int Count
        {
            get
            {
                return _maxIndex;
            }
        }

        private void AddLength()
        {
            int length = _array.Length;
            length += 32;

            T[] array = new T[length];

            for (int i = 0; i < _array.Length; i++)
            {
                array[i] = _array[i];
            }

            _array = array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _maxIndex; i++)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Clean()
        {
            int bigger = _array.Length - _maxIndex;
            if (bigger > 32)
            {

                T[] arr = new T[_array.Length - 32];

                for(int i = 0; i<_maxIndex; i++)
                {
                    arr[i] = _array[i];
                }
                _array = arr;
            }
        }
    }
}
