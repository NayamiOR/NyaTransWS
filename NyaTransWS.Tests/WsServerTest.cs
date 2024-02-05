using System;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Xunit;

namespace NyaTransWS.Tests;

[TestSubject(typeof(WsServer))]
public class WsServerTests
{
    [Fact]
    public async Task TestReceiveMessage()
    {
        // Arrange
        var server = new WsServer();
        var pack = new Pack("Hello, world!");
        var json = Pack.PtoJ(pack);
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        await server.ReceiveMessage(json);

        // Assert
        // 在这里添加你的断言，验证ReceiveMessage方法的行为
        var output = consoleOutput.ToString().Trim();
        Assert.Equal("Hello, world!", output);
    }
}