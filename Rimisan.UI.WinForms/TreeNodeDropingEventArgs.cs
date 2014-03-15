using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rimisan.UI.WinForms
{
    public class TreeNodeDropingEventArgs: EventArgs
    {
        public TreeNode DropingNode { get; set; }
        public TreeNode AsChildOf { get; set; }
        public int AtIndex { get; set; }
        public bool Cancel { get; set; }
    }
}
