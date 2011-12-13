/// A* Pathfinding - Unity solvers
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
using ThinksquirrelSoftware.AStar;

namespace ThinksquirrelSoftware.AStar.Unity
{
	public class CrossProductSolverV3 : Solver<Vector3>
	{
		public CrossProductSolverV3(Heuristic<Vector3> heuristic) : base(heuristic)
		{
			comparer = new UnityV3Comparer();
		}
		
		private float CalculateMod(Node<Vector3> start, Node<Vector3> current, Node<Vector3> goal)
		{
			if (current == null)
				return 0;
			
			float c1 = Vector3.Cross(start.Value, goal.Value).magnitude;
			float c2 = Vector3.Cross(current.Value, goal.Value).magnitude;

			float cross = Mathf.Abs(c1 - c2);
			return cross*0001f;
		}
		
		protected override float DoHeuristic(Node<Vector3> node, Node<Vector3> goal)
		{
			return HeuristicFunction.Run(node, goal) + CalculateMod(node, currentNode, goal);
		}
	}
}
