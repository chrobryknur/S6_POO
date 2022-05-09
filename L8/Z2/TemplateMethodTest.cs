using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateMethod;

namespace TemplateMethodTest
{
    [TestClass]
    public class TemplateMethodTest
    {
        [TestMethod]
        public void age_sum_should_be_6()
        {
            ColumnSummator columnSummator = new();
            columnSummator.Execute();
            Assert.AreEqual(6, columnSummator.result);
        }

        [TestMethod]
        public void longest_node_length_should_be_7()
        {
            LongestNodeLengthGetter lengthGetter = new();
            lengthGetter.Execute();
            Assert.AreEqual(7, lengthGetter.result);
        }
    }
}