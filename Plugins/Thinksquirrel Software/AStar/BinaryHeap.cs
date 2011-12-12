/// Binary heap classe for A* Pathfinding by Josh Montoute
/// Copyright (c) 2011 Thinksquirrel Software, LLC
/// Based on BinaryHeap.cs by Jim Mischel
///
/// Permission is hereby granted, free of charge, to any person obtaining
/// a copy of this software and associated documentation files (the "Software"),
/// to deal in the Software without restriction, including without limitation the 
/// rights to use, copy, modify, merge, publish, distribute, sublicense, 
/// and/or sell copies of the Software, and to permit persons to whom the Software 
/// is furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in all 
/// copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
/// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
/// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
/// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
/// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
/// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System;
using System.Collections;
using System.Collections.Generic;

namespace ThinksquirrelSoftware.AStar
{
	/// <summary>
	/// Binary Heap class.
	// </summary>
	public class BinaryHeap<T> : IEnumerable<T>
	{
		private List<IComparable> Keys = new List<IComparable>();
	    private List<T> Items = new List<T>();
	    public BinaryHeap() {}
	
	    /// <summary>
	    /// Get a count of the number of items in the collection.
	    /// </summary>
	    public int Count
	    {
	        get { return Items.Count; }
	    }
	
	    /// <summary>
	    /// Removes all items from the collection.
	    /// </summary>
	    public void Clear()
	    {
			Keys.Clear();
	        Items.Clear();
	    }
	
	    /// <summary>
	    /// Sets the capacity to the actual number of elements in the BinaryHeap,
	    /// if that number is less than a threshold value.
	    /// </summary>
	    /// <remarks>
	    /// The current threshold value is 90% (.NET 3.5), but might change in a future release.
	    /// </remarks>
	    public void TrimExcess()
	    {
			Keys.TrimExcess();
	        Items.TrimExcess();
	    }
	
	    /// <summary>
	    /// Inserts an item onto the heap.
	    /// </summary>
	    /// <param name="newItem">The item to be inserted.</param>
	    public void Insert(IComparable key, T newItem)
	    {
	        int i = Count;
			Keys.Add(key);
	        Items.Add(newItem);
	        while (i > 0 && Keys[(i - 1) / 2].CompareTo(key) > 0)
	        {
				Keys[i] = Keys[(i - 1) / 2];
	            Items[i] = Items[(i - 1) / 2];
	            i = (i - 1) / 2;
	        }
			Keys[i] = key;
	        Items[i] = newItem;
	    }
		
	    /// <summary>
	    /// Return the root item from the collection, without removing it.
	    /// </summary>
	    /// <returns>Returns the item at the root of the heap.</returns>
	    public T Peek()
	    {
	        if (Items.Count == 0)
	        {
	            throw new InvalidOperationException("The heap is empty.");
	        }
	        return Items[0];
	    }

	    /// <summary>
	    /// Removes and returns the root item from the collection.
	    /// </summary>
	    /// <returns>Returns the item at the root of the heap.</returns>
	    public T RemoveRoot()
	    {
	        if (Items.Count == 0)
	        {
	            throw new InvalidOperationException("The heap is empty.");
	        }
	        // Get the first item
	        T rslt = Items[0];
	        // Get the last item and bubble it down.
			IComparable tmpKey = Keys[Items.Count - 1];
	        T tmp = Items[Items.Count - 1];
	
			Keys.RemoveAt(Items.Count - 1);
	        Items.RemoveAt(Items.Count - 1);
	
	        if (Items.Count > 0)
	        {
	            int i = 0;
	            while (i < Items.Count / 2)
	            {
	                int j = (2 * i) + 1;
	                if ((j < Items.Count - 1) && (Keys[j].CompareTo(Keys[j + 1]) > 0))
	                {
	                    ++j;
	                }
	                if (Keys[j].CompareTo(tmpKey) >= 0)
	                {
	                    break;
	                }
					Keys[i] = Keys[j];
	                Items[i] = Items[j];
	                i = j;
	            }
				Keys[i] = tmpKey;
	            Items[i] = tmp;
	        }
	        return rslt;
	    }
		
		public bool Contains(T item)
		{
			return Items.Contains(item);
		}
		
		public IComparable GetKey(T item)
		{
			return Keys[Items.IndexOf(item)];
		}
		
		public T[] ToArray()
		{
			return Items.ToArray();
		}
		
	    IEnumerator<T> IEnumerable<T>.GetEnumerator()
	    {
	        foreach (var i in Items)
	        {
	            yield return i;
	        }
	    }

	    public IEnumerator GetEnumerator()
	    {
	        return GetEnumerator();
	    }
	}
}