﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository
    {
        private readonly MyDBContext _context = new MyDBContext();

        public UserRepository()
        { }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool ExistsByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
      
        public void UpdateUser(User user)
        {
            _context.Users.AddOrUpdate(user);
            _context.SaveChanges();
        }
        
        public User GetUserByUserId(string userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId.ToString() == userId);
        }
    }
}
