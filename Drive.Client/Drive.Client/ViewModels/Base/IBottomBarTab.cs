using System;
using System.Threading.Tasks;

namespace Drive.Client.ViewModels.Base {
    public interface IBottomBarTab : IVisualFiguring {

        bool HasBackgroundItem { get; }

        bool IsBudgeVisible { get; }

        int BudgeCount { get; }

        string TabIcon { get; }

        Type BottomTasselViewType { get; }
    }
}
