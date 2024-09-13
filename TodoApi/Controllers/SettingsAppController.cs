using AdminControl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    public class SettingsAppController : Controller
    {
        private readonly BloggingContext _context;
        public readonly IWebHostEnvironment _environment;
        private readonly string _connectionString;

        public SettingsAppController(BloggingContext context, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _context = context;
            _environment = environment;
            _connectionString = configuration.GetConnectionString("conn2");

        }
        public IActionResult GetBackpDataIndex()
        {
            var allData = _context.BackpDatabases.ToList();
            return View(allData);
        }
        public async Task<IActionResult> DownloadBackup()
        {
            try
            {
                string backupFileName = $"database_backup_{DateTime.Now.ToString("yyyyMMddHHmmss")}.bak";
                string backupFilePath = Path.Combine(_environment.WebRootPath, "uploadBackup");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand($"BACKUP DATABASE [db_a94fd1_almazra] TO DISK = '{backupFilePath}' WITH FORMAT;", connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(backupFilePath);
                return File(fileBytes, "application/octet-stream", backupFileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to download backup: {ex.Message}");
            }
        }
        public IActionResult GetListEmployee()
        {
            var allData = _context.Employees.Where(b => !b.IsDelete && !b.IsHide).ToList();

            return View(allData);
        }
        public IActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            try
            {
                employee.Email = employee.Email.ToLower().Trim();
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(GetListEmployee));
            }
            catch (Exception)
            {
                return View();
            }
        }
        public IActionResult CheckEmail(string email)
        {
            var Employee = _context.Employees.FirstOrDefault(b => b.Email == email);

            if (Employee == null)
            {
                return Ok(new { exists = false });
            }
            else
            {
                return Ok(new { exists = true });
            }
        }

        public IActionResult EditEmployee(int id)
        {
            var Employee = _context.Employees.Find(id);
            return View(Employee);
            
        }
        [HttpPost]
        public IActionResult EditEmployee(Employee employee)
        {
            employee.Email = employee.Email.ToLower().Trim();
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(GetListEmployee));
        }
        public IActionResult CheckEmailEdit(string email, int empid)
        {
            var findEmployee = _context.Employees.FirstOrDefault(b => b.Id == empid);
            if (findEmployee.Email == email.ToLower().Trim())
            {
                return Ok(new { exists = false });
            }
            else
            {
                var emailExists = _context.Employees.FirstOrDefault(b => b.Email == email.ToLower().Trim());

                if (emailExists == null)
                {
                    return Ok(new { exists = false });
                }
                else
                {
                    return Ok(new { exists = true });
                }
            }
        }
            public IActionResult DeleteRow(int id)
            {
                var isDeleted = _context.Employees.Find(id);


                if (isDeleted != null)
                {
                    isDeleted.IsDelete = true;
                    _context.Employees.Update(isDeleted);
                    _context.SaveChanges();
                    return Ok(new { success = true });
                }
                else
                {
                    return BadRequest(new { success = false });
                }
            }


    }
}
