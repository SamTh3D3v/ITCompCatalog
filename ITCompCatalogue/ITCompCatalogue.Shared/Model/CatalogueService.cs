using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using SQLitePCL;

namespace ITCompCatalogue.Model
{
    class CatalogueService : ICatalogueService
    {
        private readonly SQLiteConnection _connection = new SQLiteConnection("ITCompTrainingDB.db");
        public async Task<List<Technology>> GetAllTechnologies()
        {
            var technologies = new List<Technology>();
            using (var statement = _connection.Prepare("SELECT * FROM Technologies"))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    technologies.Add(new Technology()
                    {
                        C_id = (long)statement[0],
                        Code = (string)statement[1],
                        Intitule = (string)statement[2],
                        CourseCount = GetCoursesCount((long)statement[0])
                    });
                }
            }
            return technologies;
        }
        public long GetCoursesCount(long technologyId)
        {
            long count = 0;
            using (var statement = _connection.Prepare("Select Count(*) from Cours join" +
                                                       " Categories where Cours.CategorieID=Categories._id and Categories.TechnologieID = ?"))
            {

                statement.Bind(1, technologyId);
                if (statement.Step() == SQLiteResult.ROW)
                {
                    count = (long)statement[0];
                }
            }
            return count;

        }

        public async Task<List<Category>> GetCategoriesByTechnology(long technologyId)
        {
            var categories = new List<Category>();
            using (var statement = _connection.Prepare("SELECT * FROM Categories WHERE TechnologieID= ?"))
            {
                statement.Bind(1, technologyId);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    categories.Add(new Category()
                    {
                        C_id = (long)statement[0],
                        Code = (string)statement[1],
                        Intitule = (string)statement[2],
                        //Technology = GetTechnology(technologyId) 
                        //Cours = new ObservableCollection<Cour>(await GetCoursesByCategoryId((long)statement[0])),
                        Cursus = new ObservableCollection<Cursu>(await GetCursusByCategoryId((long)statement[0]))

                    });
                }
            }
            return categories;
        }

        private async Task<List<Cursu>> GetCursusByCategoryId(long categoryId)
        {
            var cursus = new List<Cursu>();
            using (var statement = _connection.Prepare("Select Distinct CursusID from Cours Inner Join CursusCours" +
                                                       " ON Cours._id==CursusCours.CourID where Cours.CategorieID= ?"))
            {
                statement.Bind(1, categoryId);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    cursus.Add( await GetCursusByCursusId((long)statement[0]));
                }
            }
            return cursus;
        }

        private async Task<Cursu> GetCursusByCursusId(long cursusId)
        {
            var cursus = new Cursu();
            using (var statement=_connection.Prepare("SELECT * FROM Cursus WHERE _id = ?"))
            {
                statement.Bind(1,cursusId);
                if (statement.Step()==SQLiteResult.ROW)
                {
                    cursus.C_id = (long) statement[0];
                    cursus.Code = (string) statement[1];
                    cursus.Intitule = (string) statement[2];
                    cursus.CursusCours =
                        new ObservableCollection<CursusCour>(await GetCursusCourByCursusId((long) statement[0]));

                }

            }
            return cursus;
        }
        private async Task<List<CursusCour>> GetCursusCourByCursusId(long cursusId)
        {
            var cursusCours = new List<CursusCour>();

            using (var statement = _connection.Prepare("SELECT * FROM CursusCours WHERE CursusID = ?"))
            {
                statement.Bind(1, cursusId);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    cursusCours.Add(new CursusCour()
                    {
                        C_id = (long)statement[0],
                        CursusID = (long)statement[1],
                        CourID = (long)statement[2],
                        Ordre = (long)statement[3],
                        Recommandation = (string)statement[4],
                        Cour = GetCourseByCourseId((long)statement[2])
                       
                    });
                }
            }
            return cursusCours;

        }

        private Cour GetCourseByCourseId(long courseId)
        {
            var course = new Cour();
            using (var statement = _connection.Prepare("SELECT * FROM Cours WHERE _id = ?"))
            {
                statement.Bind(1, courseId);
                if (statement.Step() == SQLiteResult.ROW)
                {
                    course.C_id = (long) statement[0];
                    course.Code = (string) statement[1];
                    course.Intitule = (string) statement[2];
                    course.Duree = (string) statement[3];
                    course.Niveau = (string) statement[4];
                    course.Annee = (string) statement[5];
                    course.Description = (string) statement[6];
                    course.Category = GetCategoryByCategoryId((long) statement[7]);
                }
            }
            return course;
        }

        private Technology GetTechnology(long technologyId)
        {
            var technology = new Technology();
            using (var statement = _connection.Prepare("SELECT * FROM Technology WHERE TechnologieID = ?"))
            {
                statement.Bind(1, technologyId);
                if (statement.Step() == SQLiteResult.ROW)
                {
                    technology.C_id = (long)statement[0];
                    technology.Code = (string)statement[1];
                    technology.Intitule = (string)statement[2];
                }
            }
            return technology;
        }

        private async Task<ObservableCollection<Cour>> GetCoursesByCategoryId(long categoryId)
        {
            var courses = new List<Cour>();
            using (var statement = _connection.Prepare("SELECT * FROM Cours WHERE CategorieID= ?"))
            {
                statement.Bind(1, categoryId);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    courses.Add(new Cour()
                    {
                        C_id = (long)statement[0],
                        Code = (string)statement[1],
                        Intitule = (string)statement[2],
                        Duree = (string)statement[3],
                        Niveau = (string)statement[4],
                        Annee = (string)statement[5],
                        Description = (string)statement[6],
                        Category = GetCategoryByCategoryId((long)statement[7])
                    });
                }
            }
            return new ObservableCollection<Cour>(courses);
        }
        private Category GetCategoryByCategoryId(long categoryId)
        {
            var category = new Category();
            using (var statement = _connection.Prepare("SELECT * FROM Categories WHERE _id = ?"))
            {
                statement.Bind(1, categoryId);
                if (statement.Step() == SQLiteResult.ROW)
                {
                    category.C_id = (long)statement[0];
                    category.Code = (string)statement[1];
                    category.Intitule = (string)statement[2];
                    category.TechnologieID = (long)statement[3];
                }
            }
            return category;
        }

      

        public async Task<List<Cour>> GetAllCourses()
        {

            var cours = new List<Cour>();

            using (var statement = _connection.Prepare("SELECT * FROM Cours"))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    cours.Add(new Cour()
                    {
                        C_id = (long)statement[0],
                        Code = (string)statement[1],
                        Intitule = (string)statement[2],
                        Duree = (string)statement[3],
                        Niveau = (string)statement[4],
                        Annee = (string)statement[5],
                        Description = (string)statement[6],
                        CategorieID = (long)statement[7]
                    });
                }
            }
            return cours;

        }

        public async Task<List<Cour>> SearchCourses(string searchText, String searchBy)
        {
            var courses = new List<Cour>();
            var query = "SELECT * from Cours WHERE (lower(" + searchBy + ") LIKE '%" + searchText + "%')";
            using (var statement = _connection.Prepare(query))
            {
                //            statement.Bind(1, searchText);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    courses.Add(new Cour()
                    {
                        C_id = (long)statement[0],
                        Code = (string)statement[1],
                        Intitule = (string)statement[2],
                        Duree = (string)statement[3],
                        Niveau = (string)statement[4],
                        Annee = (string)statement[5],
                        Description = (string)statement[6],
                        CategorieID = (long)statement[7],
                        Category = GetCategoryByCategoryId((long)statement[7])
                    });
                }
            }
            return courses;
        }
    }
}
