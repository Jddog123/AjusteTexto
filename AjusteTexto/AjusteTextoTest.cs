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
    
    [Fact]
    public void c()
    {
        var result = Wrap("word", 2);

        result.Should().Be("wo\nrd");
    } 
    
    [Fact]
    public void d()
    {
        var result = Wrap("abcdefghij", 3);

        result.Should().Be("abc\ndef\nghi\nj");
    }

    private static string Wrap(string text, int col)
    {
        return string.IsNullOrEmpty(text) ? "" : DividirPalabra(text,col);
    }
    
    private static string DividirPalabra(string texto, int cantidadMaximaCaracteres)
    {
        var palabras = new List<string>();
        for (int posicionCaracter = 0; posicionCaracter < texto.Length; posicionCaracter += cantidadMaximaCaracteres)
        {
            int cantidadCaracteresRestantes = texto.Length - posicionCaracter;
            int cantidadCaracteresDividir = Math.Min(cantidadMaximaCaracteres, cantidadCaracteresRestantes);
            
            palabras.Add(texto.Substring(posicionCaracter, cantidadCaracteresDividir));
        }
        
        return string.Join("\n", palabras);
    }
}