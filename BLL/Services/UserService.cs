﻿using BLL.DTO.User;
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
    }
}

