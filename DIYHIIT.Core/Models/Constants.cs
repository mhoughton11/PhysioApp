using System;
using System.Collections.Generic;
using System.Text;

namespace DIYHIIT.Models
{
    public static class Constants
    {
        // Cell Background Colours
        public const string HIITBackground = "#6EA5FF";
        public const string CalisthenicsBackground = "#FFEB6E";
        public const string PilatesBackground = "#FF726E";
        public const string YogaBackground = "#6EFF8B";

        // Maximum Image File Size
        public const long MAX_FILE_SIZE = 1800000; // (1.8 MB for a 15Mp JPEG)

        // Cloud Storage Account
        public const string STORAGE_ACCOUNT_KEY1 = "DefaultEndpointsProtocol=https;AccountName=diyhiitstorage;AccountKey=r9BNAa6fGu+vuOvZG7Ia37yVKaTgtFrqVpvX15HyMGV4LfKVNLyavcjLt5YWHuDAhulHYzacyOMVdTxbMI6JlA==;EndpointSuffix=core.windows.net";
    }
}
