using System;

namespace Visitor
{
    public abstract class Tree
    {
        public virtual void Accept(TreeVisitor visitor)
        {
        }
    }

    public class TreeNode : Tree
    {
        public Tree Left { get; set; }
        public Tree Right { get; set; }

        public override void Accept(TreeVisitor visitor)
        {
            visitor.VisitNode(this);

            visitor.DoEntryActions();

            if (Left != null)
                Left.Accept(visitor);
            if (Right != null)
                Right.Accept(visitor);

            visitor.DoExitActions();
        }
    }
    public class TreeLeaf : Tree
    {
        public override void Accept(TreeVisitor visitor)
        {
            visitor.VisitLeaf(this);
        }
    }

    public abstract class TreeVisitor
    {
        public abstract void VisitLeaf(TreeLeaf leaf);
        public abstract void VisitNode(TreeNode node);

        public abstract void DoEntryActions();
        public abstract void DoExitActions();
    }

    public class DepthTreeVisitor : TreeVisitor
    {
        public int Depth { get; private set; }
        public override void VisitLeaf(TreeLeaf leaf)
        {
            if(currentDepth > Depth)
            {
                Depth = currentDepth;
            }
        }

        public override void VisitNode(TreeNode node)
        {
        }

        public override void DoEntryActions()
        {
            currentDepth++;
        }

        public override void DoExitActions()
        {
            currentDepth--;
        }

        private int currentDepth = 1;
    }
}
