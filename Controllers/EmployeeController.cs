using EmployeeManagementMVC.DTOs;
using EmployeeManagementMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: Employee
        //public async Task<IActionResult> Index(string sortOrder, string searchString,int? page)
        //public async Task<IActionResult> Index(string sortOrder, string search, string sort, int page = 1)
        public async Task<IActionResult> Index(string searchString, string sortOrder, int page = 1)
        {
            /*
            ViewBag.CurrentFilter = searchString;

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.EmailSortParam = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.SalarySortParam = sortOrder == "Salary" ? "salary_desc" : "Salary";

            int pageSize = 5;
            int pageNumber = page ?? 1;
            
            var employees = _service.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            { 
              employees = employees.Where(e =>  e.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.Name);
                    break;

                case "Email":
                    employees = employees.OrderBy(e => e.Email);
                    break;

                case "email_desc":
                    employees = employees.OrderByDescending(e => e.Email);
                    break;

                case "Salary":
                    employees = employees.OrderBy(e => e.Salary);
                    break;

                case "salary_desc":
                    employees = employees.OrderByDescending(e => e.Salary);
                    break;

                default:
                    employees = employees.OrderBy(e => e.Name);
                    break;
            }

            var pagedEmployee = employees
                                //.OrderBy(e => e.Id)
                                .ToPagedList(pageNumber, pageSize);
            return View(pagedEmployee);
            */
            int pageSize = 5;

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.EmailSortParam = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.SalarySortParam = sortOrder == "Salary" ? "salary_desc" : "Salary";

            var employees = await _service
                                    .GetEmployeesAsync(searchString, sortOrder, page, pageSize);

            return View(employees);
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var employee = await _context.Employees
            //  .FirstOrDefaultAsync(m => m.Id == id);
            var employee = await _service.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Department,Salary")] EmployeeDTO employeeDto)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(employeeDto);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDto);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var employee = await _context.Employees.FindAsync(id);
            var employee = await _service.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Department,Salary")] EmployeeDTO employeeDto)
        {
            if (id != employeeDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(employeeDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!EmployeeExists(employee.Id))
                    if (await _service.GetByIdAsync(employeeDto.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDto);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var employee = await _context.Employees
            //  .FirstOrDefaultAsync(m => m.Id == id);
            var employee = await _service.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var employee = await _context.Employees.FindAsync(id);
            //var employee = await _repository.GetByIdAsync(id);
            //if (employee != null)
            //{
            //_context.Employees.Remove(employee);
            await _service.DeleteAsync(id);
            //}

            //await _context.SaveChangesAsync();
            //await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool EmployeeExists(int id)
        //{
        //    return _context.Employees.Any(e => e.Id == id);
        //}
    }
}
