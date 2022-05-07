﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using worksheet2.Model;
using worksheet2.Model.Request;

namespace worksheet2.Data.Repository.Impl
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly BankContext _context;

        private bool _disposed;

        public UserRepository(BankContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            var userEntry = _context.Users
                .Add(user);
            _context.SaveChanges();
            return userEntry.Entity;
        }

        public User GetUserByUserId(string userId)
        {
            return _context.Users
                .Include(u => u.UserDetails)
                .FirstOrDefault(user => user.UserId == userId);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users
                .Include(u => u.UserDetails)
                .FirstOrDefault(user => user.UserName == username);
        }

        public User GetUserByAccountNumber(string accountNumber)
        {
            return _context.Users
                .Include(u => u.UserDetails)
                .FirstOrDefault(user => user.AccountNumber == accountNumber);
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