using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using cs58_Razor_09.Models;
using Microsoft.AspNetCore.Identity;
using cs58_Razor_09.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace cs58_Razor_09
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            // đăng kí dịch vụ DB context
            services.AddDbContext<MyBlogContext>(options=>
            {
                string connectionstring = Configuration.GetConnectionString("MyBlogContext");
                options.UseSqlServer(connectionstring);
            });

            // đăng ký Identity
            // Thêm vào dịch vụ Identity với cấu hình mặc định cho AppUser (model user)
            // vào IdentityRole (model Role - vai trò)
            services.AddIdentity<AppUser, IdentityRole>()
                // Thêm triển khai EF lưu trữ thông tin về Idetity (theo AppDbContext -> MS SQL Server).
                .AddEntityFrameworkStores<MyBlogContext>()
                // Thêm Token Provider - nó sử dụng để phát sinh token (reset password, confirm email ...)
                // đổi email, số điện thoại .
                .AddDefaultTokenProviders();

            //// đoạn code này giống bên trên nhưng nó có sử dụng mấy trang razor login,logout,default
            //// code bên trên thì phải tự thiết kết
            //services.AddDefaultIdentity<AppUser>()
            //    // Thêm triển khai EF lưu trữ thông tin về Idetity (theo AppDbContext -> MS SQL Server).
            //    .AddEntityFrameworkStores<MyBlogContext>()
            //    // Thêm Token Provider - nó sử dụng để phát sinh token (reset password, confirm email ...)
            //    // đổi email, số điện thoại .
            //    .AddDefaultTokenProviders();


            // Truy cập IdentityOptions
            services.Configure<IdentityOptions>(options => {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = true;         // yêu cầu xác thực tài khoản để đăng nhập
            });


            // đăng ký gửi mail
            services.AddOptions();
            var mailsetting = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailsetting);
            services.AddSingleton<IEmailSender, SendMailService>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login/";
                options.LogoutPath = "/Logout/";
                // đường dẫn tới trang khi user bị cấm truy cập
                options.AccessDeniedPath = "/KhongDuocTruyCap";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Phục hồi thông tin đăng nhập (xác thực), login logout
            app.UseAuthentication();
            // Phục hồi thông tin về quyền của User
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
/*
 * CRUD
 * phát sinh các trang theo model Article
 * - dc databasecontext
 * dotnet aspnet-codegenerator razorpage -m cs58_Razor_09.Models.Article -dc cs58_Razor_09.Models.MyBlogContext -outDir Pages/Blog -udl --referenceScriptLibraries
 *              
 *  Identity:
 *     - Authitication : Xác định danh tính -> login logout
 *     - Authorization: xác thực quyền truy cập, admin thì có quyền đăng bài
 *     - QuẢN LÝ :user, Role,Sign up
 *     
 *     /Identity/Account/Login
 *     /Identity/Account/Manage
 *     
 *     SigninManager<AppUser>
 *     UserManager<AppUser>
 *     
 *     tạo ra các page Identity
 *     dotnet aspnet-codegenerator identity -dc cs58_Razor_09.Models.MyBlogContext
 */

