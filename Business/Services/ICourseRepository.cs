using System.Collections.Generic;
using Business.Entities;

namespace Business.Services
{
    public interface ICourseRepository
    {
        void Add(Course course);
        void Commit();
        IList<Course> GetAll();
        Course GetById(int id);
    }
}