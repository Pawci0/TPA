using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Database.Model;
using System.Data.Entity;
using DTGBase.Enums;

namespace Database.Tests
{
    [TestClass()]
    public class DBSerializerTests
    {
        Mock<DatabaseContext> mockContext;
        DBSerializer serializer;
        
        List<DatabaseNamespace> namespaces;
        List<DatabaseType> types;

        [TestInitialize]
        public void Init()
        {
            types = new List<DatabaseType>
            {
                new DatabaseType{ Name = "ServiceA", Type = TypeKindEnum.ClassType},
                new DatabaseType{ Name = "ServiceB", Type = TypeKindEnum.ClassType},
                new DatabaseType{ Name = "ServiceC", Type = TypeKindEnum.ClassType}
            };

            namespaces = new List<DatabaseNamespace>
            {
                new DatabaseNamespace{ Name = "TPA.ApplicationArchitecture.BusinessLogic", Types = types},
                new DatabaseNamespace{ Name = "TPA.ApplicationArchitecture.Data"},
                new DatabaseNamespace{ Name = "TPA.ApplicationArchitecture.Presentation"}
            };

            var data = new List<DatabaseAssembly>
            {
                new DatabaseAssembly { Name = "ExampleDLL.dll", Namespaces = namespaces }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<DatabaseAssembly>>();
            mockSet.As<IQueryable<DatabaseAssembly>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<DatabaseAssembly>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<DatabaseAssembly>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<DatabaseAssembly>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(x => x.AssemblyModel).Returns(mockSet.Object);

            serializer = new DBSerializer();
        }

        [TestMethod]
        public void AssemblyNameTests()
        {
            DatabaseAssembly baseAssembly = serializer.Load(mockContext.Object);

            Assert.AreEqual("ExampleDLL.dll", baseAssembly.Name);
        }

        [TestMethod]
        public void NamespacesTests()
        {
            DatabaseAssembly baseAssembly = serializer.Load(mockContext.Object);
            
            CollectionAssert.AreEquivalent(baseAssembly.Namespaces.ToList(), namespaces);
        }

        [TestMethod]
        public void TypesTests()
        {
            DatabaseAssembly baseAssembly = serializer.Load(mockContext.Object);

            Assert.AreEqual(3, baseAssembly.Namespaces.ToList()[0].Types.Count());
            CollectionAssert.AreEquivalent(baseAssembly.Namespaces.ToList()[0].Types.ToList(), types);
        }
    }
}
