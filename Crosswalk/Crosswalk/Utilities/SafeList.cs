using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosswalk
{
    /// <summary>
    /// A list that stores changes to its items until Update() is called. Prevents ordering issues when accessed from multiple sources.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class SafeList<T>: ICollection<T>
    {
        private List<T> Items = new List<T>();
        private List<T> ItemsToAdd = new List<T>();
        private List<T> ItemsToRemove = new List<T>();

        /// <summary>
        /// Item count (after update)
        /// </summary>
        public int Count { get { return Items.Count + ItemsToAdd.Count - ItemsToRemove.Count; } }

        public bool IsReadOnly { get { return false; } }

        bool ClearNextUpdate = false;

        /// <summary>
        /// Commits the changes dictated by ItemsToAdd and ItemsToRemove.
        /// </summary>
        public void Update()
        {
            if (ClearNextUpdate)
            {
                Items.Clear();
                ItemsToAdd.Clear();
                ItemsToRemove.Clear();
                return;
            }

            foreach (T item in ItemsToAdd)
            {
                Items.Add(item);
            }
            ItemsToAdd.Clear();

            foreach (T item in ItemsToRemove)
            {
                Items.Remove(item);
            }
            ItemsToRemove.Clear();
        }
        
        public void Add(T item)
        {
            if(!Items.Contains(item))
                ItemsToAdd.Add(item);
        }
        
        public bool Remove(T item)
        {
            bool contains = Items.Contains(item);
            if(contains)
                ItemsToRemove.Add(item);

            return contains;
        }

        /// <summary>
        /// All lists will be cleared the next time Update() is called.
        /// </summary>
        public void Clear()
        {
            ClearNextUpdate = true;
        }
        
        public bool Contains(T item)
        {
            return Items.Contains(item);
        }

        public int IndexOf(T item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            Items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        public void CopyTo(T[] arr, int index)
        {
            Items.CopyTo(arr, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                Items[index] = value;
            }
        }
    }
}
