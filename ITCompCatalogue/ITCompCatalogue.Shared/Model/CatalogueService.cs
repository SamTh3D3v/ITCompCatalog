using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using SQLitePCL;

namespace ITCompCatalogue.Model
{
    class CatalogueService:ICatalogueService
    {
        private readonly SQLiteConnection _connection = new SQLiteConnection("ITCompTrainingDB.db");        
        public async Task<List<Technology>> GetAllTechnologies()
        {
            var technologies = new List<Technology>();
            using (var statement=_connection.Prepare("SELECT * FROM Technologies"))
            {
                while (statement.Step()==SQLiteResult.ROW)
                {
                    technologies.Add(new Technology()
                    {
                        C_id = (long)statement[0],
                        Code = (string)statement[1],
                        Intitule = (string)statement[2],
                        CourseCount=
                    });
                }
            }
            return technologies;
        }
      

        public async Task<List<Category>> GetCategoriesByTechnology(long technologyId)
        {
            var categories = new List<Category>();
            using (var statement = _connection.Prepare("SELECT * FROM Categories WHERE TechnologieID= ?"))
            {
                statement.Bind(1,technologyId);
                while (statement.Step()==SQLiteResult.ROW)
                {
                    categories.Add(new Category()
                    {
                        C_id = (long)statement[0],
                        Code = (string) statement[1],
                        Intitule = (string) statement[2],
                        //Technology = GetTechnology(technologyId) 
                        Cours =new ObservableCollection<Cour>(await GetCoursesByCategoryId((long)statement[0]))
                    });
                }
            }
            return categories;
        }

        private Technology GetTechnology(long technologyId)
        {
            var technology = new Technology();
            using (var statement = _connection.Prepare("SELECT * FROM Technology WHERE TechnologieID = ?"))
            {
                statement.Bind(1, technologyId);
                if (statement.Step() == SQLiteResult.ROW)
                {
                    technology.C_id = (long) statement[0];
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
                statement.Bind(1,categoryId);
                while (statement.Step()==SQLiteResult.ROW)
                {
                    courses.Add(new Cour()
                    {
                        C_id = (long)statement[0],
                        Code =(string) statement[1] ,
                        Intitule =(string) statement[2] ,
                        Duree = (string)statement[3],
                        Niveau = (string)statement[4],
                        Annee = (string)statement[5],
                        Description = (string)statement[6],
                        Category = GetCategory((long)statement[7])
                    });
                }
            }
            return new ObservableCollection<Cour>(courses);
        }
        private Category GetCategory(long categoryId)
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

        private Cursu GetCursus(long cursusId)
        {
            var cursus = new Cursu();
            using (var statement=_connection.Prepare("SELECT * FROM Cursus WHERE C_id = ?"))
            {
                statement.Bind(1,cursusId);
                if (statement.Step()==SQLiteResult.ROW)
                {
                    cursus.C_id = (long) statement[0];
                    cursus.Code = (string) statement[1];
                    cursus.Intitule = (string) statement[2];
                }

            }
            return cursus;
        }

        public async Task<List<Cour>> GetAllCourses()
        {

            var cours = new List<Cour>();

            using (var statement = _connection.Prepare("SELECT * FROM Cour"))
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
       
    }
}
