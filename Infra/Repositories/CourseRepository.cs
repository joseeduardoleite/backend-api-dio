using System.Collections.Generic;
using System.Linq;
using Business.Entities;
using Business.Services;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Course course) => _context.Courses.Add(course);

        public void Commit() => _context.SaveChanges();

        public IList<Course> GetAll() => _context.Courses.ToList();

        public Course GetById(int id) => _context.Courses.Include(x => x.User).FirstOrDefault(x => x.Id == id);
    }
}