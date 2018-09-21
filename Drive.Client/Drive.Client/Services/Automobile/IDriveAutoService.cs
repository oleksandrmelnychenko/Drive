using Drive.Client.Models.EntityModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drive.Client.Services.Automobile {
    public interface IDriveAutoService {
        Task<List<DriveAuto>> GetAllDriveAutosAsync(string value, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource));

        Task<List<DriveAuto>> GetDriveAutoByNumberAsync(string number, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource));

        Task<List<string>> GetCarNumbersAutocompleteAsync(string value, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource));
    }
}
