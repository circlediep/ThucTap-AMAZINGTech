using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TASK_Nhom_01.Data;
using TASK_Nhom_01.Repositories;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Thêm các dịch vụ vào container DI (Dependency Injection).
builder.Services.AddControllers();

// Cấu hình Swagger để tài liệu hóa và kiểm thử API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình dịch vụ CORS (Cross-Origin Resource Sharing)
// Thiết lập chính sách cho phép tất cả các nguồn gốc, tiêu đề và phương thức
builder.Services.AddCors(option =>
    option.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod()
    )
);

// Cấu hình dịch vụ Identity
// Thêm các dịch vụ Identity với ứng dụng người dùng và vai trò mặc định
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<XuongMayContext>()
    .AddDefaultTokenProviders();

// Cấu hình Authentication với JWT (JSON Web Tokens)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

// Thêm dịch vụ DbContext vào container DI, cấu hình XuongMayContext để sử dụng SQL Server
builder.Services.AddDbContext<XuongMayContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("XuongMay"));
});

// Thêm dịch vụ AutoMapper vào container DI
builder.Services.AddAutoMapper(typeof(Program));

// Đăng ký các dịch vụ repository với container DI
builder.Services.AddScoped<IMayRepository, MayRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection(); 

app.UseAuthorization();   

app.MapControllers();


app.Run();
