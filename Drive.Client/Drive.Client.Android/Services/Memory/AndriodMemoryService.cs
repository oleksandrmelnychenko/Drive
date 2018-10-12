using Android.Content;
using Drive.Client.Droid.Services.Memory;
using Drive.Client.Services.Memory;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndriodMemoryService))]
namespace Drive.Client.Droid.Services.Memory {
    public class AndriodMemoryService : IMemoryService {

        private static Context _context;

        /// <summary>
        ///     ctor().
        /// </summary>
        public AndriodMemoryService() {
            _context = MainActivity.Instance;
        }

        public MemoryInfo GetInfo() => MemoryHelper.GetMemoryInfo(_context);
    }
}