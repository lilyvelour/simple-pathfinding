/// A* Pathfinding - Path class
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
	/// Represents a path of nodes.
	public class Path<T>
	{
		private Dictionary<Node<T>, Node<T>> path = new Dictionary<Node<T>, Node<T>>();
		private Node<T> goal;
		
		public Node<T> Goal
		{
			get
			{
				return goal;
			}
			set
			{
				goal = value;
			}
		}
		
		public void Add(Node<T> node, Node<T> parent)
		{
			path.Add(node, parent);
		}
		
		public bool Contains(Node<T> node)
		{
			return path.ContainsKey(node);
		}
		
		public void Clear()
		{
			path.Clear();
		}
		public void SetParent(Node<T> node, Node<T> parent)
		{
			path[node] = parent;
		}
		
		public void Remove(Node<T> node)
		{
			path.Remove(node);
		}
		
		public NodeList<T> Reconstruct()
		{
			// Reconstructed path
			NodeList<T> reconstructedPath = new NodeList<T>();
			
			// Add the goal node
			reconstructedPath.Add(goal);
			
			// Start the index at the end
			Node<T> current;
			path.TryGetValue(goal, out current);
			while(current != null)
			{
				// Add the current node
				reconstructedPath.Insert(0, current);
				
				// Search for the node's parent and set that as the new node
				path.TryGetValue(current, out current);
			}
			return reconstructedPath;
		}
	}
}