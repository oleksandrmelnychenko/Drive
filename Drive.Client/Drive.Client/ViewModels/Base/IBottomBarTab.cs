using System;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.Base {
    public interface IBottomBarTab {
        bool HasBackgroundItem { get; }

        bool IsBudgeVisible { get; }

        int BudgeCount { get; }

        string TabHeader { get; }

        string TabIcon { get; }

        Type RelativeViewType { get; }

        void Dispose();

        Task InitializeAsync(object navigationData);
    }
}
