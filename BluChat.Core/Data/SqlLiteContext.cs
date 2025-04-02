using BluChat.Core.Data.Interfaces;
using BluChat.Core.UserFolder;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace BluChat.Core.Data;

public class SqlLiteContext : AppDbContext, IAppDbContext
{
    private string FileName { get; set; }

    public SqlLiteContext()
    {
        
    }

    public SqlLiteContext(string fileName) : base()
    {
        FileName = fileName;
        Batteries.Init();

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=" + AppDomain.CurrentDomain.BaseDirectory + FileName + ".db");
    }
}
