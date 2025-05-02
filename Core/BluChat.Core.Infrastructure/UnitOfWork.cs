using BluChat.Core.Data.EF.Context;
using BluChat.Core.Data.EF.Repositories;
using BluChat.Core.Infrastructure.Logger;
using BluChat.Core.Infrastructure.Logger.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace BluChat.Core.Infrastructure;

public class UnitOfWork
{
    private readonly SqlLiteContext _context;

    public UserRepository Users { get; }
    public ChatRepository Chats { get; }
    public MessageRepository Messages { get; }

    public ILogger Logger { get; set; }

    public UnitOfWork(SqlLiteContext context)
    {
        _context = context;

        Users = new UserRepository(_context);
        Chats = new ChatRepository(_context);
        Messages = new MessageRepository(_context);
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