using Drive.Client.Helpers.Localize;
using Drive.Client.Models.DataItems.Vehicle;
using Drive.Client.Models.EntityModels.Search;
using Drive.Client.Resources.Resx;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Drive.Client.Factories.Vehicle {
    public class VehicleFactory : IVehicleFactory {

        public List<BaseRequestDataItem> BuildResidentRequestItems(IEnumerable<ResidentRequest> residentRequests, ResourceLoader resourceLoader) {
            List<BaseRequestDataItem> residentRequestDataItems = new List<BaseRequestDataItem>();

            foreach (var request in residentRequests) {
                ResidentRequestDataItem residentRequestDataItem = new ResidentRequestDataItem {
                    ResidentRequest = request,
                    HistoryType = request.HistoryType,
                    Created = request.Created,
                    Status = GetLocalizeStatus(request.Status, resourceLoader),
                    CountOutput = GetOutputValue(request.VehicleCount, resourceLoader),
                    StatusColor = (request.Status == Status.Finished) ? (Color)App.Current.Resources["StatusFinishedColor"] : (Color)App.Current.Resources["StatusProcessingColor"]
                };
                residentRequestDataItem.InitializeAsync(null);
                residentRequestDataItems.Add(residentRequestDataItem);
            }

            return residentRequestDataItems;
        }

        public List<BaseRequestDataItem> BuildPolandRequestItems(IEnumerable<PolandVehicleRequest> residentRequests, ResourceLoader resourceLoader) {
            List<BaseRequestDataItem> residentRequestDataItems = new List<BaseRequestDataItem>();

            foreach (var request in residentRequests) {
                PolandRequestDataItem polandRequestDataItem = new PolandRequestDataItem {
                    PolandVehicleRequest = request,
                    HistoryType = request.HistoryType,
                    Created = request.Created,
                    Status = GetPolandLocalizeStatus(request.IsParsed, resourceLoader),
                    StatusColor = request.IsParsed ? (Color)App.Current.Resources["StatusFinishedColor"] : (Color)App.Current.Resources["ErrorColor"]
                };
                polandRequestDataItem.InitializeAsync(null);
                residentRequestDataItems.Add(polandRequestDataItem);
            }

            return residentRequestDataItems;
        }

        public List<BaseRequestDataItem> BuildCognitiveRequestItems(IEnumerable<CognitiveRequest> cognitiveRequests, ResourceLoader resourceLoader) {
            List<BaseRequestDataItem> residentRequestDataItems = new List<BaseRequestDataItem>();

            foreach (var request in cognitiveRequests) {
                CognitiveRequestDataItem cognitiveRequestDataItem = new CognitiveRequestDataItem {
                    CognitiveRequest = request,
                    HistoryType = request.HistoryType,
                    Created = request.Created,
                };
                cognitiveRequestDataItem.InitializeAsync(null);
                residentRequestDataItems.Add(cognitiveRequestDataItem);
            }

            return residentRequestDataItems;
        }

        private StringResource GetPolandLocalizeStatus(bool isParsed, ResourceLoader resourceLoader) {
            return isParsed ? resourceLoader.GetString(nameof(AppStrings.ExecutedUpperCase))
                            : resourceLoader.GetString(nameof(AppStrings.ErrorUpperCase));
        }

        private StringResource GetLocalizeStatus(Status status, ResourceLoader resourceLoader) {
            StringResource resource = (status == Status.Finished) 
                ? resourceLoader.GetString(nameof(AppStrings.FinishedUpperCase))
                : resourceLoader.GetString(nameof(AppStrings.ProcessingUpperCase));
            return resource;
        }

        private StringResource GetOutputValue(long count, ResourceLoader resourceLoader) {
            StringResource resource = count.Equals(0) || count > 4 
                ? resourceLoader.GetString(nameof(AppStrings.VehiclesUpperCase))
                : count.Equals(1) 
                    ? resourceLoader.GetString(nameof(AppStrings.VehicleUpperCase))
                    : resourceLoader.GetString(nameof(AppStrings.VehiclesSecondTypeUpperCase));
            return resource;
        }

    }
}
