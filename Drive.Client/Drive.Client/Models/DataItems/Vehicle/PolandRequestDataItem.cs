using Drive.Client.Helpers.Localize;
using Drive.Client.Models.EntityModels.Search;

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
    }
}
