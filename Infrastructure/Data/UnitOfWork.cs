using Infrastructure.Data.EntityFramework;

namespace Infrastructure.Data;

public sealed class UnitOfWork(AppDbContext dataContext) : IUnitOfWork
{
    private bool _isDisposed;

    public async Task<int> CommitAsync()
    {
        return await dataContext.SaveChangesAsync();
    }

    public int Commit()
    {
        return dataContext.SaveChanges();
    }

    public async ValueTask DisposeAsync()
    {
        if (!_isDisposed)
        {
            _isDisposed = true;
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
    }

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing) dataContext.Dispose();
    }

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing) await dataContext.DisposeAsync();
    }
}