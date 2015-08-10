﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Microsoft.WindowsAzure.MobileServices;
using SQLitePCL;

namespace ITCompCatalogue.Model
{
    public class CatalogueService : ICatalogueService
    {
        private readonly SQLiteConnection _connection;
        private readonly SQLiteConnection _roamingConnection;
        public CatalogueService()
        {
            _connection = new SQLiteConnection("ITCompTrainingDB.db");
            var favDbFilename = Path.Combine(ApplicationData.Current.RoamingFolder.Path, "ITCompFavoritesDB.db");
            _roamingConnection = new SQLiteConnection(favDbFilename);
        }
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
                        CourseCount = GetCoursesCount((long)statement[0]),
                        Categories = await GetCategoriesByTechnology((long)statement[0])

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
                        TechnologieID = (long)statement[3],
                        //Technology = GetTechnology(technologyId) 
                        //Cours = new ObservableCollection<Cour>(await GetCoursesByCategoryId((long)statement[0])),
                        Cursus = new ObservableCollection<Cursu>(await GetCursusByCategoryId((long)statement[0]))

                    });
                }
            }
            return categories;
        }
        public async Task<Category> GetCategoriesByCatgoryId(long categoryId)
        {
            var category = new Category();
            using (var statement = _connection.Prepare("SELECT * FROM Categories WHERE _id= ?"))
            {
                statement.Bind(1, categoryId);
                if (statement.Step() == SQLiteResult.ROW)
                {
                    category = new Category()
                    {
                        C_id = (long)statement[0],
                        Code = (string)statement[1],
                        Intitule = (string)statement[2],
                        TechnologieID = (long)statement[3],
                        //Technology = GetTechnology(technologyId) 
                        //Cours = new ObservableCollection<Cour>(await GetCoursesByCategoryId((long)statement[0])),
                        Cursus = new ObservableCollection<Cursu>(await GetCursusByCategoryId((long)statement[0]))

                    };
                }
            }
            return category;
        }

        public async Task<List<Cursu>> GetCursusByCategoryId(long categoryId)
        {
            var cursus = new List<Cursu>();
            using (var statement = _connection.Prepare("Select Distinct CursusID from Cours Inner Join CursusCours" +
                                                       " ON Cours._id==CursusCours.CourID where Cours.CategorieID= ?"))
            {
                statement.Bind(1, categoryId);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    cursus.Add(await GetCursusByCursusId((long)statement[0]));
                }
            }
            return cursus;
        }

        private async Task<Cursu> GetCursusByCursusId(long cursusId)
        {
            var cursus = new Cursu();
            using (var statement = _connection.Prepare("SELECT * FROM Cursus WHERE _id = ?"))
            {
                statement.Bind(1, cursusId);
                if (statement.Step() == SQLiteResult.ROW)
                {
                    cursus.C_id = (long)statement[0];
                    cursus.Code = (string)statement[1];
                    cursus.Intitule = (string)statement[2];
                    cursus.CursusCours =
                        new ObservableCollection<CursusCour>(await GetCursusCourByCursusId((long)statement[0]));

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

        public async Task<List<Cour>> GetCoursesByCursusId(long cursusId)
        {
            var courses = new List<Cour>();
            using (var statement = _connection.Prepare("SELECT * FROM CursusCours inner join Cours on Cours._id=CursusCours.CourID WHERE CursusID= ?"))
            {
                statement.Bind(1, cursusId);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    courses.Add(new Cour()
                    {
                        C_id = (long)statement[2],
                        Intitule = (string)statement[7],
                        CategorieID = (long)statement[12],
                        Category = GetCategoryByCategoryId((long)statement[12])
                    });
                }
            }
            return courses;
        }
        public Cour GetCourseByCourseId(long courseId)
        {
            var course = new Cour();
            using (var statement = _connection.Prepare("SELECT * FROM Cours WHERE _id = ?"))
            {
                statement.Bind(1, courseId);
                if (statement.Step() == SQLiteResult.ROW)
                {
                    course.C_id = (long)statement[0];
                    course.Code = (string)statement[1];
                    course.Intitule = (string)statement[2];
                    course.Duree = (string)statement[3];
                    course.Niveau = (string)statement[4];
                    course.Annee = (string)statement[5];
                    course.Description = (string)statement[6];
                    course.Category = GetCategoryByCategoryId((long)statement[7]);
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
        public async Task<List<Cour>> SearchCourses(string searchText, String searchBy = null)
        {
            var courses = new List<Cour>();
            var query = (searchBy != null) ? "SELECT * from Cours WHERE (lower(" + searchBy + ") LIKE '%" + searchText + "%')" :
                "SELECT * from Cours WHERE (code LIKE '%" + searchText + "%') or (intitule LIKE '%" + searchText + "%')";

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
        public void FavoriteCourse(long courseId, bool roamingFavorite)
        {
            if (!IsCourseFavorite(courseId,roamingFavorite))
            {
                using (var statement =(roamingFavorite? _roamingConnection:_connection).Prepare("INSERT INTO Favorite (_id) VALUES (@IdCourse);"))
                {
                    statement.Bind("@IdCourse", courseId);
                    statement.Step();
                }

            }
        }
        public void UnFavoriteCourse(long courseId, bool roamingFavorite)
        {
            using (var statement = (roamingFavorite ? _roamingConnection : _connection).Prepare("Delete from Favorite Where _id = ?"))
            {
                statement.Bind(1, courseId);
                statement.Step();
            }

        }
        public bool IsCourseFavorite(long courseId, bool roamingFavorite)
        {
            using (var statement = (roamingFavorite ? _roamingConnection : _connection).Prepare("SELECT * from Favorite Where _id = ?"))
            {
                statement.Bind(1, courseId);
                if (statement.Step() == SQLiteResult.ROW)
                {
                    return true;
                }
            }
            return false;
        }
        public void UnfavoriteAllCourses(bool roamingFavorite)
        {
            using (var statement = (roamingFavorite ? _roamingConnection : _connection).Prepare("Delete From Favorite"))
            {
                statement.Step();
            }
        }
        public async Task<List<Cour>> GetFavoriteCourses(bool roamingFavorite)
        {
            var courses = new List<Cour>();
            using (var statement = (roamingFavorite ? _roamingConnection : _connection).Prepare("SELECT * FROM Favorite"))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    courses.Add(GetCourseByCourseId((long)statement[0]));
                }
            }
            return courses;
        }
        public async Task<List<CourDate>> GetCoursScheduleByCursusId(long cursusId)
        {

            return new List<CourDate>()
            {
                // new CourDate(3, new DateTime(2015,01,5),new DateTime(2015,01,12),this),
                // new CourDate(7, new DateTime(2015,01,6),new DateTime(2015,01,18),this),
                // new CourDate(12, new DateTime(2015,01,6),new DateTime(2015,01,18),this),
                // new CourDate(3, new DateTime(2015,01,5),new DateTime(2015,01,12),this),
                // new CourDate(72, new DateTime(2015,01,6),new DateTime(2015,01,18),this),
                // new CourDate(32, new DateTime(2015,01,6),new DateTime(2015,01,18),this), 
                // new CourDate(33, new DateTime(2015,01,5),new DateTime(2015,01,12),this),
                // new CourDate(71, new DateTime(2015,01,6),new DateTime(2015,01,18),this),
                // new CourDate(12, new DateTime(2015,01,6),new DateTime(2015,01,18),this),
                //new CourDate(41, new DateTime(2015,01,15),new DateTime(2015,01,23),this),
                //new CourDate(5, new DateTime(2012,10,6),new DateTime(2012,11,10),this),
            };
        }

        public async Task<List<CourDate>> GetCoursScheduleByCoursId(long coursId)
        {
            var client = new MobileServiceClient("https://itcompdz.azure-mobile.net/",
                    "bObdidRoQhCjwMgwwfaTFOcqQLfvdL26");

            var listDates = await client.GetTable<CourDate>().Where(x => x.CoursId == coursId).ToListAsync();
            return listDates;
        }


    }
}
