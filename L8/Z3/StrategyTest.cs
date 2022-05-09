using Microsoft.VisualStudio.TestTools.UnitTesting;
using Strategy;

namespace StrategyTest
{
    [TestClass]
    public class StrategyTest
    {
        [TestMethod]
        public void age_sum_should_be_6()
        {
            ColumnSummatorStrategy columnSummator = new();
            DataAccessHandlerContext context = new(columnSummator);
            int result = context.Execute();
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void longest_node_length_should_be_7()
        {
            LongestNodeLengthGetterStrategy lengthGetter = new();
            DataAccessHandlerContext context = new(lengthGetter);
            int result = context.Execute();
            Assert.AreEqual(7, result);
        }
    }
}