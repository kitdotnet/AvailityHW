using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LispParenCheck.Tests
{
    [TestClass()]
    public class ParenCheckerTests
    {
        [TestMethod()]
        public void SimpleMatch_HasValidParens()
        {
            RunTest("()");
        }

        [TestMethod()]
        public void MultipleSets_HasValidParens()
        {
            RunTest("()()");
        }

        [TestMethod()]
        public void WhiteSpace_HasValidParens()
        {
            RunTest(" () ");
        }

        [TestMethod()]
        public void Nested_HasValidParens()
        {
            RunTest("( nested ( parens ) )");
        }

        [TestMethod()]
        public void Mismatch_HasInvalidParens()
        {
            RunTest("( mismatch (", expectingValid: false);
        }

        [TestMethod()]
        public void Mismatch2_HasInvalidParens()
        {
            RunTest("( mismatch2 ) (", expectingValid: false);
        }

        [TestMethod()]
        public void NoParens_HasInvalidParens()
        {
            RunTest("no parens", expectingValid: false);
        }

        void RunTest(string testString, bool expectingValid = true)
        {
            var parenChecker = new ParenChecker();

            var result = parenChecker.HasValidParens(testString);

            Assert.IsTrue(result == expectingValid);
        }
    }
}