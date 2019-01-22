using Drive.Client.Models.EntityModels.Cognitive;
using Drive.Client.Models.EntityModels.Search;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Automobile {
    public interface IDriveAutoService {
        Task<List<DriveAuto>> GetAllDriveAutosAsync(string value, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<DriveAuto>> GetDriveAutoByNumberAsync(string number, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<DriveAutoSearch>> GetCarNumbersAutocompleteAsync(string value, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<DriveAuto>> SearchDriveAutoByCognitiveAsync(FormDataContent formDataContent, CancellationToken cancellationToken = default(CancellationToken));
    }
}
