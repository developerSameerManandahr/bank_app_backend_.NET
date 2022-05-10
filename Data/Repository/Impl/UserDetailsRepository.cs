using System;
using worksheet2.Model;

namespace worksheet2.Data.Repository.Impl
{
    public sealed class UserDetailsRepository : IUserDetailsRepository, IDisposable
    {
        private readonly BankContext _context;

        private bool _disposed;

        public UserDetailsRepository(BankContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void CreateUserDetails(UserDetails userDetails)
        {
            _context.UserDetails
                .Add(userDetails);
            _context.SaveChanges();
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }
    }
}