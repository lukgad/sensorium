using System;
using Sensorium.Core;

namespace libSensorsPlugin
{
	enum NodeType { Chip, Feature, SubFeature }

	class LibSensorsTreeNode : TreeNode<IntPtr> {

		public readonly NodeType NodeType;

		public LibSensorsTreeNode(IntPtr contents, NodeType type) {
			Contents = contents;
			NodeType = type;
		}

		public override string ToString() {
			return string.Format("{0}: {1}", NodeType, base.ToString());
		}
	}
}
