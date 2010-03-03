using System.Collections.Generic;

namespace Sensorium.Common {
	public class TreeNode<T> {
		public T Contents;
		public TreeNode<T> Parent { get; private set; }
		
		protected readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();
		public IEnumerable<TreeNode<T>> Children {get {
			return _children.ToArray();
		}}

		public TreeNode(T contents) {
			Contents = contents;
		}

		public TreeNode() {}

		public void Add(TreeNode<T> node) {
			_children.Add(node);
			node.Parent = this;
		}

		public override string ToString() {
			return string.Format("{0} - {1} children", Contents, _children.Count);
		}

		public void Traverse(IVisitor v) {
			v.Visit(this);
			foreach(TreeNode<T> t in _children)
				t.Traverse(v);
		}
	}
}
