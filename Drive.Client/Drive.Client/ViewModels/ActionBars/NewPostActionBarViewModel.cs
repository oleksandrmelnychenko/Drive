using Drive.Client.Exceptions;
using Drive.Client.Models.EntityModels.Announcement;
using Drive.Client.Models.EntityModels.Identity;
using Drive.Client.Services.Announcement;
using Drive.Client.ViewModels.Base;
using Microsoft.AppCenter.Crashes;
using System;
using System.Diagnostics;
using System.Threading;

namespace Drive.Client.ViewModels.ActionBars {
    public class NewPostActionBarViewModel : ExecutionActionBarBaseViewModel {

        private readonly IAnnouncementService _announcementService;

        private CancellationTokenSource _newAnnounceCancellationTokenSource = new CancellationTokenSource();

        public NewPostActionBarViewModel(IAnnouncementService announcementService) {
            _announcementService = announcementService;
        }

        public override void Dispose() {
            base.Dispose();

            ResetCancellationTokenSource(ref _newAnnounceCancellationTokenSource);
        }

        public override void ResolveExecutionAvailability(object condition) {
            base.ResolveExecutionAvailability(condition);

            if (condition is bool boolCondition) {
                IsExecutionAvailable = boolCondition;
            }
        }

        protected async override void OnExecuteCommand() {
            base.OnExecuteCommand();

            if (NavigationService.LastPageViewModel is IInputForm inputForm) {
                if (inputForm.ValidateForm()) {
                    Guid busyKey = Guid.NewGuid();
                    UpdateBusyVisualState(busyKey, true);

                    ResetCancellationTokenSource(ref _newAnnounceCancellationTokenSource);
                    CancellationTokenSource cancellationTokenSource = _newAnnounceCancellationTokenSource;

                    if (inputForm is IBuildFormModel buildFormModel) {
                        object formModel = buildFormModel.BuildFormModel();

                        try {
                            if (formModel is AnnounceBody announceBody) {
                                await _announcementService.NewAnnouncementAsync(announceBody, cancellationTokenSource);
                                await NavigationService.GoBackAsync();
                            } else if (formModel is AnnounceBodyWithData announceBodyWithData) {
                                string eventId = await _announcementService.UploadAttachedDataAsync(announceBodyWithData.AttachedData, new CancellationTokenSource());

                                if (!string.IsNullOrEmpty(eventId)) {
                                    await _announcementService.NewAnnouncementAsync(announceBodyWithData.AnnounceBody, cancellationTokenSource, eventId);
                                    await NavigationService.GoBackAsync();
                                }
                            }
                        }
                        catch (OperationCanceledException) { }
                        catch (ObjectDisposedException) { }
                        catch (ServiceAuthenticationException) { }
                        catch (Exception exc) {
                            Crashes.TrackError(exc);
                            Debugger.Break();
                        }
                    }

                    UpdateBusyVisualState(busyKey, false);
                }
            }
        }
    }
}
