/// A* Pathfinding - Heuristic base class
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

namespace ThinksquirrelSoftware.AStar
{
	public abstract class Heuristic<T>
	{
		private float mScale = 1;
		private float mAlpha = 1;
		private float mMod = 0;
		
		public float Scale
		{
			get
			{
				return mScale;
			}
			set
			{
				mScale = value;
			}
		}
		
		public float Alpha
		{
			get
			{
				return mAlpha;
			}
			set
			{
				mAlpha = value;
			}
		}
		
		public float Mod
		{
			get
			{
				return mMod;
			}
			set
			{
				mMod = value;
			}
		}
		
		public Heuristic() {}
		
		public Heuristic(float scale, float alpha, float mod)
		{
			mScale = scale;
			mAlpha = alpha;
			mMod = mod;
		}
		
		public abstract float Run(Node<T> node, Node<T> goal);
	}
}
