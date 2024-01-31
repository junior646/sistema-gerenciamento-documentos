using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

public partial class ControllerTesting<TController> where TController : Controller
{
    public readonly Mock<ISender> Sender = new();
    public readonly TController Controller;
    public readonly string IpUnitTest = "https://localhost:5001";

    public ControllerTesting()
    {
        Controller = (TController)Activator.CreateInstance(typeof(TController), args: new object[] { Sender.Object })!;
    }

    [SetUp]
    public void Setup()
    {
        var httpContext = new DefaultHttpContext()
        {
            Connection = { RemoteIpAddress = new System.Net.IPAddress(16885952) }
        };

        var controllerContext = new ControllerContext() { HttpContext = httpContext };

        Controller.ControllerContext = controllerContext;
    }
}
