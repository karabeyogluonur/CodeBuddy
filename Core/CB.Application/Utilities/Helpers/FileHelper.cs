using CB.Application.Utilities.Defaults;
using CB.Domain.Enums.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Application.Utilities.Helpers
{
    public class FileHelper
    {
        public static string GetRegisteredFilePath(RegisteredFileType registeredFileType)
        {
            switch (registeredFileType)
            {
                case RegisteredFileType.AvatarImage:
                    return FilePathDefaults.AvatarImageLocalPath;
                default:
                    return FilePathDefaults.CommonLocalPath;
            }
        }

        public static string GetCommonPath()
        {
            return FilePathDefaults.CommonLocalPath;
        }
    }
}
