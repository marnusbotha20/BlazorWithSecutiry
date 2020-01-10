using BlazorWithSecutiry.Data;
using BlazorWithSecutiry.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithSecutiry.DataAccess
{
    public class EmployeeDataAccessLayer
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                return _context.Employees.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Employee GetEmployeeData(long id)
        {
            try
            {
                var employee = _context.Employees.Find(id);
                _context.Entry(employee).State = EntityState.Detached;
                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddCourse(Courses courses)
        {
            try
            {
                _context.Courses.Add(courses);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            try
            {
                Employee emp = _context.Employees.Find(id);
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Cities> GetCityData()
        {
            try
            {
                return _context.Cities.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Course Methods
        public List<Courses> GetCourses()
        {
            return _context.Courses.Include(x => x.EmployeeCourse).ToList();
        }
        public List<Courses> GetEmployeeCourses(long Id, bool link)
        {
            var courseData = _context.Courses.ToList();
            var LinkingTableData = _context.EmployeeCourse.Where(x => x.EmployeeId == Id).ToList();

            if (link)
            {
                return courseData.Where(p => !LinkingTableData.Any(p2 => p2.CourseId == p.CourseId)).ToList();
            }

            return courseData.Where(p => LinkingTableData.Any(p2 => p2.CourseId == p.CourseId)).ToList();
        }
        public int GetCoursesCount(long Id)
        {
            var counter = _context.EmployeeCourse.Where(x => x.CourseId == Id).ToList().Count();
            return counter;
        }
        public double CountCredits(long Id)
        {
            double totCredits = 0;

            List<Courses> courses = _context.Courses.ToList();
            List<EmployeeCourse> employeeCourses = _context.EmployeeCourse.ToList();

            var list = (from x in courses
                       join y in employeeCourses on x.CourseId equals y.CourseId into results
                       from _results in results.DefaultIfEmpty()
                       where _results.EmployeeId == Id
                       select new Courses
                       { 
                            CourseId = _results.CourseId,
                            CourseName = x.CourseName,
                            Credits = x.Credits
                       }).ToList();

            foreach (var item in list)
            {
                totCredits += item.Credits;
            }
            return totCredits;
        }
        public Courses GetCoursesDetails(int id)
        {
            try
            {
                var course = _context.Courses.Find(id);
                _context.Entry(course).State = EntityState.Detached;
                return course;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCourse(Courses courses)
        {
            _context.Entry(courses).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            try
            {
                Courses c = _context.Courses.Find(id);
                _context.Courses.Remove(c);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void LinkCourses(long employeeID, List<string> EmployeeCourses, bool LinkAction)
        {
            foreach (var course in EmployeeCourses)
            {
                try
                {
                    var item = new EmployeeCourse()
                    {
                        EmployeeId = employeeID,
                        CourseId = Convert.ToInt32(course)
                    };

                    if (LinkAction)
                    {
                        _context.EmployeeCourse.Add(item);
                    }
                    else
                    {
                        var ec = _context.EmployeeCourse.Find(item.CourseId, employeeID);
                        _context.EmployeeCourse.Remove(ec);
                    }
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}
