using System;

namespace APIFolha.Modelos

{
    public class FolhaPagamento
    {
        public int Id { get; set; }
        public string NomeFuncionario { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public int Mes { get; set; }
        public int Ano { get; set; }
        public double HorasTrabalhadas { get; set; }
        public double ValorHora { get; set; }
        public double SalarioBruto { get; set; }
        public double Inss { get; set; }
        public double Fgts { get; set; }
        public double IR { get; set; }
        public double SalarioLiquido { get; set; }
    }
}