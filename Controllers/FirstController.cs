using System;
using System.Data;
using System.IO;
using System.Linq;
using App.Services;
using AppMvc1.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Controlers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;//Khai báo một trường private read only có tên là _logger

        private readonly ProductService _productService;//Khai báo một trường private read only có tên là _productService
        public FirstController(ILogger<FirstController> logger, ProductService productService)//inject 1 controller bằng cách khai báo
        {
            _logger = logger;//Controller tạo ra đã tự động inject logger vào đó
            _productService = productService;
        }


        public string Index()
        {
            //Lấy được các đối tượng 
            //this.Request
            //this.Response
            //this.RouteData
            //this.User
            //this.ModelStale
            //this.ViewData
            //this.ViewBag
            //this.Url
            //this.TemData
            // _logger.log(LogLevel.Warning, "Thong bao abc");
            

            _logger.LogWarning("Thong bao warrning");
            _logger.LogError("Thong bao Error");
            _logger.LogDebug("Thong bao debug");
            _logger.LogCritical("Thong bao critical");
            _logger.LogInformation("Index Action");
            //Serilog xuất thông tin log ở bên thứ 3 và lưu trữ ở nơi nào đó 
            //vậy có nghĩa là sau này gọi đến Ilogger là gọi đến serilog

            return "Tôi là index của first";
        }

        //action trong controller
        public void Nothing()
        {
            _logger.LogInformation("Nothing Action");
            Response.Headers.Add("oh","loi xay ra roi sao? ");
        }

        public object Anything() => new int[] {1,2,3};

        public IActionResult Readme()
        {
            //để tạo ra IActionResult thì trong Controller có sẵn phương thức là Content()
            //Khai báo ra 1 chuỗi để thiết lập nội dung của content 1 đoạn text viết trên nhiều dòng
            var content = @"
            Xin chao Vo Anh Tuan,
            ban dang hoc ve ASP.NET MVC


            VOANHTUAN
            ";
            return Content(content,"text/html");//nội dung được thiết lập vào tham số thứ nhất- tham số thứ 2 là thiết lập kiểu văn bản trả về. co the la html hoac plain
        }

        public IActionResult Bird()
        {
            //ta phải thiết lập đường dẫn tới file hình ảnh -- muốn đọc thông tin thì ta phải inject dịch vụ IWebHostEnviroment--- Ta truy cập vào Startup và ContentRootPath để có được được đường dẫn đến thư mục MVC1.Net
            //Startup.ContentRootPath 
            string filePath = Path.Combine(Startup.ContentRootPath,"Files","Birds.jpg");//tạo đường dẫn đến thư mục ứng dụng ta khai báo mảng với tên gọi filePath
            var bytes = System.IO.File.ReadAllBytes(filePath);//Sau khi ta có đường dẫn rồi-> đọc toàn bộ nội dung file đó vào một cái mảng bytes để đọc file Birds.jpg
            return File(bytes, "image/jpg");
        }


        public IActionResult IphonePrice()
        {
            //tao thanh chuoi Json
            return Json(
                new {
                    productName = "Iphone 13",
                    Price = 2500
                }
            );
        }

        public IActionResult Privacy()
        {
            var url = Url.Action("Privacy","Home");
            //tham so la dia chi can chuyen huong la URL chi can dam bao la local nghia la dia chi url ko co phan host
            _logger.LogInformation("Chuyen huong den "+ url);
            return LocalRedirect(url);
        }

        public IActionResult Google()
        {
            //vì google không phải là web local nen phai dung phương thức Redirect
            var url = "https://google.com";
            _logger.LogInformation("Chuyen huong den "+ url);
            return Redirect(url);
        }


        //Phương thức quan trọng nhất của MVC là gọi ra được ViewResult
        public IActionResult HelloView(string username)
        {
            if(string.IsNullOrEmpty(username))
                username = "(Khách)";

            //sử dụng phương thức view của ViewResult để tạo ra controller trả về action
            //phương thức view() -> yêu cầu sử dụng Razor engine, đọc và thi hành file .cshtlm được gọi là template
            //------------------------------------------
            //trường hợp thứ 1 sử dụng gọi phương thức view(template) chỉ ra file cshtml gọi là template-chú ý template là đường dẫn tuyệt đối tới file cshtml
            //trương hợp 2 muốn truyền dữ liệu từ control sang view thì chúng ta thiết lập ở tham số thứ 2 View(template,model)
            //return View("/MyView/xinchao1.cshtml",username);

            //xinchao2.cshtml -> /View/First/xinchao2.cshtml
            //return View("xinchao2",username);

            //trường hợp 3 nó yêu cầu razor engine mở một file template cshtml trùng tên với cả action tức là HelloView
            //cụ thể nó sẽ tìm HelloView.cshtml ->  /View/First/HelloView.cshtml
            //tìm các file template theo thứ tự /View/Controller/Action.cshtlm
            //return View((object)username);

            return View("xinchao3",username);


            //2 trường hợp sử dụng phổ biến là
            // View(template) và View(template,Model)
        }
        
        [TempData]
        public string StatusMessage {get;set;}

        public IActionResult ViewProduct(int? id)
        {
            var product = _productService.Where(p => p.Id == id).FirstOrDefault();
            if(product == null)
            {
                //TempData["StatusMessage"] = "San pham ban yeu cau khong co";
                StatusMessage = "Sản phẩm bạn yêu cầu không có";
                return Redirect(Url.Action("Index", "Home"));
            }

            //tìm mặc định /View/First/ViewProduct.cshtml nếu không thấy nó tiếp tục tìm trên model Myview
            // /MyView/First/ViewProduct.cshtml   
            //trường hợp 1 truyền dữ liệu sang view bằng cách thiết lập model
            return View(product);

            //trường hợp 2 ta truyền dữ liệu từ controller sang view bằng cách sử dụng thuộc tính ViewData
            //this.ViewData["product"] = product;
            //return View("ViewProduct2");

            //ViewBag.product = product;

            




        }

        //IActionResult
        // ContentResult               | Content()
        // EmptyResult                 | new EmptyResult()
        // FileResult                  | File()
        // ForbidResult                | Forbid()
        // JsonResult                  | Json()
        // LocalRedirectResult         | LocalRedirect()
        // RedirectResult              | Redirect()
        // RedirectToActionResult      | RedirectToAction()
        // RedirectToPageResult        | RedirectToRoute()
        // RedirectToRouteResult       | RedirectToPage()
        // PartialViewResult           | PartialView()
        // ViewComponentResult         | ViewComponent()
        // StatusCodeResult            | StatusCode()
        // ViewResult                  | View()
    }
}