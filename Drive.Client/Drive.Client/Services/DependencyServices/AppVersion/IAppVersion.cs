namespace Drive.Client.Services.DependencyServices.AppVersion {
    public interface IAppVersion {
        string GetVersion();

        string GetBuild();
    }
}
