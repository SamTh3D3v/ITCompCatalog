using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeItcompService
{
    public class ItcompService:IItcompService
    {
        public List<CoursSchedule> GetCoursScheduleByCursusId(long cursusId)
        {
            return new List<CoursSchedule>()
            {
                new CoursSchedule(3, new DateTime(2013,11,5),new DateTime(2013,12,5)),
                new CoursSchedule(4, new DateTime(2013,07,15),new DateTime(2013,07,25)),
                new CoursSchedule(5, new DateTime(2012,10,5),new DateTime(2012,11,10)),
            };
        }

        public List<CoursSchedule> GetCoursScheduleByCoursId(long coursId)
        {
            return new List<CoursSchedule>()
            {
                new CoursSchedule(4, new DateTime(2013,07,15),new DateTime(2013,07,25)),
                new CoursSchedule(4, new DateTime(2012,10,5),new DateTime(2012,11,10)),
            };
        }
    }

    public class CoursSchedule
    {
        public long CoursId { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        public CoursSchedule(long coursId,DateTime dateDebut,DateTime dateFin)
        {
            CoursId = coursId;
            DateDebut = dateDebut;
            DateFin = dateFin;
        }
    }
}
