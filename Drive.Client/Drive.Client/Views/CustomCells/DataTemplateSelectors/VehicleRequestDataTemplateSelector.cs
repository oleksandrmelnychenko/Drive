using Drive.Client.Models.DataItems.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Drive.Client.Views.CustomCells.DataTemplateSelectors {
    public class VehicleRequestDataTemplateSelector : DataTemplateSelector {

        private readonly DataTemplate _residentRequestDataTemplate;

        private readonly DataTemplate _polandRequestDataTemplate;

        private readonly DataTemplate _lithuaniaRequestDataTemplate;

        /// <summary>
        ///     ctor().
        /// </summary>
        public VehicleRequestDataTemplateSelector() {
            _residentRequestDataTemplate = new DataTemplate(typeof(ResidentRequestViewCell));
            _polandRequestDataTemplate = new DataTemplate(typeof(PolandRequestViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            BaseRequestDataItem baseRequestDataItem = item as BaseRequestDataItem;

            if (baseRequestDataItem == null) return null;

            return baseRequestDataItem is ResidentRequestDataItem ? _residentRequestDataTemplate : _polandRequestDataTemplate;
        }
    }
}
