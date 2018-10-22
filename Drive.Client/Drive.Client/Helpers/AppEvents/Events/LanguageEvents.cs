using Drive.Client.Helpers.AppEvents.Events.Args;
using Drive.Client.Models.EntityModels.Search;
using System;

namespace Drive.Client.Helpers.AppEvents.Events {
    public class LanguageEvents {

        public event EventHandler<LanguageChangedArgs> LanguageChanged = delegate { };

        public event EventHandler<TestArgs> TestEve = delegate { };

        public LanguageEvents() {

        }

        ~LanguageEvents() {

        }

        public void OnTestEve(VehicleDetailsByResidentFullName vehicleDetailsByResidentFullName) => TestEve(this, new TestArgs { VehicleDetailsByResidentFullName = vehicleDetailsByResidentFullName });

        public void OnLanguageChanged(string languageId) => LanguageChanged(this, new LanguageChangedArgs { LanguageId = languageId });
    }
}
