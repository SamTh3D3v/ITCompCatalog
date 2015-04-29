using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using ITCompCatalogue.ViewModel;
using Telerik.UI.Xaml.Controls.Input;
using Telerik.UI.Xaml.Controls.Input.Calendar;

namespace ITCompCatalogue.Helper
{
    public class CustomStyleSelector : CalendarCellStyleSelector
    {
        public DataTemplate EventTemplate { get; set; }       
        protected override void SelectStyleCore(CalendarCellStyleContext context, Telerik.UI.Xaml.Controls.Input.RadCalendar container)
        {
            var scheduleViewModel = container.DataContext as ScheduleViewModel;
            if (scheduleViewModel != null)
            {
                var events = scheduleViewModel.CoursesScheduleList;
                switch (scheduleViewModel.DisplayMode)
                {
                    case CalendarDisplayMode.MonthView:

                        if (events.Any(e => e.DateDebut <= context.Date && e.DateFin >= context.Date))
                        {
                            context.CellTemplate = this.EventTemplate;
                        }
                        break;
                    case CalendarDisplayMode.YearView:
                        if (events.Any(e => (e.DateDebut.Month == context.Date.Month && e.DateDebut.Year == context.Date.Year) || (e.DateFin.Month == context.Date.Month && e.DateFin.Year == context.Date.Year)))
                        {
                            context.CellTemplate = this.EventTemplate;
                        }
                        break;
                    default:
                        if (events.Any(e => e.DateDebut.Year == context.Date.Year || e.DateFin.Year == context.Date.Year))
                        {
                            context.CellTemplate = this.EventTemplate;
                        }
                        break;
                }                                              
            }
        }
    }
}
