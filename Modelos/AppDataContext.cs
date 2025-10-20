using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace APIFolha.Modelos;
public class AppDataContext : DbContext
{
    public DbSet<FolhaPagamento> FolhaPagamentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=DBFolhaPagamentos.db");
    }
}
