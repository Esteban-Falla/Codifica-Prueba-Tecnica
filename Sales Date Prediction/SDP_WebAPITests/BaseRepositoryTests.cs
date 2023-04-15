using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SDP_WebAPI.Interfaces;
using SDP_WebAPI.Repositories;

namespace SDP_WebAPITests;

[TestFixture]
public class BaseRepositoryTests
{
    private BaseDirectoryImpl testBDI;
    private IConfiguration config;
    private string testConnStr;
    private Mock<ILogger> loggerMock;

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
        loggerMock = new Mock<ILogger>();

        testBDI = new BaseDirectoryImpl(config, loggerMock.Object);
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
        var sut = new BaseDirectoryImpl(config, loggerMock.Object);

        Assert.Multiple(() =>
        {
            Assert.IsNotNull(sut);
            StringAssert.AreEqualIgnoringCase(testConnStr, sut.ConnStr);
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
    public void TestValidateParams(bool expResult, object[] args)
    {
        var expectedResult = expResult;

        testBDI.Validate(args);
    }

    private static object[] _validateParamsTestCases = new object[]
    {
        new object[]
        {
            false,
            new object[] { null, 1 },
        },
        new object[]
        {
            false,
            new object[] { 1, 5, null },
        },
        new object[]
        {
            true,
            new object[] { 1, 3, 5, 7 },
        },
    };

    private class BaseDirectoryImpl : BaseRepository<TestElement>
    {
        public string ConnStr { get; }

        public BaseDirectoryImpl(IConfiguration config, ILogger logger) : base(config, logger)
        {
            ConnStr = config.GetConnectionString("SalesDB");
        }

        public void Validate(params object[] args) => ValidateParams(args);

        public override Task<IEnumerable<TestElement>> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<TestElement> GetById(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<int> Add(TestElement element)
        {
            throw new NotImplementedException();
        }

        public override Task<TestElement> Update(TestElement element)
        {
            throw new NotImplementedException();
        }

        public override Task<int> Delete(TestElement element)
        {
            throw new NotImplementedException();
        }
    }

    private class TestElement : IElement
    {
        public static T FromADOReader<T>(SqlDataReader reader)
            where T : IElement
        {
            throw new NotImplementedException();
        }
    }
}