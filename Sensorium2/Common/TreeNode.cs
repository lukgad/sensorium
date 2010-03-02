using System.Collections.Generic;

namespace Sensorium.Common {
	public class TreeNode<T> {
		public T Contents;
		public TreeNode<T> Parent { get; private set; }
		
		private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();
		public IEnumerable<TreeNode<T>> Children {get {
			return _children.ToArray();
		}}

		public TreeNode(T contents) {
			Contents = contents;
		}

		public TreeNode() {}

		public void AddChild(TreeNode<T> node) {
			_children.Add(node);
			node.Parent = this;
		}
	}
}
