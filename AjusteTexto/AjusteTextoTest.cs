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
    
    [Theory]
    [InlineData("this",10,"this")]
    [InlineData("word",2,"wo\nrd")]
    [InlineData("abcdefghij",3,"abc\ndef\nghi\nj")]
    public void b_c_d(string texto, int col, string textoEsperado)
    {
        var result = Wrap(texto, col);

        result.Should().Be(textoEsperado);
    } 
    
    [Theory]
    [InlineData("word word",3,"wor\nd\nwor\nd")]
    [InlineData("word word",6,"word\nword")]
    [InlineData("word word",5,"word\nword")]
    public void e_f_f2(string texto, int col, string textoEsperado)
    {
        var result = Wrap(texto, col);

        result.Should().Be(textoEsperado);
    } 
    
    [Theory]
    [InlineData("word word word",6,"word\nword\nword")]
    [InlineData("word word word",11,"word word\nword")]
    [InlineData("word word word word word word word word",16,"word word word\nword word word\nword word")]
    public void g_h_i(string texto, int col, string textoEsperado)
    {
        var result = Wrap(texto, col);

        result.Should().Be(textoEsperado);
    } 
    
    private static string Wrap(string text, int col)
    {
        const string saltoLinea = "\n";
        
        if (EstaVacio(text))
            return "";

        return ContieneEspacios(text) ? DividirTextoConEspacios(text , col, saltoLinea) : DividirPalabra(text,col, saltoLinea);
    }

    private static string DividirTextoConEspacios(string texto, int cantidadMaximaCaracteres, string saltoLinea)
    {
        string textoDivido = string.Empty;

        foreach (var palabraOriginal in texto.Split(' '))
        {
            string palabra = DividirPalabra(palabraOriginal, cantidadMaximaCaracteres, saltoLinea);

            if (EstaVacio(textoDivido))
            {
                textoDivido = palabra;
                continue;
            }

            textoDivido += PalabraMenorOIgualACantidadMaxima(cantidadMaximaCaracteres, palabra, textoDivido,saltoLinea)
                ? " "
                : saltoLinea;

            textoDivido += palabra;
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
   
    private static bool ContieneEspacios(string text) => text.Contains(' ');
    private static bool EstaVacio(string textoDivido) => string.IsNullOrEmpty(textoDivido);
    private static bool ContieneSaltoDeLinea(string textoDivido, string saltoLinea) => textoDivido.Contains(saltoLinea);
    private static string ObtenerUltimaPalabraPorSaltoDeLinea(string textoDivido, string saltoLinea) => textoDivido.Substring(textoDivido.LastIndexOf(saltoLinea) + 1);
    private static string ObtenerUltimaPalabra(string textoDivido, string saltoLinea) =>
        ContieneSaltoDeLinea(textoDivido, saltoLinea)
            ? ObtenerUltimaPalabraPorSaltoDeLinea(textoDivido, saltoLinea)
            : textoDivido;
    private static bool PalabraMenorOIgualACantidadMaxima(int cantidadMaximaCaracteres, string palabra, string textoDivido, string saltoLinea)
    {
        string ultimaPalabra = ObtenerUltimaPalabra(textoDivido, saltoLinea);
        
        return ultimaPalabra.Length + 1 + palabra.Length <= cantidadMaximaCaracteres;
    }
}