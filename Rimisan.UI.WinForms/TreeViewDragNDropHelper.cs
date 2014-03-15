using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Rimisan.UI.WinForms
{
    /// <summary>
    /// Mimo cuja responsabilidade é fazer com que os nós de um 
    /// objeto TreeView possam ser manipulados através através de operações de
    /// arrastar e soltar. 
    /// Permite que o nó que está sendo arrastado se torne filho, pai ou irmão
    /// de qualquer nó, que não si mesmo, na árvore.
    /// O andamento das operações possui um bom retorno visual tornando claro para o 
    /// utilizador qual vai ser o resultado no momento após a execução do movimento soltar.
    /// Para cancelar a operação assine o evento WillDropTheNode e atribua true para o membro Cancel do parametro do tipo TreeNodeDropingEventArgs.
    /// Você pode utilizar os outros membros de TreeNodeDropingEventArgs para avaliar melhor a siatuação e decidir 
    /// se o evento deve realmente ser cancelado.
    /// A fazer: 
    ///     - Permitir a cópia do nó quando utilizado o modificador Ctrl durante a fase soltar da operação.
    /// 
    /// </summary>
    /// <remarks>
    /// Adaptei daqui: http://msdn.microsoft.com/pt-br/library/system.windows.forms.treeview.itemdrag(v=vs.110).aspx
    /// </remarks>
    public class TreeViewDragNDropHelper
    {
        public event EventHandler<TreeNodeDropingEventArgs> WillDropTheNode; 
        
        private TreeView tree;
        private Form form;
        public TreeViewDragNDropHelper(TreeView treeView)
        {
            this.tree = treeView;
            tree.AllowDrop = true;
            tree.ItemDrag += new ItemDragEventHandler(tree_ItemDrag);
            tree.DragEnter += new DragEventHandler(tree_DragEnter);
            tree.DragDrop += new DragEventHandler(tree_DragDrop);
            tree.DragOver += new DragEventHandler(tree_DragOver);
            form = tree.FindForm();
        }

        private TreeNode ultimoNo = null;
        private OperacaoEnum? ultimaOperacao = null;

        private enum OperacaoEnum
        {
            Filho,
            Irmao
        }

        // Determine whether one node is a parent 
        // or ancestor of a second node.
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            if (node2 == null) return false;
            // Check the parent node of the second node.
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node, 
            // call the ContainsNode method recursively using the parent of 
            // the second node.
            return ContainsNode(node1, node2.Parent);
        }

        void tree_DragOver(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the mouse position.
            Point targetPoint = tree.PointToClient(new Point(e.X, e.Y));
            var targetNode = tree.GetNodeAt(targetPoint);
            var draggedNode = ((TreeNode) e.Data.GetData(typeof (TreeNode)));
            if (ContainsNode(draggedNode, targetNode)) return;
            if (targetNode == null) return;
            if (draggedNode.Equals(targetNode)) return;
            OperacaoEnum operacao;

            decimal meio = targetNode.Bounds.Width/2M;
            if ((targetPoint.X - targetNode.Bounds.X) > meio )
            {
                operacao = OperacaoEnum.Filho;
            }
            else
            {
                operacao = OperacaoEnum.Irmao;
            }

            if (targetNode == ultimoNo && operacao == ultimaOperacao) return;
            if (targetNode == placeholderTreeNode) targetNode = placeholderTreeNode.NextNode;
            
            ultimoNo = targetNode;
            ultimaOperacao = operacao;

            // Select the node at the mouse position.
            //treeView1.SelectedNode = placeholderTreeNode;
            if (targetNode != null)
            {
                if(placeholderTreeNode.Parent!=null)
                    placeholderTreeNode.Parent.Nodes.Remove(placeholderTreeNode);
                else
                    tree.Nodes.Remove(placeholderTreeNode);

                placeholderTreeNode.Text = draggedNode.Text;


                if (targetNode.NextNode != placeholderTreeNode)
                {

                    TreeNodeCollection treeNodeCollection = null;

                    if(operacao == OperacaoEnum.Irmao)
                        treeNodeCollection = targetNode.Parent == null ? tree.Nodes : targetNode.Parent.Nodes;
                    else if (operacao == OperacaoEnum.Filho)
                        treeNodeCollection = targetNode.Nodes;
                    treeNodeCollection.Insert(
                        targetNode.Index,
                        placeholderTreeNode
                        );
                    targetNode.Expand();
                }
            }
        }

        TreeNode placeholderTreeNode = 
            new TreeNode("---------")
            {
                Name = "placeholderTreeNode",
                BackColor = Color.Black,
                ForeColor = Color.White
            };

        void tree_DragDrop(object sender, DragEventArgs e)
        {
             // Retrieve the client coordinates of the drop location.
            Point targetPoint = tree.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = tree.GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            
            // If it is a move operation, remove the node from its current 
            // location and add it to the node at the drop location.
            if (e.Effect == DragDropEffects.Move)
            {
                
                int index = placeholderTreeNode.Index;
                var tnCollection = 
                    placeholderTreeNode.Parent == null ? 
                        tree.Nodes : placeholderTreeNode.Parent.Nodes;

                bool cancel = false;
                if (WillDropTheNode != null)
                {
                    var evt = new TreeNodeDropingEventArgs()
                    {
                        DropingNode = draggedNode,
                        AsChildOf = placeholderTreeNode.Parent,
                        AtIndex = index
                    };
                    WillDropTheNode(tree, evt);
                    cancel = evt.Cancel;
                }
                tree.BeginUpdate();
                placeholderTreeNode.Remove();
                if (!cancel)
                {
                    draggedNode.Remove();
                    tnCollection.Insert(index, draggedNode);
                }
                tree.EndUpdate();
            }

            // If it is a copy operation, clone the dragged node 
            // and add it to the node at the drop location.
            else if (e.Effect == DragDropEffects.Copy)
            {
                //targetNode
                targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
            }

            // Expand the node at the location 
            // to show the dropped node.
            if(targetNode!=null)
                targetNode.Expand();
            
        }

        void tree_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        void tree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Move the dragged node when the left mouse button is used.
            if (e.Button == MouseButtons.Left)
            {
                form.DoDragDrop(e.Item, DragDropEffects.Move);
            }
            // Copy the dragged node when the right mouse button is used.
            else if (e.Button == MouseButtons.Right)
            {
                form.DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }
    }
}
