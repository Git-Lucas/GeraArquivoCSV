//var pessoas = new List<Pessoa> { 
//    new Pessoa("Lucas", "55319999999991", DateTime.Now.AddYears(-22)),
//    new Pessoa("João", "55319999999992", DateTime.Now.AddYears(-25)),
//    new Pessoa("Maria", "55319999999993", DateTime.Now.AddYears(-60)),
//    new Pessoa("José", "55319999999994", DateTime.Now.AddYears(-50)),
//    new Pessoa("Mateus", "55319999999995", DateTime.Now.AddYears(-30)),
//    new Pessoa("Olívia", "55319999999996", DateTime.Now.AddYears(-28)),
//    new Pessoa("Lavínia", "55319999999997", DateTime.Now.AddYears(-16))
//};

////Através do Select, todas os objetos da lista de Pessoa, são convertidos para uma lista de String, através do implicit operator
//var data = pessoas.Select(x => (string)x);
//File.WriteAllLines("C:\\WorkSpace\\VisualStudio\\GeraArquivoCSV\\GeraArquivoCSV\\arquivo.csv", data);

var data = File.ReadAllLines("C:\\WorkSpace\\VisualStudio\\GeraArquivoCSV\\GeraArquivoCSV\\arquivo.csv");
var pessoas = new List<Pessoa>();
foreach(var item in data)
    //A cada String de "data", o implicit operator transforma em objeto Pessoa, e adiciona na lista de Pessoa "pessoas"
    pessoas.Add(item);
foreach(var item in pessoas)
    //ToTelefone() é estático e poderia ser utilizado em qualquer lugar da aplicação (método que adiciona a máscara de telefone)
    Console.WriteLine($"Nome: {item.Nome} | Telefone: {item.Telefone.ToTelefone()} | DataNascimento: {item.DataNascimento.ToString("dd/MM/yyyy")}");

public class Pessoa
{
    public Pessoa(string nome, string telefone, DateTime dataNascimento)
    {
        Nome = nome;
        Telefone = telefone;
        DataNascimento = dataNascimento;
    }
    
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; set; }

    public static implicit operator string(Pessoa p) => 
        $"{p.Nome},{p.Telefone},{p.DataNascimento.ToString("dd/MM/yyyy")}";

    public static implicit operator Pessoa(string texto)
    {
        var result = texto.Split(",");

        return new Pessoa(result[0], result[1], result[2].ToDateTime());
    }
}

//Extension Methods
public static class StringExtension
{
    public static DateTime ToDateTime(this string texto)
    {
        var result = texto.Split("/");

        return new DateTime(int.Parse(result[2]), int.Parse(result[1]), int.Parse(result[0]));
    }

    public static string ToTelefone(this string texto) =>
        $"+{texto.Substring(0,2)} {texto.Substring(2,2)} {texto.Substring(4,5)}-{texto.Substring(9,4)}";
}