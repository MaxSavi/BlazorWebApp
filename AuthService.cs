//using System;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using BlazorWebApp.Models;
//using BlazorWebApp.Data;

//namespace BlazorWebApp
//{
//    public class AuthService
//    {
//        private readonly BlazorWebAppContext _context;

//        public AuthService(BlazorWebAppContext context)
//        {
//            _context = context;
//        }

//        public async Task<bool> RegisterUserAsync(string username, string password)
//        {
//            // Проверяем, существует ли пользователь с таким именем
//            if (await _context.UserModel.AnyAsync(u => u.Username == username))
//                return false; // Пользователь с таким именем уже существует

//            // Хешируем пароль
//            string passwordHash = HashPassword(password);

//            // Создаем нового пользователя
//            var user = new UserModel { Username = username, Password = password };
//            _context.UserModel.Add(user);
//            await _context.SaveChangesAsync();
//            return true; // Регистрация успешна
//        }

//        public async Task<bool> AuthenticateAsync(string username, string password)
//        {
//            // Получаем пользователя по имени
//            var user = await _context.UserModel.FirstOrDefaultAsync(u => u.Username == username);

//            // Проверяем существует ли пользователь и совпадает ли его пароль
//            return user != null && VerifyPassword(password, user.Password);
//        }

//        // Остальной код сервиса остается неизменным

//        // Методы хеширования и проверки пароля
//        public string HashPassword(string password)
//        {
//            // Хешируем пароль с использованием SHA256
//            using (var sha256 = SHA256.Create())
//            {
//                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//                return Convert.ToBase64String(hashedBytes);
//            }
//        }

//        private bool VerifyPassword(string inputPassword, string hashedPassword)
//        {
//            // Проверяем, совпадают ли хеши паролей
//            string inputPasswordHash = HashPassword(inputPassword);
//            return string.Equals(inputPasswordHash, hashedPassword);
//        }
//    }
//}

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorWebApp.Models;
using BlazorWebApp.Data;

namespace BlazorWebApp
{
    public class AuthService
    {
        private readonly BlazorWebAppContext _context;

        public AuthService(BlazorWebAppContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            // Проверяем, существует ли пользователь с таким именем
            if (await _context.UserModel.AnyAsync(u => u.Username == username))
                return false; // Пользователь с таким именем уже существует

            // Создаем нового пользователя
            var user = new UserModel { Id =0, Username = username, Password = password };
            _context.UserModel.Add(user);
            await _context.SaveChangesAsync();
            return true; // Регистрация успешна
        }

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            // Получаем пользователя по имени
            var user = await _context.UserModel.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            // Проверяем существует ли пользователь и совпадает ли его пароль
            return user != null && user.Password == password;
        }
    }
}
