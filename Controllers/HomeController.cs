using Employee.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xceed.Words.NET;
using System.Linq;
using System.IO;





namespace Employee.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeesContext db;

        public HomeController(ILogger<HomeController> logger, EmployeesContext injectedContext)
        {
            _logger = logger;
            db = injectedContext; 
        }

        public IActionResult Index(string sort)
        {
 
            HomeIndexViewModel model = new
                (

                    EmployeesCount: db.Staff.Count(),
                    Staffs: db.Staff.ToList(),
                    Posts: db.Posts.ToList(),
                    Divisions: db.Divisions.ToList()

                );
 
            return View(model); //передача модели представлению
        }

      
        //Подробная информация о сотрунике
        public IActionResult EmployeeDetail(int? id)
        {


            var model = new EmployeeDetailViewModel
            {
                Staff = db.Staff.SingleOrDefault(p => p.StaffId == id),
                Post = db.Posts.SingleOrDefault(p => p.PositionId == id),
                Division = db.Divisions.SingleOrDefault(p => p.DivisionId ==id),
                DaysSinceHiring = CalculateDaysSinceHiring(db.Staff.SingleOrDefault(p => p.StaffId == id)?.HiringDate),
 
            };

            return View(model);

        }
        // рассчет колличества дней, с момента трудоустройства
        private int CalculateDaysSinceHiring(DateTime? hiringDate)
        {
            if (hiringDate.HasValue)
            {
                TimeSpan timeSinceHiring = DateTime.Now - hiringDate.Value;
                return (int)timeSinceHiring.TotalDays;
            }
            return 0;
        }



        public IActionResult Privacy()
        {

            // Создать модель представления
            HomePrivacyViewModel model = new
            (
                Posts:db.Posts.ToList(),
                Staffs:db.Staff.ToList()
                );

            return View(model);
        }
        public IActionResult Division()
        {
            HomeDivisionViewModel model = new
                (
                    EmployeesCount: db.Staff.Count(),
                    Divisions: db.Divisions.ToList(),
                    Staffs: db.Staff.ToList(),
                    Posts: db.Posts.ToList()
                );
            return View(model);
        }

        public IActionResult StaffingTable()
        {
            HomeDivisionViewModel model = new
                (
                    EmployeesCount: db.Staff.Count(),
                    Divisions: db.Divisions.ToList(),
                    Staffs: db.Staff.ToList(),
                    Posts: db.Posts.ToList()
                );
            return View(model);
        }


        // Добавление должности в таблицу Post
        [HttpGet]
        public IActionResult AddPosition()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPosition(AddPositionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newPosition = new Post
                {
                    PositionId = model.PositionId,
                    PositionName = model.PositionName,
                    TariffRate = model.TariffRate,
                    Coefficient = model.Coefficient
                };

                db.Posts.Add(newPosition);
                db.SaveChanges();

                return RedirectToAction("Privacy", "Home");
            }

            return View(model);
        }

        //добавлене нового подразделения в тпблицу Division
        [HttpGet]
        public IActionResult AddDivision()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddDivision(AddDivisionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newDivision = new Division
                {
                    DivisionName = model.DivisionName,
                    DivisionId = model.DivisionId
                };

                db.Divisions.Add(newDivision);
                db.SaveChanges();

                return RedirectToAction("Division", "Home");
            }

            return View(model);
        }

        //добавление нового сотрудника в таблицу Staff
        [HttpGet]
        public IActionResult AddStaff()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStaff(AddStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Создание нового объекта Staff и заполнение его свойств значениями из модели представления
                var newStaff = new Staff
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    Age = model.Age,
                    PositionId = model.PositionId,
                    Description = model.Description,
                    Experience = model.Experience,
                    PhoneNumber = model.PhoneNumber,
                    PassportData = model.PassportData,
                    DivisionId = model.DivisionId,
                    BirthDate = model.BirthDate,
                    HiringDate = model.HiringDate
                };

                // Обработка загрузки файла документа, если требуется
                if (model.DocumentFile != null && model.DocumentFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        model.DocumentFile.CopyTo(memoryStream);
                        newStaff.Document = memoryStream.ToArray();
                    }
                }

                // Обработка загрузки файла фотографии, если требуется
                if (model.PhotoFile != null && model.PhotoFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        model.PhotoFile.CopyTo(memoryStream);
                        newStaff.Photo = memoryStream.ToArray();
                    }
                }

                // Добавление нового сотрудника в контекст базы данных и сохранение изменений
                db.Staff.Add(newStaff);
                db.SaveChanges();

                // Перенаправление на главную страницу
                return RedirectToAction("Index", "Home");
            }
            
            // Если модель не прошла валидацию, возвращаем представление с ошибками валидации
            return View(model);
        }

        //Удаление сотрудника из таблицы Staff
        [HttpGet]
        public IActionResult DeleteEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteEmployee(int id)
        {
            // Находим сотрудника по его идентификатору
            var employee = db.Staff.SingleOrDefault(p => p.StaffId == id);

            if (employee == null)
            {
                // Если сотрудник не найден, вернуть ошибку или выполнить нужное действие
                return NotFound();
            }

            // Удаление сотрудника из базы данных
            db.Staff.Remove(employee);
            db.SaveChanges();

            // Перенаправление на страницу со списком сотрудников или другое нужное действие
            return RedirectToAction("Index");
        }

        //Удаление должности

        public IActionResult DeletePosition(int? id)
        {
            var model = new DeletePositionViewModel
            {
                Staff = db.Staff.SingleOrDefault(p => p.StaffId == id),
                Post = db.Posts.SingleOrDefault(p => p.PositionId == id),
                Division = db.Divisions.SingleOrDefault(p => p.DivisionId == id)
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult DeletePostPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeletePostPost(int id)
        {
            // Находим сотрудника по его идентификатору
            var position = db.Posts.SingleOrDefault(p => p.PositionId == id);

            if (position == null)
            {
                // Если сотрудник не найден, вернуть ошибку или выполнить нужное действие
                return NotFound();
            }

            // Удаление сотрудника из базы данных
            db.Posts.Remove(position);
            db.SaveChanges();

            // Перенаправление на страницу со списком сотрудников или другое нужное действие
            return RedirectToAction("Privacy");
        }
        //экшен страницы удаления
        public IActionResult DeleteDivision(int? id)
        {
            var model = new DeletePositionViewModel
            {
                Staff = db.Staff.SingleOrDefault(p => p.StaffId == id),
                Post = db.Posts.SingleOrDefault(p => p.PositionId == id),
                Division = db.Divisions.SingleOrDefault(p => p.DivisionId == id)
            };

            return View(model);
        }
        //удаление подразделения 
        [HttpGet]
        public IActionResult DeleteDivisionDivision()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteDivisionDivision(int id)
        {
            // Находим сотрудника по его идентификатору
            var division = db.Divisions.SingleOrDefault(p => p.DivisionId == id);

            if (division == null)
            {
                // Если сотрудник не найден, вернуть ошибку или выполнить нужное действие
                return NotFound();
            }

            // Удаление сотрудника из базы данных
            db.Divisions.Remove(division);
            db.SaveChanges();

            // Перенаправление на страницу со списком сотрудников или другое нужное действие
            return RedirectToAction("Division");
        }
     



        public IActionResult DownloadDocument(int id)
        {
            var staff = db.Staff.FirstOrDefault(s => s.StaffId == id);
            if (staff == null || staff.Document == null)
            {
                // Возвращаем ошибку или редирект, если документ не найден
                return NotFound();
            }

            // Определяем тип контента
            string contentType = "application/pdf";

            // Возвращаем документ в ответе
            return File(staff.Document, contentType);
        }

        // GET: Редактирование сотрудника
        [HttpGet]
        public IActionResult EditEmployee(int? id)
        {
            // Получаем информацию о сотруднике по его идентификатору
            var employee = db.Staff.SingleOrDefault(p => p.StaffId == id);

            if (employee == null)
            {
                // Если сотрудник не найден, вернуть ошибку или выполнить нужное действие
                return NotFound();
            }

            // Создаем модель представления EditEmployeeViewModel и заполняем ее данными о сотруднике
            var model = new EditEmployeeViewModel
            {
                StaffId = employee.StaffId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age,
                PositionId = employee.PositionId,
                Description = employee.Description,
                Experience = employee.Experience,
                PhoneNumber = employee.PhoneNumber,
                PassportData= employee.PassportData,
                DivisionId = employee.DivisionId,
                BirthDate= employee.BirthDate,
                HiringDate = employee.HiringDate,
                MiddleName = employee.MiddleName,
                // Добавьте остальные свойства модели представления, если необходимо
            };

            return View(model);
        }

        // POST: Редактирование сотрудника
        [HttpPost]
        public IActionResult EditEmployee(EditEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Получаем информацию о сотруднике, который требуется отредактировать
                var employee = db.Staff.SingleOrDefault(p => p.StaffId == model.StaffId);

                if (employee == null)
                {
                    // Если сотрудник не найден, вернуть ошибку или выполнить нужное действие
                    return NotFound();
                }

                // Обновляем свойства сотрудника значениями из модели представления
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Age = model.Age;
                employee.PositionId = model.PositionId;
                employee.Description = model.Description;
                employee.Experience = model.Experience;
                employee.PhoneNumber = model.PhoneNumber;
                employee.PassportData = model.PassportData;
                employee.DivisionId = model.DivisionId;
                employee.BirthDate = model.BirthDate;
                employee.HiringDate = model.HiringDate;
                employee.MiddleName = model.MiddleName;
                // Обновите остальные свойства сотрудника, если необходимо

                // Сохраняем изменения в базе данных
                db.SaveChanges();

                // Перенаправляем пользователя на страницу с информацией о сотруднике или другую нужную страницу
                return RedirectToAction("EmployeeDetail", new { id = model.StaffId });
            }

            // Если модель не прошла валидацию, возвращаем представление с ошибками валидации
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}