using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Client.Services
{
    public interface Interface1
    {
        /// <summary>
        /// Takes photo from device (camera, galery). Media in base64 format.
        /// </summary>
        /// <returns></returns>
        Task<string> GetPhotoAsync();
    }
}
