using System.Globalization;
using Core.Extensions;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using OpenQA.Selenium;

namespace Core.Tests;

public class WebElementExtensionsTests
{
    private readonly IWebElement _webElement = Substitute.For<IWebElement>();
        
    [Fact]
    public void FindElementIfExists_ShouldReturnWebElement_WhenElementExists()
    {
        _webElement.FindElement(null).Returns(new WebElement(null, "id"));

        var actualResult = _webElement.FindElementIfExists(null);

        actualResult.Should().NotBeNull();
        actualResult.Should().BeOfType(typeof(WebElement));
    }
        
    [Fact]
    public void FindElementIfExists_ShouldReturnNull_WhenElementDoesNotExist()
    {
        _webElement.FindElement(null).Throws(new NoSuchElementException());

        var actualResult = _webElement.FindElementIfExists(null);

        actualResult.Should().BeNull();
    }
        
    [Fact]
    public void GetTextAsInt_ShouldReturnMinusOne_WhenWebElementIsNull()
    {
        IWebElement? actualResult = null;
        actualResult.GetTextAsInt().Should().Be(-1);
    }
        
    [Fact]
    public void GetTextAsInt_ShouldReturnMinusOne_WhenTextFormatIsIncorrect()
    {
        _webElement.Text.Returns("abc");
        _webElement.GetTextAsInt().Should().Be(-1);
    }
        
    [Fact]
    public void GetTextAsInt_ShouldReturnInteger_WhenInputIsValid()
    {
        const int expectedInteger = 523;
            
        _webElement.Text.Returns(expectedInteger.ToString());
        _webElement.GetTextAsInt().Should().Be(expectedInteger);
    }

    [Fact]
    public void GetTextAsDouble_ShouldReturnMinusOne_WhenWebElementIsNull()
    {
        IWebElement? actualResult = null;
        actualResult.GetTextAsDouble().Should().Be(-1);
    }

    [Fact]
    public void GetTextAsDouble_ShouldReturnMinusOne_WhenTextFormatIsIncorrect()
    {
        _webElement.Text.Returns("abc");
        _webElement.GetTextAsDouble().Should().Be(-1);
    }

    [Fact]
    public void GetTextAsDouble_ShouldReturnInteger_WhenInputIsValid()
    {
        const double expectedInteger = 523.55;

        _webElement.Text.Returns(expectedInteger.ToString(CultureInfo.InvariantCulture));
        _webElement.GetTextAsDouble().Should().Be(expectedInteger);
    }
}