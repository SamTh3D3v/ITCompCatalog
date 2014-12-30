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
        void UnFavoriteCourse(long courseId);
        bool IsCourseFavorite(long courseId);
        void UnfavoriteAllCourses();
        Task<List<Cour>> GetFavoriteCourses();

    }
}
