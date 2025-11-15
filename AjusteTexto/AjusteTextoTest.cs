using FluentAssertions;

namespace AjusteTexto;

public class AjusteTextoTest
{
    [Fact]
    public void a()
    {
        var result = Wrap("", 1);

        result.Should().Be("");
    }
    
    [Fact]
    public void b()
    {
        var result = Wrap("this", 10);

        result.Should().Be("this");
    } 

    private static string Wrap(string text, int col)
    {
        if (string.IsNullOrEmpty(text))
            return "";
        
        throw new Exception();
    }
}