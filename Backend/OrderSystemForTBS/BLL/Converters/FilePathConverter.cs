using System;
using System.Collections.Generic;
using System.Text;
using BLL.BusinessObjects;
using DAL.Entities;

namespace BLL.Converters
{
    public class FilePathConverter
    {

        public FilePath Convert(FilePathBO file)
        {
            if (file == null) { return null; }
            {
                return new FilePath()
                {
                    Id = file.Id,
                    Path = file.Path
                };
            }
        }

        public FilePathBO Convert(FilePath file)
        {
            if (file == null) { return null; }
            {
                return new FilePathBO()
                {
                    Id = file.Id,
                    Path = file.Path
                };
            }
        }

    }
}
