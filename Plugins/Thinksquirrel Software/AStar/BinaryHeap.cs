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
		private List<IComparable> mKeys = new List<IComparable>(100);
	    private List<T> mItems = new List<T>(100);
		private HashSet<T> mHash;
	    public BinaryHeap()
		{
			mHash = new HashSet<T>();
		}
		
		public BinaryHeap(IEqualityComparer<T> comparer)
		{
			mHash = new HashSet<T>(comparer);
		}
	
	    /// <summary>
	    /// Get a count of the number of items in the collection.
	    /// </summary>
	    public int Count
	    {
	        get { return mItems.Count; }
	    }
	
	    /// <summary>
	    /// Removes all items from the collection.
	    /// </summary>
	    public void Clear()
	    {
			mKeys.Clear();
	        mItems.Clear();
			mHash.Clear();
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
			mKeys.TrimExcess();
	        mItems.TrimExcess();
			mHash.TrimExcess();
	    }
	
	    /// <summary>
	    /// Inserts an item onto the heap.
	    /// </summary>
	    /// <param name="newItem">The item to be inserted.</param>
	    public void Insert(IComparable key, T newItem)
	    {
	        int i = Count;
			mKeys.Add(key);
	        mItems.Add(newItem);
			mHash.Add(newItem);
	        while (i > 0 && mKeys[((i - 1) / 2)].CompareTo(key) > 0)
	        {
				mKeys[i] = mKeys[mKeys.Count - 1 - ((i - 1) / 2)];
	            mItems[i] = mItems[mKeys.Count - 1 - ((i - 1) / 2)];
	            i = (i - 1) / 2;
	        }
			mKeys[i] = key;
	        mItems[i] = newItem;
	    }
		
	    /// <summary>
	    /// Return the root item from the collection, without removing it.
	    /// </summary>
	    /// <returns>Returns the item at the root of the heap.</returns>
	    public T Peek()
	    {
	        if (mItems.Count == 0)
	        {
	            throw new InvalidOperationException("The heap is empty.");
	        }
	        return mItems[0];
	    }

	    /// <summary>
	    /// Removes and returns the root item from the collection.
	    /// </summary>
	    /// <returns>Returns the item at the root of the heap.</returns>
	    public T RemoveRoot()
	    {
	        if (mItems.Count == 0)
	        {
	            throw new InvalidOperationException("The heap is empty.");
	        }
	        // Get the first item
	        T rslt = mItems[0];
	        // Get the last item and bubble it down.
			IComparable tmpKey = mKeys[mItems.Count - 1];
	        T tmp = mItems[mItems.Count - 1];
	
			mKeys.RemoveAt(mItems.Count - 1);
	        mItems.RemoveAt(mItems.Count - 1);
			mHash.Remove(rslt);	
	        if (mItems.Count > 0)
	        {
	            int i = 0;
	            while (i < mItems.Count / 2)
	            {
	                int j = (2 * i) + 1;
	                if ((j < mItems.Count - 1) && (mKeys[j].CompareTo(mKeys[j + 1]) > 0))
	                {
	                    ++j;
	                }
	                if (mKeys[j].CompareTo(tmpKey) >= 0)
	                {
	                    break;
	                }
					mKeys[i] = mKeys[j];
	                mItems[i] = mItems[j];
	                i = j;
	            }
				mKeys[i] = tmpKey;
	            mItems[i] = tmp;
	        }
	        return rslt;
	    }
		
		public bool Contains(T item)
		{
			return mHash.Contains(item);
		}
		
	    IEnumerator<T> IEnumerable<T>.GetEnumerator()
	    {
	        foreach (var i in mItems)
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