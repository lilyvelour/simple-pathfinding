/// A* Pathfinding - Solver base class
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
using System.Collections.Generic;

namespace ThinksquirrelSoftware.AStar
{
	public class Solver<T>
	{
		// The heuristic function to use.
		private Heuristic<T> mHeuristic;
		
		public Heuristic<T> HeuristicFunction
		{
			get
			{
				return mHeuristic;
			}
			set
			{
				mHeuristic = value;
			}
		}
		
		public Solver(Heuristic<T> heuristic)
		{
			this.mHeuristic = heuristic;
		}
		
		// Returns a list of nodes, representing a path from beginning to end.
		// Returns null if no solution found.
		public Path<T> Solve(
			Node<T> start,
			Node<T> goal)
		{
			
			float g = 0;
			float h = HeuristicFunction.Run(start, goal);
			float f = g + h;
			
			BinaryHeap<Node<T>> openSet = new BinaryHeap<Node<T>>();
			NodeList<T> closedSet = new NodeList<T>();
			Path<T> path = new Path<T>(goal);
			
			openSet.Insert(f, start);
			
			while (openSet.Count > 0)
			{	
				// If the current node is the goal, finish early
				if (openSet.Peek() == goal)
				{
					return path;
				}
				
				// Grab the lowest cost node in the open set
				Node<T> currentNode = openSet.RemoveRoot();
				
				// Move the current node to the closed set
				closedSet.Add(currentNode);
								
				// Open all of the neighbors of the current node
				for(int i = 0; i < currentNode.Neighbors.Count; i++)
				{
					// Neighbor node
					Node<T> neighbor = currentNode.Neighbors[i];
					// Movement cost from currentNode
					float movementCost = currentNode.Costs[i];
					
					// The cost to traverse to the current neighbor from the start
					float g_Neighbor = g + movementCost;
					float h_Neighbor = HeuristicFunction.Run(neighbor, goal);
					float f_Neighbor = g_Neighbor + h_Neighbor;
					
					// If neighbor is already scheduled to be opened
					if (openSet.Contains(neighbor))
					{
							continue;
					}
					
					// If this node has already been visited
					if (closedSet.Contains(neighbor))
						continue;
					
					// Add the neighbor to the open set (sorted)
					if (!openSet.Contains(neighbor))
					{
						openSet.Insert(f_Neighbor, neighbor);
					}
					
					if (!path.Contains(neighbor))
					{
						path.Add(neighbor, currentNode);
					}
					else
					{
						path.SetParent(neighbor, currentNode);
					}
					g = g_Neighbor;
					h = h_Neighbor;
					f = f_Neighbor;
				}
			}
			
			return null;
		}
	}
}