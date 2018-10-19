using FFImageLoading.Svg.Forms;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Drive.Client.Controls {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateEntry : ContentView {

        private static readonly string _APP_COMMON_DATE_FORMAT_RESOURCE_KEY = "FormattedDate";

        public static BindableProperty EntrySvgIconPathProperty = BindableProperty.Create(
            nameof(EntrySvgIconPath),
            typeof(string),
            typeof(DateEntry),
            defaultValue: default(string));

        public static BindableProperty EntryBackgroundColorProperty = BindableProperty.Create(
            nameof(EntryBackgroundColor),
            typeof(Color),
            typeof(DateEntry),
            defaultValue: default(Color));

        public static BindableProperty EntryHeighProperty = BindableProperty.Create(
            nameof(EntryHeigh),
            typeof(double),
            typeof(DateEntry),
            defaultValue: 60.0);

        public static BindableProperty EntryFontFamilyProperty = BindableProperty.Create(
            nameof(EntryFontFamily),
            typeof(string),
            typeof(DateEntry),
            defaultValue: default(string));

        public static BindableProperty EntryFontSizeProperty = BindableProperty.Create(
            nameof(EntryFontSize),
            typeof(double),
            typeof(DateEntry),
            defaultValue: 14.0);

        public static BindableProperty EntryTextColorProperty = BindableProperty.Create(
            nameof(EntryTextColor),
            typeof(Color),
            typeof(DateEntry),
            defaultValue: default(Color));

        public static BindableProperty EntryPlaceholderTextColorProperty = BindableProperty.Create(
            nameof(EntryPlaceholderTextColor),
            typeof(Color),
            typeof(DateEntry),
            defaultValue: default(Color));

        public static BindableProperty EntryPlaceholderProperty = BindableProperty.Create(
            nameof(EntryPlaceholder),
            typeof(string),
            typeof(DateEntry),
            defaultValue: default(string));

        public static BindableProperty EntryDateProperty = BindableProperty.Create(
            nameof(EntryDate),
            typeof(DateTime?),
            typeof(DateEntry),
            defaultBindingMode: BindingMode.TwoWay,
            defaultValue: default(DateTime?),
            propertyChanged: (BindableObject bindable, object oldValue, object newValue) => {
                if (bindable is DateEntry declarer) {
                    declarer.ResolvePlaceHolderAndValueVisibility();

                    declarer._datePicker_DatePickerExtended.Date = ((Nullable<DateTime>)newValue).HasValue ? ((Nullable<DateTime>)newValue).Value : declarer._datePicker_DatePickerExtended.Date;
                    declarer._dateOutput_LabelExtended.Text = string.Format(App.Current.Resources[_APP_COMMON_DATE_FORMAT_RESOURCE_KEY].ToString(), declarer._datePicker_DatePickerExtended.Date);
                }
            });

        public static BindableProperty EntryCalendarCultureProperty = BindableProperty.Create(
            nameof(EntryCalendarCulture),
            typeof(CultureInfo),
            typeof(DateEntry),
            defaultValue: default(CultureInfo));

        public event EventHandler Done = delegate { };

        public DateEntry() {
            InitializeComponent();

            _icon_SvgCachedImage.SetBinding(SvgCachedImage.SourceProperty, new Binding(nameof(EntrySvgIconPath), source: this));

            _mainSpot_Grid.SetBinding(Grid.BackgroundColorProperty, new Binding(nameof(EntryBackgroundColor), source: this));
            _mainSpot_Grid.SetBinding(Grid.HeightRequestProperty, new Binding(nameof(EntryHeigh), source: this));

            _datePicker_DatePickerExtended.SetBinding(DatePickerExtended.CalendarCultureProperty, new Binding(nameof(EntryCalendarCulture), source: this));
            _datePicker_DatePickerExtended.TranslationX = short.MaxValue;

            _dateOutput_LabelExtended.SetBinding(LabelExtended.FontFamilyProperty, new Binding(nameof(EntryFontFamily), source: this));
            _dateOutput_LabelExtended.SetBinding(LabelExtended.FontSizeProperty, new Binding(nameof(EntryFontSize), source: this));
            _dateOutput_LabelExtended.SetBinding(LabelExtended.TextColorProperty, new Binding(nameof(EntryTextColor), source: this));

            _placeholder_LabelExtended.SetBinding(LabelExtended.FontFamilyProperty, new Binding(nameof(EntryFontFamily), source: this));
            _placeholder_LabelExtended.SetBinding(LabelExtended.FontSizeProperty, new Binding(nameof(EntryFontSize), source: this));
            _placeholder_LabelExtended.SetBinding(LabelExtended.TextColorProperty, new Binding(nameof(EntryPlaceholderTextColor), source: this));
            _placeholder_LabelExtended.SetBinding(LabelExtended.TextProperty, new Binding(nameof(EntryPlaceholder), source: this));

            _datePicker_DatePickerExtended.DateSelected += OnDatePickerExtendedDateSelected;
            _datePicker_DatePickerExtended.Unfocused += TODO_TEST;

            ResolvePlaceHolderAndValueVisibility();
        }

        public string EntrySvgIconPath {
            get => (string)GetValue(EntrySvgIconPathProperty);
            set => SetValue(EntrySvgIconPathProperty, value);
        }

        public Color EntryBackgroundColor {
            get => (Color)GetValue(EntryBackgroundColorProperty);
            set => SetValue(EntryBackgroundColorProperty, value);
        }

        public double EntryHeigh {
            get => (double)GetValue(EntryHeighProperty);
            set => SetValue(EntryHeighProperty, value);
        }

        public string EntryFontFamily {
            get => (string)GetValue(EntryFontFamilyProperty);
            set => SetValue(EntryFontFamilyProperty, value);
        }

        public double EntryFontSize {
            get => (double)GetValue(EntryFontSizeProperty);
            set => SetValue(EntryFontSizeProperty, value);
        }

        public Color EntryTextColor {
            get => (Color)GetValue(EntryTextColorProperty);
            set => SetValue(EntryTextColorProperty, value);
        }

        public Color EntryPlaceholderTextColor {
            get => (Color)GetValue(EntryPlaceholderTextColorProperty);
            set => SetValue(EntryPlaceholderTextColorProperty, value);
        }

        public string EntryPlaceholder {
            get => (string)GetValue(EntryPlaceholderProperty);
            set => SetValue(EntryPlaceholderProperty, value);
        }

        public DateTime? EntryDate {
            get => (DateTime?)GetValue(EntryDateProperty);
            set => SetValue(EntryDateProperty, value);
        }

        public CultureInfo EntryCalendarCulture {
            get => (CultureInfo)GetValue(EntryCalendarCultureProperty);
            set => SetValue(EntryCalendarCultureProperty, value);
        }

        private void OnDatePickerExtendedDateSelected(object sender, DateChangedEventArgs e) => EntryDate = e.NewDate;

        private void OnMainSpotGridTapGestureRecognizerTapped(object sender, EventArgs e) => _datePicker_DatePickerExtended.Focus();

        private void ResolvePlaceHolderAndValueVisibility() {
            _dateOutput_LabelExtended.TranslationX = EntryDate.HasValue ? 0 : short.MaxValue;
            _placeholder_LabelExtended.TranslationX = EntryDate.HasValue ? short.MaxValue : 0;
        }

        private void TODO_TEST(object sender, FocusEventArgs e) {

        }
    }
}