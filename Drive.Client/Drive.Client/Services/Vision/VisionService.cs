using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.Services.Vision {
    public class VisionService : IVisionService {

        private readonly string _subscriptionKey = "6a52ff8f3dd2422ba3d55f415151eb40";

        private readonly ComputerVisionClient _computerVision;

        // For printed text, change to TextRecognitionMode.Printed
        private const TextRecognitionMode textRecognitionMode = TextRecognitionMode.Printed;

        private const int numberOfCharsInOperationId = 36;

        /// <summary>
        ///     ctor().
        /// </summary>
        public VisionService() {
            _computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_subscriptionKey), new System.Net.Http.DelegatingHandler[] { }) {
                Endpoint = "https://westeurope.api.cognitive.microsoft.com/"
            };
        }

        public async Task<List<string>> AnalyzeImageForText(MediaFile file) {
            List<string> result = new List<string>();

            try {
                using (Stream imageStream = file.GetStream()) {
                    // Start the async process to recognize the text
                    RecognizeTextInStreamHeaders textHeaders = await _computerVision.RecognizeTextInStreamAsync(imageStream, textRecognitionMode);

                    result = await GetTextAsync(_computerVision, textHeaders.OperationLocation);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"--ERROR--: {ex.Message}");
                Debugger.Break();
            }

            return result;
        }

        private async Task<List<string>> GetTextAsync(ComputerVisionClient computerVision, string operationLocation) {
            // Retrieve the URI where the recognized text will be
            // stored from the Operation-Location header
            string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

            TextOperationResult result = await computerVision.GetTextOperationResultAsync(operationId);

            // Wait for the operation to complete
            int i = 0;
            int maxRetries = 10;
            while ((result.Status == TextOperationStatusCodes.Running || result.Status == TextOperationStatusCodes.NotStarted) && i++ < maxRetries) {
                Debug.WriteLine("Server status: {0}, waiting {1} seconds...", result.Status, i);
                await Task.Delay(1000);

                result = await computerVision.GetTextOperationResultAsync(operationId);
            }

            List<string> resultlines = new List<string>();

            IList<Line> lines = result.RecognitionResult?.Lines;

            foreach (Line line in lines) {
                resultlines.Add(line.Text.Trim().Replace(" ", ""));
            }

            return resultlines;
        }
    }
}
