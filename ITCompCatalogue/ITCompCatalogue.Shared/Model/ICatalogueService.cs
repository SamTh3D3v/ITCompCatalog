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
        Task<List<Cour>> SearchCourses(string searchText,String searchBy=null);
        void FavoriteCourse(long courseId,bool roamingFavorite);
        Cour GetCourseByCourseId(long courseId);
        Task<List<Cour>> GetCoursesByCursusId(long cursusId);
        void UnFavoriteCourse(long courseId, bool roamingFavorite);
        bool IsCourseFavorite(long courseId, bool roamingFavorite);
        void UnfavoriteAllCourses(bool roamingFavorite);
        Task<List<Cour>> GetFavoriteCourses(bool roamingFavorite);
        Task<List<CourDate>> GetCoursScheduleByCursusId(long cursusId);
        Task<List<CourDate>> GetCourseScheduleByCourseId(long coursId);
        Task<List<CourReview>> GetCourseReviewByCourseId(long courseId);
        Task AddCourseReviewByCourseId(CourReview courseReview);
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

    public class CourReview
    {
        public String Id { get; set; }
        public int CourId { get; set; }
        public String ReviewerName { get; set; }
        public String ReviewerEmail { get; set; }
        public String ReviewMessage { get; set; }
        public DateTime ReviewDate { get; set; }
        public int CourRanking  { get; set; }
        public String ReviewTitle  { get; set; }
    }
}
