/// A* Pathfinding - Graph node classes
/// Copyright (c) 2011 Thinksquirrel Software, LLC
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
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ThinksquirrelSoftware.AStar
{
	public class Node<T>
	{
		private bool mEnabled = true;
		private NodeList<T> mNeighbors;
		private List<float> mCosts;
		private T mValue;
		
		public bool Enabled
		{
			get
			{
				return mEnabled;
			}
		}
		public NodeList<T> Neighbors
		{
			get
			{
				if (mNeighbors == null)
					mNeighbors = new NodeList<T>();
					
				return mNeighbors;
			}
		}
		
		public List<float> Costs
		{
			get
			{
				if (mCosts == null)
					mCosts = new List<float>();
					
				return mCosts;
			}
		}
		public T Value
		{
			get
			{
				return mValue;
			}
			set
			{
				mValue = value;
			}
		}
		
		public Node() {}
		public Node(T value) : this(value, null, true) {}
		public Node(T value, bool enabled) : this(value, null, enabled) {}
		public Node(T value, NodeList<T> neighbors) : this(value, neighbors, true) {}
		public Node(T value, NodeList<T> neighbors, bool enabled)
		{
			this.mValue = value;
			this.mNeighbors = neighbors;
			this.mEnabled = enabled;
		}
		
		public void Toggle(bool toggle)
		{
			mEnabled = toggle;
		}
		
	}
	
	public class NodeList<T> : List<Node<T>>
	{
	    public NodeList() : base() { }

	    public NodeList(int initialSize)
	    {
	        for (int i = 0; i < initialSize; i++)
	            base.Add(default(Node<T>));
	    }

	    public Node<T> FindByValue(T value)
	    {
	        foreach (Node<T> node in this)
	            if (node.Value.Equals(value))
	                return node;

	        return null;
	    }
	}
	
	public class Graph<T> : IEnumerable<T>
	{
		private bool mChanged;
		
		public bool Changed
		{
			get
			{
				return mChanged;
			}
		}
		public delegate void GraphChangeHandler();
		public event GraphChangeHandler GraphChange;
		
		public void PushGraphChange()
		{
			if (mChanged)
			{
				if (GraphChange != null)
				{
					GraphChange();
				}
				mChanged = false;
			}
		}
		
	    private NodeList<T> nodeSet;

	    public Graph() : this(null) {}
	    public Graph(NodeList<T> nodeSet)
	    {
	        if (nodeSet == null)
	            this.nodeSet = new NodeList<T>();
	        else
	            this.nodeSet = nodeSet;
	    }

	    public Node<T> AddNode(Node<T> node)
	    {
	        nodeSet.Add(node);
			mChanged = true;
			return node;
	    }

	    public Node<T> AddNode(T value)
	    {
			Node<T> node = new Node<T>(value);
	        nodeSet.Add(node);
			mChanged = true;
			return node;
	    }
		
		
		public void ToggleNode(Node<T> node)
		{
			node.Toggle(!node.Enabled);
			mChanged = true;
		}
		
		public void ToggleNode(Node<T> node, bool enabled)
		{
			if (node.Enabled != enabled)
			{
				node.Toggle(enabled);
				mChanged = true;
			}
		}

	    public bool AddDirectedEdge(Node<T> from, Node<T> to, float cost)
	    {
			if (!from.Neighbors.Contains(to))
			{
	        	from.Neighbors.Add(to);
				from.Costs.Add(cost);
				mChanged = true;
				return true;
			}
			
			return false;
	    }

	    public bool AddUndirectedEdge(Node<T> from, Node<T> to, float cost)
	    {
			if (from == null || to == null)
				return false;
				
			if (!from.Neighbors.Contains(to) && !to.Neighbors.Contains(from))
			{
	        	from.Neighbors.Add(to);
	        	from.Costs.Add(cost);
	
	        	to.Neighbors.Add(from);
	        	to.Costs.Add(cost);
				mChanged = true;
				return true;
			}
			
			return false;
	    }

	    public bool Contains(T value)
	    {
	        return nodeSet.FindByValue(value) != null;
	    }
	
		public Node<T> Find(T value)
		{
			return nodeSet.FindByValue(value);
		}

	    public bool Remove(T value)
	    {
	        Node<T> nodeToRemove = (Node<T>) nodeSet.FindByValue(value);
	        
			if (nodeToRemove == null)
	            return false;
			
	        nodeSet.Remove(nodeToRemove);

	        foreach (Node<T> node in nodeSet)
	        {
	            int index = node.Neighbors.IndexOf(nodeToRemove);
	
	            if (index != -1)
	            {
	                node.Neighbors.RemoveAt(index);
	                node.Costs.RemoveAt(index);
	            }
	        }
			
			mChanged = true;
	        return true;
	    }

	    public NodeList<T> Nodes
	    {
	        get
	        {
	            return nodeSet;
	        }
	    }
	
	    public Node<T> this[int i]
	    {
	        get
	        {
	            return nodeSet[i];
	        }
	        set
	        {
	            nodeSet[i] = value;
	        }
	    }
	
	    public int Count
	    {
	        get { return nodeSet.Count; }
	    }
	
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
		    return (IEnumerator<T>)nodeSet;			  
		}
		
		IEnumerator IEnumerable.GetEnumerator()
		{
		    return nodeSet.GetEnumerator();			  
		}
	}
}