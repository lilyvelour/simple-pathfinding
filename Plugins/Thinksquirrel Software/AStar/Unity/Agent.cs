/// A* Pathfinding - Unity Agent (MonoBehaviour)
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
	public abstract class Agent<T> : MonoBehaviour
	{
		protected Graph<T> nodeGraph;
		protected Solver<T> solver;
		protected Node<T> currentNode;
		protected Node<T> goal;
		protected Path<T> path;
		protected NodeList<T> pathNodes;
		
		public Node<T> CurrentNode
		{
			get
			{
				return currentNode;
			}
		}
		protected void Initialize()
		{
			nodeGraph.GraphChange += new Graph<T>.GraphChangeHandler(OnGraphChange);
		}
		
		void OnDisable()
		{
			if (nodeGraph != null)
			{
				nodeGraph.GraphChange -= new Graph<T>.GraphChangeHandler(OnGraphChange);
			}			
		}
		
		protected abstract void OnGraphChange();
	}
}
