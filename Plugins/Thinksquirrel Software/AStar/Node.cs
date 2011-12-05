/// Graph node classes for A* Pathfinding by Josh Montoute
/// For COSC 4322
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
using UnityEngine;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ThinksquirrelSoftware.AStar
{
	public class Node<TValue>
		where TValue : struct
	{
		private NodeList<TValue> mNeighbors;
		private List<float> mCosts;
		private TValue mValue;
		
		public NodeList<TValue> Neighbors
		{
			get
			{
				if (mNeighbors == null)
					mNeighbors = new NodeList<TValue>();
					
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
		public TValue Value
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
		public Node(TValue value) : this(value, null) {}
		public Node(TValue value, NodeList<TValue> neighbors)
		{
		   this.mValue = value;
           this.mNeighbors = neighbors;
		}
	}
	
	public class NodeList<TValue> : List<Node<TValue>>
		where TValue : struct
	{
	    public NodeList() : base() { }

	    public NodeList(int initialSize)
	    {
	        for (int i = 0; i < initialSize; i++)
	            base.Add(default(Node<TValue>));
	    }

	    public Node<TValue> FindByValue(TValue value)
	    {
	        foreach (Node<TValue> node in this)
	            if (node.Value.Equals(value))
	                return node;

	        return null;
	    }
	}
	
	public class Graph<TValue> : IEnumerable<TValue>
		where TValue : struct
	{
	    private NodeList<TValue> nodeSet;

	    public Graph() : this(null) {}
	    public Graph(NodeList<TValue> nodeSet)
	    {
	        if (nodeSet == null)
	            this.nodeSet = new NodeList<TValue>();
	        else
	            this.nodeSet = nodeSet;
	    }

	    public void AddNode(Node<TValue> node)
	    {
	        nodeSet.Add(node);
	    }

	    public void AddNode(TValue value)
	    {
	        nodeSet.Add(new Node<TValue>(value));
	    }

	    public bool AddDirectedEdge(Node<TValue> from, Node<TValue> to, float cost)
	    {
			if (!from.Neighbors.Contains(to))
			{
	        	from.Neighbors.Add(to);
				from.Costs.Add(cost);
				return true;
			}
			
			return false;
	    }

	    public bool AddUndirectedEdge(Node<TValue> from, Node<TValue> to, float cost)
	    {

			if (!from.Neighbors.Contains(to) && !to.Neighbors.Contains(from))
			{
	        	from.Neighbors.Add(to);
	        	from.Costs.Add(cost);
	
	        	to.Neighbors.Add(from);
	        	to.Costs.Add(cost);
				return true;
			}
			
			return false;
	    }

	    public bool Contains(TValue value)
	    {
	        return nodeSet.FindByValue(value) != null;
	    }

	    public bool Remove(TValue value)
	    {
	        Node<TValue> nodeToRemove = (Node<TValue>) nodeSet.FindByValue(value);
	        
			if (nodeToRemove == null)
	            return false;

	        nodeSet.Remove(nodeToRemove);

	        foreach (Node<TValue> node in nodeSet)
	        {
	            int index = node.Neighbors.IndexOf(nodeToRemove);
	
	            if (index != -1)
	            {
	                node.Neighbors.RemoveAt(index);
	                node.Costs.RemoveAt(index);
	            }
	        }

	        return true;
	    }

	    public NodeList<TValue> Nodes
	    {
	        get
	        {
	            return nodeSet;
	        }
	    }
	
	    public Node<TValue> this[int i]
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
	
		IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
		{
		    return (IEnumerator<TValue>)nodeSet;			  
		}
		
		IEnumerator IEnumerable.GetEnumerator()
		{
		    return nodeSet.GetEnumerator();			  
		}
	}
}