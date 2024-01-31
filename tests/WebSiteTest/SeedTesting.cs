using Moq;
using MediatR;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

namespace UnitTest;
public partial class SeedTesting
{
    public readonly Mock<ISender> Mediator = new();

    public static object GetInstanceOf(Type type)
    {
        return type.GetConstructor(Type.EmptyTypes) is not null
            ? Activator.CreateInstance(type)!
            : FormatterServices.GetUninitializedObject(type);
    }

    public static IFormFile MockIFormFile(string content = "Hello World from a Fake File", string fileName = "test.pdf")
    {
        //Setup mock file using a memory stream
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        //create FormFile with desired data
        IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

        return file;
    }
}