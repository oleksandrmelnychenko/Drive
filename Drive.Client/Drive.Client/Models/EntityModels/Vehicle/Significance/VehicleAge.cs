using Drive.Client.ViewModels.Popups;

namespace Drive.Client.Models.EntityModels.Vehicle.Significance {
    public class VehicleAge : IPopupSelectionItem {

        private uint _age;
        public uint Age {
            get => _age;
            set {
                _age = value;
                ((IPopupSelectionItem)this).Title = value.ToString();
            }
        }

        string IPopupSelectionItem.Title { get; set; }
    }
}
