using Drive.Client.ViewModels.Popups;

namespace Drive.Client.Models.EntityModels.Vehicle.Significance {
    public class EngineCapacity : IPopupSelectionItem {

        private double _capacity;
        public double Capacity {
            get => _capacity;
            set {
                _capacity = value;
                ((IPopupSelectionItem)this).Title = value.ToString("0.0");
            }
        }

        string IPopupSelectionItem.Title { get; set; }
    }
}
