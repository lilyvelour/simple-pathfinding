/// Simple A* Algorithm by Josh Montoute
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

namespace ThinksquirrelSoftware.AStar
{
	public abstract class Solver<TValue>
		where TValue : struct
	{
		// Returns a list of nodes, representing a path from beginning to end.
		// Returns null if no solution found.
		// TODO: Use builtin arrays? Will increase speed, and sacrifice readability
		public NodeList<TValue> Solve(
			Node<TValue> start,
			Node<TValue> goal)
		{
			
			NodeList<TValue> closedSet = new NodeList<TValue>();
			
			NodeList<TValue> openSet = new NodeList<TValue>();
			openSet.Add(start);
			
			NodeList<TValue> traversedSet = new NodeList<TValue>();
			
			Node<TValue> lastNode = start;
			
			float g = 0;
			float h = Heuristic(start, goal);
			float f = g + h;
			
			while (openSet.Count > 0)
			{	
				// Grab the lowest cost node in the open set
				Node<TValue> currentNode = openSet[0];
				
				if (openSet.Count > 1)
				{
					for (int i = 0; i < openSet.Count; i++)
					{
						if (g + Heuristic(openSet[i], goal) < g + Heuristic(lastNode, goal))
						{
							currentNode = openSet[i];
						}
					}
				}
				
				// Move the current node to the closed set
				openSet.Remove(currentNode);
				closedSet.Add(currentNode);
				
				// If the current node is the goal, finish early
				if (currentNode == goal)
				{
					traversedSet.Add(currentNode);
					return traversedSet;
				}
				
				// True if a neighbor is opened
				bool good = false;
				
				// Open all of the neighbors of the current node
				for(int i = 0; i < currentNode.Neighbors.Count; i++)
				{
					// Skip this node if it's already been visited
					if (closedSet.Contains(currentNode.Neighbors[i]))
						continue;
						
					float g_Cost = g + currentNode.Costs[i];
					bool goodEstimate = false;
					
					if (g_Cost <= f)
					{
						goodEstimate = true;
						good = true;
					}
					else
					{
						goodEstimate = false;
						
						if (traversedSet.Contains(currentNode.Neighbors[i]))
							traversedSet.Remove(currentNode.Neighbors[i]);
					}
					
					// Add the neighbor to the open set
					if (!openSet.Contains(currentNode.Neighbors[i]))
					{
						openSet.Add(currentNode.Neighbors[i]);
					}
					
					if (goodEstimate)
					{		
						lastNode = currentNode;
						g = g_Cost;
						h = Heuristic(currentNode.Neighbors[i], goal);
						f = g + h;
					}
				}
				
				// If no good estimates, remove the node from the traversed set ('dead ended')
				if (good)
				{
					// Add the current node to the traversed set
					if (!traversedSet.Contains(currentNode))
						traversedSet.Add(currentNode);
				}
				else
				{
					if (traversedSet.Contains(currentNode))
						traversedSet.Remove(currentNode);
				}
			}
			
			return null;
		}
		
		protected abstract float Heuristic(Node<TValue> start, Node<TValue> goal);
	}
	
	public class Vector2Solver : Solver<Vector2>
	{
		protected override float Heuristic(Node<Vector2> start, Node<Vector2> goal)
		{
			return Vector2.Distance(start.Value, goal.Value);
		}
	}
	
	public class Vector3Solver : Solver<Vector3>
	{
		protected override float Heuristic(Node<Vector3> start, Node<Vector3> goal)
		{
			return Vector3.Distance(start.Value, goal.Value);
		}
	}
}