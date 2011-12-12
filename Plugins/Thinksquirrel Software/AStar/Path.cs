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
		private NodeList<T> mNodes = new NodeList<T>();
		private NodeList<T> mParents = new NodeList<T>();
		private Node<T> goal = new Node<T>();
		
		public Path(Node<T> goal)
		{
			this.goal = goal;
		}
		
		public void Add(Node<T> node, Node<T> parent)
		{
			mNodes.Add(node);
			mParents.Add(parent);
		}
		
		public bool Contains(Node<T> node)
		{
			return mNodes.Contains(node);
		}
		
		public void SetParent(Node<T> node, Node<T> parent)
		{
			mParents[mNodes.IndexOf(node)] = parent;
		}
		
		public void Remove(Node<T> node)
		{
			int i = mNodes.IndexOf(node);
			mNodes.RemoveAt(i);
			mParents.RemoveAt(i);
		}
		
		public NodeList<T> Reconstruct()
		{
			NodeList<T> reconstructedPath = new NodeList<T>();
			
			// Start the index at the end
			int i = mNodes.IndexOf(goal);
			while(i != -1)
			{
				// Add the node at the index
				reconstructedPath.Insert(0, mNodes[i]);
				
				// Break if no parent (start node)
				if (mParents[i] == null)
					break;
				
				// Search for the node's parent and set that as the new index
				i = mNodes.IndexOf(mParents[i]);
			}
			
			return reconstructedPath;
		}
	}
}