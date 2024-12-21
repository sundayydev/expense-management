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

        public void RegisterUser(RegisterDto registerDto)
        {
            // Kiểm tra email đã tồn tại
            if (_userRepository.ExistsByEmail(registerDto.Email))
            {
                throw new InvalidOperationException("Email đã tồn tại.");
            }

            // Hash mật khẩu (sử dụng ví dụ đơn giản)
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            // Tạo đối tượng User
            var user = new User
            {
                UserId = Guid.NewGuid(),
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.Now
            };

            // Thêm vào cơ sở dữ liệu
            _userRepository.AddUser(user);
        }

        
        public bool LoginUser(LoginDto loginDto)
        {
            // Lấy thông tin user từ email
            var user = _userRepository.GetUserByEmail(loginDto.Email);

            // Kiểm tra mật khẩu
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new InvalidOperationException("Email hoặc mật khẩu không đúng.");
            }
            
            AppContext.Instance.UserId = user.UserId.ToString();
            
            if(AppContext.Instance.UserId != null)
                return true;
            return false;
        }

        public bool GetUserByEmail(string emailforgot)
        {
            var user = _userRepository.GetUserByEmail(emailforgot);
            if (user == null)
            {
                return false;
            }

           
            return true;
        }

       
        public void UpdatePassword(string email, string newPassword)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new InvalidOperationException("Không tìm thấy tài khoản với email này.");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _userRepository.UpdateUser(user);
        }
        
        public UserDto GetUserByUserId(string userId)
        {
            var user = _userRepository.GetUserByUserId(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User không tồn tại.");
            }
            
            return new UserDto
            {
                FullName = user.FullName,
                Gender = user.Gender,
                Email = user.Email,
            };
        }

    }
}

