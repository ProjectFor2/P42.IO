using System;
using System.Activities.Presentation.Model;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace P42.IO.Helper
{
    public class ActivityIconConverterHelper : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }

                var activityType = (value as ModelItem).ItemType;
                var resourceName = activityType.Name;

                if (activityType.IsGenericType)
                {
                    resourceName = resourceName.Split('`')[0];
                }

                resourceName += "Icon";

                var iconsSource = new ResourceDictionary { Source = new Uri(parameter as string) };
                var icon = iconsSource[resourceName] as DrawingBrush;

                if (icon == null && icon is DrawingBrush)
                {
                    icon = Application.Current.Resources[resourceName] as DrawingBrush;
                }

                if (icon == null && icon is DrawingBrush)
                {
                    icon = Application.Current.Resources["GenericLeafActivityIcon"] as DrawingBrush;
                }

                return icon?.Drawing ?? null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}