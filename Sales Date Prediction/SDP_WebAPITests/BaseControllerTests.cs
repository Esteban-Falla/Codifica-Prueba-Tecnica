using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SDP_WebAPI.Controllers;
using SDP_WebAPI.Interfaces;

namespace SDP_WebAPITests;

[TestFixture]
public class BaseControllerTests
{
    private Mock<ILogger> loggerMock;
    private Mock<IRepository<ElementImpl>> repositoryMock;
    private BaseCtrlImpl controller;

    [SetUp]
    public void SetUp()
    {
        loggerMock = new Mock<ILogger>();
        repositoryMock = new Mock<IRepository<ElementImpl>>();
    }

    [TearDown]
    public void TearDown()
    {
        loggerMock = null;
        repositoryMock = null;
        controller = null;
    }

    [Test]
    public void TestConstructorNullLogger()
    {
        ILogger logger = null;

        Assert.Throws<ArgumentNullException>(() => new BaseCtrlImpl(logger, repositoryMock.Object));
    }

    [Test]
    public void TestConstructorNullRepository()
    {
        IRepository<ElementImpl> repo = null;

        Assert.Throws<ArgumentNullException>(() => new BaseCtrlImpl(loggerMock.Object, repo));
    }

    [Test]
    public void TestConstructor()
    {
        Assert.DoesNotThrow(() => new BaseCtrlImpl(loggerMock.Object, repositoryMock.Object));
    }

    [Test]
    [TestCaseSource(nameof(_validateInputsTestCases))]
    public void TestValidateInputs(bool throwsException, object[] args)
    {
        controller = new BaseCtrlImpl(loggerMock.Object, repositoryMock.Object);

        if (throwsException)
        {
            Assert.Throws<ArgumentNullException>(() => controller.Validate(args));
            return;
        }

        Assert.DoesNotThrow(() => controller.Validate(args));
    }

    private static object[] _validateInputsTestCases = new object[]
    {
        new object[]
        {
            true,
            new object[] { null, 1 },
        },
        new object[]
        {
            true,
            new object[] { 1, 5, null },
        },
        new object[]
        {
            false,
            new object[] { 1, 3, 5, 7 },
        },
    };

    public class ElementImpl : IElement
    {
        public static T FromADOReader<T>(SqlDataReader reader) where T : IElement
        {
            throw new NotImplementedException();
        }
    }

    public class BaseCtrlImpl : BaseController<ElementImpl>
    {
        public BaseCtrlImpl(ILogger logger, IRepository<ElementImpl> repository)
            : base(logger, repository)
        {
        }

        public void Validate(params object[] Args)
        {
            ValidateInputs(Args);
        }
    }
}