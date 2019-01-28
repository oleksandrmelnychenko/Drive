using Drive.Client.ViewModels.Popups;

namespace Drive.Client.Models.DataItems {

    /// <summary>
    /// TODO: extend with `STRING SOURCE`
    /// </summary>
    public class CommonDataItem<TData> : IPopupSelectionItem {

        public string IconPath { get; set; }

        public string Titile { get; set; }

        public TData Data { get; set; }

        string IPopupSelectionItem.Title { get; set; }
    }
}
