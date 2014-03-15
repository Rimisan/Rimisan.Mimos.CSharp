using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rimisan.UI.WinForms.Test
{
    [TestClass]
    public class TreeViewDragNDropHelperTest
    {
        [TestMethod]
        public void Test_TreeViewDragNDropHelper()
        {
            var frm =
                new FormTreeViewDragNDropHelperTest();
            var trvHelps = new TreeViewDragNDropHelper(frm.treeView1);
            frm.ShowDialog();
            
        }
    }
}
