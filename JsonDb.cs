using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System;

namespace JsonDb
{
    public class JsonDb<T> : IList<T>
    {
        private IList<T> _rows = null;

        private string _path;
        
        public string Path
        {
            get => _path;
            set => _path = value;
        }

        public int Count => _rows.Count;

        public bool IsReadOnly => _rows.IsReadOnly;

        public T this[int index]
        {
            get => _rows[index];
            set => _rows[index] = value;
        }

        public JsonDb(string path)
        {
            _path = path;

            if (File.Exists(_path))
            {
                using var reader = new StreamReader(_path);
                _rows = JsonSerializer.Deserialize<IList<T>>(reader.ReadToEnd());
            }
            else
            {
                _rows = new List<T>();
            }
        }

        public void Save()
        {
            using var writer = new StreamWriter(_path);
            writer.Write(JsonSerializer.Serialize(_rows));
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _rows.Count; i++)
            {
                if (item.GetHashCode() == _rows[i].GetHashCode())
                {
                    return i;
                }
                continue;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            _rows.Insert(index, item);
        }

        public bool Remove(T item)
        {
            for (int i = 0;  i < _rows.Count; i++)
            {
                if (item.GetHashCode() == _rows[i].GetHashCode())
                {
                    this.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            _rows.RemoveAt(index);
        }

        public void Add(T item)
        {
            _rows.Add(item);
        }

        public void Clear()
        {
            _rows = new List<T>();
        }

        public bool Contains(T item)
        {
            foreach (T row in _rows)
            {
                if (item.GetHashCode()
                    != row.GetHashCode())
                {
                    continue;
                }
                return true;
            }

            return false;
        }

        public bool ContainsAny(IList<T> items)
        {
            foreach (T item in items)
            {
                if (this.Contains(item))
                { 
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _rows.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(_rows);
        }

        public override int GetHashCode()
        {
            int sum = 0;

            foreach (T item in _rows)
            { 
                sum += item.GetHashCode();
            }

            return sum;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is IList && ((IList<T>)obj).Count == _rows.Count)
            {
                for (int i = 0; i < ((IList<T>)obj).Count; i++)
                {
                    if ((obj as IList<T>)[i].GetHashCode() != _rows[i].GetHashCode())
                    {
                        return false;
                    }
                }
                
                return true;
            }

            return false;
        }
    }
}
