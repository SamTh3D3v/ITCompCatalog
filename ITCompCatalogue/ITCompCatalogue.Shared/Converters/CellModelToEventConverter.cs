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
    public class CellModelToEventConverter : IValueConverter
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
                        var courseDate = listSchedules.FirstOrDefault(e => e.DateDebut == cellModel.Date);
                        if (courseDate != null)
                        {
                            return cellModel.Label;
                        }
                    }
                }
            }            
            return cellModel.Label;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
