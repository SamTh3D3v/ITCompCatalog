using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using ITCompCatalogue.ViewModel;
using Telerik.UI.Xaml.Controls.Input;
using Telerik.UI.Xaml.Controls.Input.Calendar;

namespace ITCompCatalogue.Converters
{
    public class CellModelToListEventConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var cellModel = value as CalendarCellModel;
            if (cellModel != null)
            {
                var calendar = cellModel.Presenter as RadCalendar;
                if (calendar != null)
                {
                    var scheduleViewModel = calendar.DataContext as ScheduleViewModel;
                    if (scheduleViewModel != null)
                    {
                        var listSchedules = scheduleViewModel.CoursesScheduleList;
                        var courseDateList = listSchedules.Where(e => e.DateDebut <= cellModel.Date && e.DateFin >= cellModel.Date).ToList();
                        return courseDateList;
                    }
                }
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
