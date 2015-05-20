using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITCompCatalogue.Model
{
    public interface ICatalogueService
    {
        Task<List<Technology>> GetAllTechnologies();
        Task<List<Category>> GetCategoriesByTechnology(long technologyId);
        Task<List<Cour>> GetAllCourses();
        Task<List<Cour>> SearchCourses(string searchText,String searchBy);
        void FavoriteCourse(long courseId);
        Cour GetCourseByCourseId(long courseId);
        Task<List<Cour>> GetCoursesByCursusId(long cursusId);
        void UnFavoriteCourse(long courseId);
        bool IsCourseFavorite(long courseId);
        void UnfavoriteAllCourses();
        Task<List<Cour>> GetFavoriteCourses();
        Task<List<CoursSchedule>> GetCoursScheduleByCursusId(long cursusId);
        Task<List<CoursSchedule>> GetCoursScheduleByCoursId(long coursId);        
    }
    public class CoursSchedule
    {
        public long CoursId { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public Cour Cour { get; set; }

        public CoursSchedule(long coursId, DateTime dateDebut, DateTime dateFin,CatalogueService catalogueService)
        {
            CoursId = coursId;
            DateDebut = dateDebut;
            DateFin = dateFin;
            Cour = catalogueService.GetCourseByCourseId(coursId);
        }
    }
}
