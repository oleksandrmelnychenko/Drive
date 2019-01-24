using Drive.Client.ViewModels.Popups;

namespace Drive.Client.Models.EntityModels.Vehicle.Significance {
    public class VehicleType : IPopupSelectionItem {

        private string _type;
        public string Type {
            get => _type;
            set {
                _type = value;
                ((IPopupSelectionItem)this).Title = value.ToString();
            }
        }

        string IPopupSelectionItem.Title { get; set; }
    }
}
