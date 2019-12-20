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
        private readonly EmployeeContext db;

        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                return db.Employees.ToList();
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
                var employee = db.Employees.Find(id);
                db.Entry(employee).State = EntityState.Detached;
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
                db.Employees.Add(employee);
                db.SaveChanges();
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
                db.Courses.Add(courses);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            try
            {
                Employee emp = db.Employees.Find(id);
                db.Employees.Remove(emp);
                db.SaveChanges();
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
                return db.Cities.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Course Methods
        public List<Courses> GetCourses()
        {
            return db.Courses.Include(x => x.EmployeeCourse).ToList();
        }
        public List<Courses> GetEmployeeCourses(long Id, bool link)
        {
            var courseData = db.Courses.ToList();
            var LinkingTableData = db.EmployeeCourse.Where(x => x.EmployeeId == Id).ToList();

            if (link)
            {
                return courseData.Where(p => !LinkingTableData.Any(p2 => p2.CourseId == p.CourseId)).ToList();
            }

            return courseData.Where(p => LinkingTableData.Any(p2 => p2.CourseId == p.CourseId)).ToList();
        }
        public int GetCoursesCount(long Id)
        {
            var counter = db.EmployeeCourse.Where(x => x.CourseId == Id).ToList().Count();
            return counter;
        }
        public double CountCredits(long Id)
        {
            double totCredits = 0;

            List<Courses> courses = db.Courses.ToList();
            List<EmployeeCourse> employeeCourses = db.EmployeeCourse.ToList();

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
                var course = db.Courses.Find(id);
                db.Entry(course).State = EntityState.Detached;
                return course;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCourse(Courses courses)
        {
            db.Entry(courses).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            try
            {
                Courses c = db.Courses.Find(id);
                db.Courses.Remove(c);
                db.SaveChanges();
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
                        db.EmployeeCourse.Add(item);
                    }
                    else
                    {
                        var ec = db.EmployeeCourse.Find(item.CourseId, employeeID);
                        db.EmployeeCourse.Remove(ec);
                    }
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}
