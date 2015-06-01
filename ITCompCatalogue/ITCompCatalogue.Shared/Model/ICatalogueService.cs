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
        Task<List<CourDate>> GetCoursScheduleByCursusId(long cursusId);
        Task<List<CourDate>> GetCoursScheduleByCoursId(long coursId);
        Task<List<Cursu>> GetCursusByCategoryId(long categoryId);
        Task<Category> GetCategoriesByCatgoryId(long categoryId);        
    }
    public class CourDate
    {        
        public string Id { get; set; }
        public int CoursId { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }        

        public CourDate(int coursId, DateTime dateDebut, DateTime dateFin)
        {
            CoursId = coursId;
            DateDebut = dateDebut;
            DateFin = dateFin;
            //Cour = catalogueService.GetCourseByCourseId(coursId);
        }
    }
}
