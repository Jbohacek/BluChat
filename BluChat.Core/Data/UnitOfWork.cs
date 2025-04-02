using BluChat.Core.Data.Interfaces;
using BluChat.Core.Data.Repositories;

namespace BluChat.Core.Data;

public class UnitOfWork
{
    private readonly AppDbContext _context;

    public UserRepository Users { get; }


    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        Users = new UserRepository(_context);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}