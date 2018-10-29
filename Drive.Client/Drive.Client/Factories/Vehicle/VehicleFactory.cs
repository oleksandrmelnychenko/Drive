using Drive.Client.Helpers.Localize;
using Drive.Client.Models.DataItems.Vehicle;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Resources.Resx;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Drive.Client.Factories.Vehicle {
    class VehicleFactory : IVehicleFactory {
        public List<BaseRequestDataItem> BuildResidentRequestItems(IEnumerable<ResidentRequest> residentRequests) {
            List<BaseRequestDataItem> residentRequestDataItems = new List<BaseRequestDataItem>();

            foreach (var request in residentRequests) {
                ResidentRequestDataItem residentRequestDataItem = new ResidentRequestDataItem {
                    ResidentRequest = request,
                    Created = request.Created,
                    Status = GetLocalizeStatus(request.Status),
                    CountOutput = GetOutputValue(request.VehicleCount),
                    StatusColor = (request.Status == Status.Finished) ? (Color)App.Current.Resources["StatusFinishedColor"] : (Color)App.Current.Resources["StatusProcessingColor"]
                };
                residentRequestDataItem.InitializeAsync(null);
                residentRequestDataItems.Add(residentRequestDataItem);
            }

            return residentRequestDataItems;
        }

        public List<BaseRequestDataItem> BuildPolandRequestItems(IEnumerable<PolandVehicleRequest> residentRequests) {
            List<BaseRequestDataItem> residentRequestDataItems = new List<BaseRequestDataItem>();

            foreach (var request in residentRequests) {
                PolandRequestDataItem polandRequestDataItem = new PolandRequestDataItem {
                    PolandVehicleRequest = request,
                    Created = request.Created,
                    Status = GetPolandLocalizeStatus(request.IsParsed),
                    StatusColor = request.IsParsed ? (Color)App.Current.Resources["StatusFinishedColor"] : (Color)App.Current.Resources["ErrorColor"]
                };
                polandRequestDataItem.InitializeAsync(null);
                residentRequestDataItems.Add(polandRequestDataItem);
            }

            return residentRequestDataItems;
        }

        private StringResource GetPolandLocalizeStatus(bool isParsed) {
            return isParsed ? ResourceLoader.Instance.GetString(nameof(AppStrings.ExecutedUpperCase))
                            : ResourceLoader.Instance.GetString(nameof(AppStrings.ErrorUpperCase));
        }

        private StringResource GetLocalizeStatus(Status status) {
            StringResource resource = (status == Status.Finished) ? ResourceLoader.Instance.GetString(nameof(AppStrings.FinishedUpperCase))
                : ResourceLoader.Instance.GetString(nameof(AppStrings.ProcessingUpperCase));
            return resource;
        }

        private StringResource GetOutputValue(long count) {
            StringResource resource = count.Equals(0) || count > 4 ?
                ResourceLoader.Instance.GetString(nameof(AppStrings.VehiclesUpperCase))
                : count.Equals(1) ? ResourceLoader.Instance.GetString(nameof(AppStrings.VehicleUpperCase))
                : ResourceLoader.Instance.GetString(nameof(AppStrings.VehiclesSecondTypeUpperCase));
            return resource;
        }
    }
}
