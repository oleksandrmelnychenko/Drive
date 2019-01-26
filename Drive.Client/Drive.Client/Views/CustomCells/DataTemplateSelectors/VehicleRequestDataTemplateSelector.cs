using Drive.Client.Models.DataItems.Vehicle;
using Drive.Client.Models.EntityModels.Search;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Drive.Client.Views.CustomCells.DataTemplateSelectors {
    public class VehicleRequestDataTemplateSelector : DataTemplateSelector {

        private readonly DataTemplate _residentRequestDataTemplate;

        private readonly DataTemplate _polandRequestDataTemplate;

        private readonly DataTemplate _cogitiveRequestDataTemplate;

        /// <summary>
        ///     ctor().
        /// </summary>
        public VehicleRequestDataTemplateSelector() {
            _residentRequestDataTemplate = new DataTemplate(typeof(ResidentRequestViewCell));
            _polandRequestDataTemplate = new DataTemplate(typeof(PolandRequestViewCell));
            _cogitiveRequestDataTemplate = new DataTemplate(typeof(CognitiveRequestViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {

            if (!(item is BaseRequestDataItem baseRequestDataItem)) return null;

            switch (baseRequestDataItem.HistoryType) {
                case RequestType.ResidentVehicle:
                    return _residentRequestDataTemplate;
                case RequestType.CognitiveImageData:
                    return _cogitiveRequestDataTemplate;
                case RequestType.PolandVehicle:
                    return _polandRequestDataTemplate;
                default:
                    return null;
            }
        }
    }
}
