namespace Rimisan.UI.WinForms.Test
{
    partial class FormTreeViewDragNDropHelperTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Filho A");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Filho B");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Root 001", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Filho C");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Filho D");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Filho E");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Root 002", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(13, 29);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node2";
            treeNode1.Text = "Filho A";
            treeNode2.Name = "Node3";
            treeNode2.Text = "Filho B";
            treeNode3.Name = "Root001";
            treeNode3.Text = "Root 001";
            treeNode4.Name = "Node4";
            treeNode4.Text = "Filho C";
            treeNode5.Name = "Node5";
            treeNode5.Text = "Filho D";
            treeNode6.Name = "Node6";
            treeNode6.Text = "Filho E";
            treeNode7.Name = "Root002";
            treeNode7.Text = "Root 002";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode7});
            this.treeView1.Size = new System.Drawing.Size(554, 321);
            this.treeView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Drag and drop the nodes up and down.";
            // 
            // FormTreeViewDragNDropHelperTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 362);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Name = "FormTreeViewDragNDropHelperTest";
            this.Text = "FormTreeViewDragNDropHelperTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
    }
}