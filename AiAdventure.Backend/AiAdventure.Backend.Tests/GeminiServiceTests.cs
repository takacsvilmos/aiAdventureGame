using System.Net;
using System.Text;
using System.Text.Json;
using AiAdventure.Backend.Exceptions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;

public class GeminiServiceTests
{
    [Fact]
    public async Task GenerateContentAsync_WhenApiIsSuccessful_ReturnsContent()
    {
        // Arrange
        var prompt = "hello world";
        var expectedResponseString = "This is the generated text.";

        // 1. Mock HttpMessageHandler
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedResponseString, Encoding.UTF8, "application/json")
            });

        // 2. Create HttpClient from the mock handler
        var httpClient = new HttpClient(mockHttpMessageHandler.Object);

        // 3. Mock IConfiguration
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(c => c["Gemini:ApiKey"]).Returns("fake-api-key");

        // 4. Create the service instance with the mocked dependencies
        var geminiService = new GeminiService(httpClient, mockConfiguration.Object);

        // Act
        var result = await geminiService.GenerateContentAsync(prompt);

        // Assert
        Assert.Equal(expectedResponseString, result);
    }

    [Fact]
    public async Task GenerateContentAsync_WhenApiIsNotSuccessful_ReturnsContent()
    {
        var prompt = "hello world";

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var mockConfiguration = new Mock<IConfiguration>();
        var geminiService = new GeminiService(httpClient, mockConfiguration.Object);
        var exception = await Assert.ThrowsAsync<GeminiApiException>(() => geminiService.GenerateContentAsync(prompt));
        
        Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
    }
}