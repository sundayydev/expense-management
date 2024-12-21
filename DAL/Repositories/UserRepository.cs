using DAL.Models;
using System;
using System.Collections.Generic;
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
            var existingUser = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);

            if (existingUser != null)
            {
                existingUser.FullName = user.FullName; 
                existingUser.Email = user.Email;
                existingUser.PasswordHash = user.PasswordHash;

                _context.SaveChanges();
            }
        }
    }
}
