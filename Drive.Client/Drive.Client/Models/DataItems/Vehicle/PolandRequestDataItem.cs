using Drive.Client.Helpers.Localize;
using Drive.Client.Models.EntityModels.Search;
using System.Drawing;

namespace Drive.Client.Models.DataItems.Vehicle {
    public class PolandRequestDataItem : BaseRequestDataItem {

        PolandVehicleRequest _polandVehicleRequest;
        public PolandVehicleRequest PolandVehicleRequest {
            get { return _polandVehicleRequest; }
            set { SetProperty(ref _polandVehicleRequest, value); }
        }

        StringResource _status;
        public StringResource Status {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        Color _statusColor;
        public Color StatusColor {
            get { return _statusColor; }
            set { SetProperty(ref _statusColor, value); }
        }
    }
}
