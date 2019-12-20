using BlazorWithSecutiry.DataAccess;
using BlazorWithSecutiry.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithSecutiry.Service
{
    public class CourseService
    {
        EmployeeDataAccessLayer objemployee = new EmployeeDataAccessLayer();

        public Task<List<Courses>> GetCourses()
        {
            IEnumerable<Courses> courses = objemployee.GetCourses();
            return Task.FromResult(courses.ToList());
        }

        public Task<Courses> CourseDetails(int id)
        {
            return Task.FromResult(objemployee.GetCoursesDetails(id));
        }

        public void EditCourse(Courses courses)
        {
            objemployee.UpdateCourse(courses);
        }

        public void DeleteCourse(int id)
        {
            objemployee.DeleteCourse(id);
        }

        public void CreateCourse(Courses courses)
        {
            objemployee.AddCourse(courses);
        }

        public int CountCourse(long Id)
        {
            return objemployee.GetCoursesCount(Id);
        }

        public double CountCredits(long Id)
        {
            return objemployee.CountCredits(Id);
        }

        public Task<List<Courses>> GetEmployeeCourses(long employeeID,bool link)
        {
            IEnumerable<Courses> courses = objemployee.GetEmployeeCourses(employeeID, link);
            return Task.FromResult(courses.ToList());
        }

        public void LinkCourses(long employeeID, List<string> EmployeeCourses,bool LinkAction)
        {
            objemployee.LinkCourses(employeeID, EmployeeCourses, LinkAction);
        }
    }
}
