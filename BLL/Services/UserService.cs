using BLL.DTO.User;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public void RegisterUser(RegisterDTO registerDTO)
        {
            // Kiểm tra email đã tồn tại
            if (_userRepository.ExistsByEmail(registerDTO.Email))
            {
                throw new InvalidOperationException("Email đã tồn tại.");
            }

            // Hash mật khẩu (sử dụng ví dụ đơn giản)
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);

            // Tạo đối tượng User
            var user = new User
            {
                UserId = Guid.NewGuid(),
                FullName = registerDTO.FullName,
                Email = registerDTO.Email,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.Now
            };

            // Thêm vào cơ sở dữ liệu
            _userRepository.AddUser(user);
        }
    }
}
