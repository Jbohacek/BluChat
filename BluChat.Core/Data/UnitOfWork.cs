using BluChat.Core.Data.Interfaces;
using BluChat.Core.Data.Repositories;
using BluChat.Core.Logger;
using BluChat.Core.Logger.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BluChat.Core.Data;

public class UnitOfWork
{
    private readonly SqlLiteContext _context;

    public UserRepository Users { get; }
    public ChatMessageRepository Chats { get; }
    public ChatLogRepository Messages { get; }

    public ILogger Logger { get; set; }

    public UnitOfWork(SqlLiteContext context)
    {
        _context = context;

        Users = new UserRepository(_context);
        //Chats = new ChatMessageRepository(_context);
        //Messages = new ChatLogRepository(_context);
    }

    public void Save()
    {
        var changes = _context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
            .ToList();

        foreach (var entityEntry in changes)
        {
            Logger.Add(LogFactory.ContextChange(entityEntry));
        }

        _context.SaveChanges();
    }
}