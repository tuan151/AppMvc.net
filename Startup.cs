using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AppMvc1.Net
{
    public class Startup
    {
        public static string ContentRootPath {get;set ;}
        //Khai báo IWebHostEnvironment env inject vào Startup đọc vào cái thông tin thư mục lưu vào thuộc tính của Startup
        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;//Thiết lập đường dẫn
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            //services.AddTransient(typeof(ILogger<>),typeof(Logger<>)); serilog
            //thiết lập cấu hình cho razor engine bằng cách gọi
            services.Configure<RazorViewEngineOptions>(options =>{
                //mặc định sẽ có thiết lập là tìm các file template trong thư mục /View/Controller/Action.cshtml
                //không thấy thì tiếp tục tìm trên /MyView/Controller/Action.cshtml
                
                //nếu có chỉ số là{0} thì tương đương -> tên Action
                //{1} -> ten Controller
                //{2} -> ten Area
                
                options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);
            });
            //nhung cach viet khac nhau de tao ra service
            //services.AddSingleton<ProductService>();
            //services.AddSingleton<ProductService, ProductService>();
            //services.AddSingleton(typeof(ProductService));
            services.AddSingleton(typeof(ProductService),typeof(ProductService));

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//Xác thực danh tính
            app.UseAuthorization();//Xác thực quyền truy cập

            app.UseEndpoints(endpoints =>
            {

                //URL có dạng : /{controller}/{action}/{id?}
                //ví dụ Abc/Xyz thì máy sẽ khởi tạo controller Abc sau đó gọi method Xyz
                //Home/Index
                // First/Index
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
