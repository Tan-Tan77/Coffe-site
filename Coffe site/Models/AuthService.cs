using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace Coffe_site.Models

{
    public class AuthService : IAuthService
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;


        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
         
            if(await _context.Users.AnyAsync(u=>u.Phone == request.Phone))
            {
                return new AuthResponse() { Success = false, Message = "Это номер телефона уже зарегистрирован" };  
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);


            var user = new User
            {
                Password = passwordHash,

                Phone = request.Phone,

            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();


            var token = GenerateJwtToken(user);


            return new AuthResponse() {

                Success = true,

                Message = "Регистрация успешна",


                Token = token


            };

        }






        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
          
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Phone == request.Phone);


            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {


                return new AuthResponse()
                {
                    Message = "Неверный телефон или пароль",
                    Success = false


                };
            }

            var token = GenerateJwtToken(user);

            return new AuthResponse()
            {
                Message = "Вход выполнен успешно",
                Success = true,
                Token = token

            };

        }

       



        public string GenerateJwtToken(User user)
        {
            
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credintals = new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);

            var claims = new[]

            {

                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),   
                new Claim(ClaimTypes.MobilePhone, user.Phone),  
                
            };


            var token = new JwtSecurityToken(


                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credintals

                );


            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        
    }
}
