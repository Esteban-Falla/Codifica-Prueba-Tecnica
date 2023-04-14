using System.Text;
using Microsoft.Extensions.Configuration;
using SDP_WebAPI.Interfaces;

namespace SDP_WebAPITests;

using SDP_WebAPI.Models;

[TestFixture]
public class BaseRepositoryTests
{
    private BaseDirectoryImpl testBDI;
    private IConfiguration config;
    private string testConnStr;

    [SetUp]
    public void Setup()
    {
        testConnStr = "Some connection string";

        var appSettings = $@"{{
            ""ConnectionStrings"":{{
                ""SalesDB"":""{testConnStr}""
                }}
            }}";

        var builder = new ConfigurationBuilder();
        builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
        config = builder.Build();
        
        testBDI = new BaseDirectoryImpl(config);
    }

    [TearDown]
    public void TearDown()
    {
        testBDI = null;
        config = null;
        testConnStr = null;
    }

    [Test]
    public void TestBaseRepositoryConstructor()
    {
        var sut = new BaseDirectoryImpl(config);

        Assert.Multiple(() =>
        {
            Assert.IsNotNull(sut);
            StringAssert.AreEqualIgnoringCase(testConnStr,sut.ConnStr);
        });
    }

    [Test]
    public void TestGetAllNotImplemented()
    {
        Assert.ThrowsAsync<NotImplementedException>(() => testBDI.GetAll());
    }

    [Test]
    public void TestGetByIdNotImplemented()
    {
        int id = 1;

        Assert.ThrowsAsync<NotImplementedException>(() => testBDI.GetById(id));
    }

    [Test]
    public void TestAddNotImplemented()
    {
        var testElement = new TestElement();

        Assert.ThrowsAsync<NotImplementedException>(() => testBDI.Add(testElement));
    }

    [Test]
    public void TestUpdateNotImplemented()
    {
        var testElement = new TestElement();

        Assert.ThrowsAsync<NotImplementedException>(() => testBDI.Update(testElement));
    }

    [Test]
    public void TestDeleteNotImplemented()
    {
        var testElement = new TestElement();

        Assert.ThrowsAsync<NotImplementedException>(() => testBDI.Delete(testElement));
    }

    [Test]
    [TestCaseSource(nameof(_validateParamsTestCases))]
    public void TestValidateParams(bool expResult,object[] args)
    {
        var expectedResult = expResult;

        var result = testBDI.Validate(args);
        
        Assert.That(result, Is.EqualTo(expectedResult));
    }
    
    private static object[] _validateParamsTestCases = new object[]
    {
        new object[]{
            false,
            new object[] {null,1},
        },
        new object[]{
            false,
            new object[] {1,5,null},
        },
        new object[]{
            true,
            new object[] {1,3,5,7},
        },
    };

    private class BaseDirectoryImpl : BaseRepository<TestElement>
    {
        public string ConnStr { get; }

        public BaseDirectoryImpl(IConfiguration config) : base(config)
        {
            ConnStr = config.GetConnectionString("SalesDB");
        }

        public bool Validate(params object[] args) => ValidateParams(args);
    }

    private class TestElement : IElement
    {
    }
}