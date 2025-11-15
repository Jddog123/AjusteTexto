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

    private static string Wrap(string text, int col)
    {
        if (string.IsNullOrEmpty(text))
            return "";
        
        throw new Exception();
    }
}