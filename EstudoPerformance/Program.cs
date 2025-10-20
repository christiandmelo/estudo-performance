// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;

class Program
{
  static void Main(string[] args)
  {
    int contador = 500000; // quantidade de registros
    int qtdBuscas = 40000;
    var random = new Random();

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Teste List vs Dictionary\n");
    Console.ResetColor();

    // Criar lista
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Criando List com " + contador + " registros Ex: new List<lan>()");
    Console.ResetColor();

    var _logList = new List<lan>();
    for (int i = 0; i < contador; i++)
    {
      _logList.Add(new lan
      {
        coligada = 1,
        id = i,
        mgs = new StringBuilder()
      });
    }

    // Criar dicionário
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Criando Dictionary com " + contador + " registros Ex: new Dictionary<(int coligada, int id), lan>()");
    Console.ResetColor();

    var _logLancamento = new Dictionary<(int coligada, int id), lan>();
    for (int i = 0; i < contador; i++)
    {
      _logLancamento[(1, i)] = new lan
      {
        coligada = 1,
        id = i,
        mgs = new StringBuilder()
      };
    }

    // Testar busca na lista
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("\nRealizando " + qtdBuscas + " buscas com List Ex: .Find(l => l.coligada == 1 && l.id == buscaId)");
    Console.ResetColor();

    var swList = Stopwatch.StartNew();
    for (int i = 0; i < qtdBuscas; i++)
    {
      int buscaId = random.Next(0, contador);
      var log = _logList.Find(l => l.coligada == 1 && l.id == buscaId);
      log?.mgs.AppendLine("Teste");
    }
    swList.Stop();

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Tempo com List (Find): {swList.ElapsedMilliseconds} ms - {swList.Elapsed}");
    Console.ResetColor();

    // Testar busca no dicionário
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("\nRealizando " + qtdBuscas + " buscas com Dictionary Ex: .TryGetValue((1, buscaId), out var log)");
    Console.ResetColor();

    var swDict = Stopwatch.StartNew();
    for (int i = 0; i < qtdBuscas; i++)
    {
      int buscaId = random.Next(0, contador);
      if (_logLancamento.TryGetValue((1, buscaId), out var log))
      {
        log.mgs.AppendLine("Teste");
      }
    }
    swDict.Stop();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Tempo com Dictionary (TryGetValue): {swDict.ElapsedMilliseconds} ms - {swDict.Elapsed}");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\n - Conclusão: ");
    Console.ResetColor();

    Console.WriteLine(
        " - List.Find e LINQ (Where/FirstOrDefault) percorrem a lista inteira: complexidade O(n).\n" +
        "   Isso vira um gargalo quando há centenas de milhares de elementos.\n" +
        " - Dictionary.TryGetValue usa uma tabela hash interna: complexidade O(1).\n" +
        "   Ou seja, acesso direto, praticamente instantâneo, mesmo com milhões de registros.\n\n" +
        " - Por isso no teste o Dictionary é mais rápido que List/Where/Find."
    );
  }
}

public class lan
{
  public int coligada { get; set; }
  public int id { get; set; }
  public StringBuilder mgs { get; set; }
}
