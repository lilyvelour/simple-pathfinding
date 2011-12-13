/// A* Pathfinding - Unity heuristics
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
using System.Collections.Generic;
using ThinksquirrelSoftware.AStar;

namespace ThinksquirrelSoftware.AStar.Unity
{
	public class ManhattanHeuristicV2 : Heuristic<Vector2>
	{	
		public ManhattanHeuristicV2(float scale, float alpha, float mod) : base(scale, alpha, mod) {}
		
		public override float Run(Node<Vector2> node, Node<Vector2> goal)
		{
			// Manhattan distance (Vector2)
			return
				(Scale * Alpha * 
				(Mathf.Abs(node.Value.x-goal.Value.x) 
				+ Mathf.Abs(node.Value.y-goal.Value.y)))
				+ Mod;		
		}
	}
	
	public class ManhattanHeuristicV3 : Heuristic<Vector3>
	{
		public ManhattanHeuristicV3(float scale, float alpha, float mod) : base(scale, alpha, mod) {}
	
		public override float Run(Node<Vector3> node, Node<Vector3> goal)
		{
			// Manhattan distance (Vector3)
			return
				(Scale * Alpha *
				(Mathf.Abs(node.Value.x-goal.Value.x) 
				+ Mathf.Abs(node.Value.y-goal.Value.y)
				+ Mathf.Abs(node.Value.z-goal.Value.z))) + Mod;			
		}
	}
	
	public class ChebyshevHeuristicV2 : Heuristic<Vector2>
	{
		public ChebyshevHeuristicV2(float scale, float alpha, float mod) : base(scale, alpha, mod) {}
		
		public override float Run(Node<Vector2> node, Node<Vector2> goal)
		{
			// Chebyshev distance (Vector2)
			return
				(Scale * Alpha *
				Mathf.Max(Mathf.Abs(node.Value.x-goal.Value.x), Mathf.Abs(node.Value.y-goal.Value.y)))
				+ Mod;
		}
	}
	
	public class ChebyshevHeuristicV3 : Heuristic<Vector3>
	{
		public ChebyshevHeuristicV3(float scale, float alpha, float mod) : base(scale, alpha, mod) {}
		
		public override float Run(Node<Vector3> node, Node<Vector3> goal)
		{
			// Chebyshev distance (Vector3)
			return
				(Scale * Alpha *
				Mathf.Max(
				Mathf.Max(Mathf.Abs(node.Value.x-goal.Value.x), Mathf.Abs(node.Value.y-goal.Value.y)),
				Mathf.Abs(node.Value.y-goal.Value.z)))
				+ Mod;	
		}
	}
	
	public class EuclideanHeuristicV2 : Heuristic<Vector2>
	{
		public EuclideanHeuristicV2(float scale, float alpha, float mod) : base(scale, alpha, mod) {}	
		
		public override float Run(Node<Vector2> node, Node<Vector2> goal)
		{
			// Euclidean distance (Vector2)
			return
				(Scale * Alpha * 
				Vector2.Distance(node.Value, goal.Value))
				+ Mod;
		}
	}
	
	public class EuclideanHeuristicV3 : Heuristic<Vector3>
	{
		public EuclideanHeuristicV3(float scale, float alpha, float mod) : base(scale, alpha, mod) {}	
		
		public override float Run(Node<Vector3> node, Node<Vector3> goal)
		{
			// Euclidean distance (Vector2)
			return
				(Scale * Alpha * 
				Vector3.Distance(node.Value, goal.Value))
				+ Mod;
		}
	}
}
