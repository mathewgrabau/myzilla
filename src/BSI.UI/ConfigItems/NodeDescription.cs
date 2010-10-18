using System;
using System.Collections.Generic;
using System.Text;

namespace MyZilla.UI.ConfigItems
{
    public class NodeDescription
    {

        private NodeType _nodeType;

        public NodeType TreeNodeType
        {
            get { return _nodeType; }
            set { _nodeType = value; }
        }

        private Object _queryRow;

        public Object NodeData
        {
            get { return _queryRow; }
            set { _queryRow = value; }
        }

        private string _format; 

        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }

        public NodeDescription(NodeType nodeType, Object row)
        {
            _nodeType = nodeType;
            _queryRow = row;
        }
	
        public NodeDescription(NodeType nodeType, Object row, string format) {
            _nodeType = nodeType;
            _queryRow = row;
            _format = format;
        }
    }
}
