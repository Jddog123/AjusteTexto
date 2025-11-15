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
    
    [Fact]
    public void e()
    {
        var result = Wrap("word word", 3);

        result.Should().Be("wor\nd\nwor\nd");
    }

    [Fact]
    public void f()
    {
        var result = Wrap("word word", 6);

        result.Should().Be("word\nword");
    }   
    
    [Fact]
    public void f2()
    {
        var result = Wrap("word word", 5);

        result.Should().Be("word\nword");
    }
    
    [Fact]
    public void g()
    {
        var result = Wrap("word word word", 6);

        result.Should().Be("word\nword\nword");
    }
    
    private static string Wrap(string text, int col)
    {
        const string saltoLinea = "\n";
        
        if (string.IsNullOrEmpty(text))
            return "";

        return ContieneEspacios(text) ? DividirTextoConEspacios(text , col, saltoLinea) : DividirPalabra(text,col, saltoLinea);
    }

    private static bool ContieneEspacios(string text) => text.Contains(' ');

    private static string DividirTextoConEspacios(string texto, int cantidadMaximaCaracteres, string saltoLinea)
    {
        string textoDivido = string.Empty;

        foreach (var palabraOriginal in texto.Split(' '))
        {
            textoDivido += (string.IsNullOrEmpty(textoDivido)? "" : "\n") + DividirPalabra(palabraOriginal, cantidadMaximaCaracteres, saltoLinea);
        }

        return textoDivido;
    }
    
    private static string DividirPalabra(string texto, int cantidadMaximaCaracteres, string saltoLinea)
    {
        var palabras = new List<string>();
        for (int posicionCaracter = 0; posicionCaracter < texto.Length; posicionCaracter += cantidadMaximaCaracteres)
        {
            int cantidadCaracteresRestantes = texto.Length - posicionCaracter;
            int cantidadCaracteresDividir = Math.Min(cantidadMaximaCaracteres, cantidadCaracteresRestantes);
            
            palabras.Add(texto.Substring(posicionCaracter, cantidadCaracteresDividir));
        }
        
        return string.Join(saltoLinea, palabras);
    }
}