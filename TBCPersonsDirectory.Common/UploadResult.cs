using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Common
{
    public class UploadResult
    {
        public bool Success { get; set; }
        public UploadError Error { get; set; }
        public string LocalUrl { get; set; }
        public string FileName { get; set; }
        public string FullUrl { get; set; }
        public class UploadError
        {
            public string Code { get; set; }
            public string Description { get; set; }
        }
    }
}
