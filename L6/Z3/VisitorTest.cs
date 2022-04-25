using Microsoft.VisualStudio.TestTools.UnitTesting;
using Visitor;

namespace VisitorTest
{
    [TestClass]
    public class VisitorTest
    {
        [TestMethod]
        public void Depth_should_be_1()
        {
            Tree tree = new TreeLeaf();
            DepthTreeVisitor visitor = new();
            tree.Accept(visitor);
            Assert.AreEqual(1, visitor.Depth);
        }

        [TestMethod]
        public void Depth_should_be_2()
        {
            Tree tree = new TreeNode
            {
                Left = new TreeLeaf()
            };

            DepthTreeVisitor visitor = new();
            tree.Accept(visitor);
            Assert.AreEqual(2, visitor.Depth);
        }

        [TestMethod]
        public void Depth_should_be_3()
        {
            Tree tree = new TreeNode
            {
                Left = new TreeLeaf(),
                Right = new TreeNode
                {
                    Left = new TreeLeaf(),
                    Right = new TreeLeaf()
                }
            };

            DepthTreeVisitor visitor = new();
            tree.Accept(visitor);
            Assert.AreEqual(3, visitor.Depth);
        }
    }
}