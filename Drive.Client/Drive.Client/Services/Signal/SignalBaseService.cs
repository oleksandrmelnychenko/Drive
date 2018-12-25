using Microsoft.AppCenter.Crashes;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Drive.Client.Services.Signal {
    public abstract class SignalBaseService {

        private static readonly int _DEFAULT_NUMBER_OF_CONNECTION_ATTEMPTS = 9;
        private static readonly string _HUB_CONNECTION_ERROR = "{0} cant connect to the appropriate hub.";

        protected HubConnection _hubConnection;

        private int _connectionAttempts;

        public SignalBaseService() {
            CrossConnectivity.Current.ConnectivityChanged += OnCurrentConnectivityChanged;
        }

        public abstract string SocketHubGateway { get; protected set; }

        public string AccessToken { get; private set; }

        public bool IsConnected { get; private set; }

        public int MaxNumberOfConnectionAttempts { get; protected set; } = _DEFAULT_NUMBER_OF_CONNECTION_ATTEMPTS;

        public Task StartAsync(string accessToken) =>
            Task.Run(() => {
                try {
                    if (_hubConnection == null) {
                        AccessToken = accessToken;

                        _hubConnection = new HubConnectionBuilder()
                            .WithUrl(SocketHubGateway, (options) => {
                                if (!string.IsNullOrEmpty(AccessToken)) {
                                    options.AccessTokenProvider = () => Task.Run(() => AccessToken);
                                }
                            })
                            .ConfigureLogging((logging) => {
                                logging.AddProvider(new InfallibleLogProvider());
                            })
                            .Build();
                        _hubConnection.Closed += OnHubConnectionClosed;

                        OnStartListeningToHub();
                    }

                    TryToConnectToHub();
                }
                catch (Exception exc) {
                    _hubConnection = null;
                    AccessToken = null;
                    IsConnected = false;

                    Crashes.TrackError(exc);
                    Debugger.Break();
                }
            });

        public Task StopAsync() =>
            Task.Run(async () => {
                try {
                    if (_hubConnection != null) {
                        _hubConnection.Closed -= OnHubConnectionClosed;
                        await _hubConnection.StopAsync();
                        await _hubConnection.DisposeAsync();
                        _hubConnection = null;

                        AccessToken = null;
                    }
                }
                catch (Exception exc) {
                    _hubConnection = null;
                    AccessToken = null;
                    IsConnected = false;

                    Crashes.TrackError(exc);
                    Debugger.Break();
                }
            });

        protected abstract void OnStartListeningToHub();

        protected TResult ParseResponseData<TResult>(object data) {
            try {
                TResult result = JsonConvert.DeserializeObject<TResult>(data.ToString());

                return result;
            }
            catch (Exception exc) {
                Debugger.Break();
                Crashes.TrackError(exc);

                throw exc;
            }
        }

        private Task OnHubConnectionClosed(Exception arg) =>
            Task.Run(() => {
                TryToConnectToHub();
            });

        private async void TryToConnectToHub() {
            try {
                //await Task.Delay(1002);

                await _hubConnection.StartAsync();

                IsConnected = true;
            }
            catch (Exception exc) {
                string message = exc.Message;
                _connectionAttempts++;
                IsConnected = false;
            }

            _connectionAttempts = 0;
        }

        private void OnCurrentConnectivityChanged(object sender, ConnectivityChangedEventArgs e) {
            if (e.IsConnected && (!IsConnected)) {
                TryToConnectToHub();
            }
        }
    }
}
