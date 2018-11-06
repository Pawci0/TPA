using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ViewModel.MetadataViews.KeywordUtils;
using Reflection.Enums;

namespace ViewModel.MetadataViews.Tests
{
    [TestClass()]
    public class KeywordUtilsTests
    {
        [TestMethod()]
        public void AbstractToStringTest()
        {
            Assert.AreEqual("abstract", AbstractToString(AbstractEnum.Abstract));
            Assert.AreEqual("", AbstractToString(AbstractEnum.NotAbstract));
        }

        [TestMethod()]
        public void StaticToStringTest()
        {
            Assert.AreEqual("static", StaticToString(StaticEnum.Static));
            Assert.AreEqual("", StaticToString(StaticEnum.NotStatic));
        }

        [TestMethod()]
        public void SealedToStringTest()
        {
            Assert.AreEqual("sealed", SealedToString(SealedEnum.Sealed));
            Assert.AreEqual("", SealedToString(SealedEnum.NotSealed));
        }

        [TestMethod()]
        public void VirtualToStringTest()
        {
            Assert.AreEqual("virtual", VirtualToString(VirtualEnum.Virtual));
            Assert.AreEqual("", VirtualToString(VirtualEnum.NotVirtual));
        }

        [TestMethod()]
        public void AccessLevelToStringTest()
        {
            Assert.AreEqual("private", AccessLevelToString(AccessLevelEnum.IsPrivate));
            Assert.AreEqual("protected", AccessLevelToString(AccessLevelEnum.IsProtected));
            Assert.AreEqual("internal", AccessLevelToString(AccessLevelEnum.IsProtectedInternal));
            Assert.AreEqual("public", AccessLevelToString(AccessLevelEnum.IsPublic));
        }
    }
}