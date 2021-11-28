using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Services.Models
{
    public class ServiceConfiguration
    {
        public AWSS3Configuration AWSS3 { get; set; }
    }

    public class AWSS3Configuration
    {
        public string BucketName { get; set; }
    }

    public enum UploadFileName
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Fifth = 5,
    }
}
