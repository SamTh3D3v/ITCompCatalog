using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeItcompService
{
    public interface IItcompService
    {
        List<CoursSchedule> GetCoursScheduleByCursusId(long cursusId);
        List<CoursSchedule> GetCoursScheduleByCoursId(long coursId);
    }
}
