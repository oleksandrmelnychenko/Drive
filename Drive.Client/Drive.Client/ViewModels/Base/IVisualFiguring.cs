using Drive.Client.Helpers.Localize;
using System;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.Base {
    public interface IVisualFiguring {

        StringResource TabHeader { get; }

        Type RelativeViewType { get; }

        void Dispose();

        Task InitializeAsync(object navigationData);
    }
}
