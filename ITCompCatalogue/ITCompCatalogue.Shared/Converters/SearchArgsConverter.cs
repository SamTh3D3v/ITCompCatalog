using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Search;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace ITCompCatalogue.Converters
{
    public sealed class SearchArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = (SearchBoxSuggestionsRequestedEventArgs)value;
            var displayHistory = (bool)parameter;

            if (args == null) return value;
            ISuggestionQuery item = new SuggestionQuery(args.Request, args.QueryText)
            {
                DisplayHistory = displayHistory
            };
            return item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
    public interface ISuggestionQuery
    {
        SearchSuggestionsRequest Request { get; }
        string QueryText { get; }
        bool DisplayHistory { get; set; }
    }
    public class SuggestionQuery : ISuggestionQuery
    {
        public SuggestionQuery(SearchSuggestionsRequest request, string queryText)
        {
            Request = request;
            QueryText = queryText;
        }

        public SearchSuggestionsRequest Request { get; private set; }
        public string QueryText { get; private set; }
        public bool DisplayHistory { get; set; }
    }
}
