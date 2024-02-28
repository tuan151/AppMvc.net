## Controller
-là một lớp kế thừa lớp Controller: Micrsoft.AspNetCore.Mvc.Controller
-Action trong controller là một phương thức public (không được static)
-Action trả về bất kỳ kiểu dữ liệu nào, thường là IActionResult
-Các dịch vụ inject vào controller qua hàm tạo
## View
-Là file .cshtml
-View cho Action lưu tại: /View/ControllerName/ActionName.cshtml
-Thêm thư mục lưu trữ View:
//nếu có chỉ số là{0} thì tương đương -> tên Action
                //{1} -> ten Controller
                //{2} -> ten Area
                
                options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);
## Truyền dữ liệu sang View
-Model
-ViewData
-ViewBag
-TemData