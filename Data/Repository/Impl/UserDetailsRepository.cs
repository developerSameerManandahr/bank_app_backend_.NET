using System;
using worksheet2.Model;

namespace worksheet2.Data.Repository.Impl
{
    public class UserDetailsRepository : IUserDetailsRepository, IDisposable
    {
        private readonly BankContext _context;

        private bool _disposed;

        public void CreateUserDetails(UserDetails userDetails)
        {
            _context.UserDetails
                .Add(userDetails);
            _context.SaveChanges();
        }

        public UserDetailsRepository(BankContext context)
        {
            _context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}