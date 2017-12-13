using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace BLL.Services
{
    public class FilePathService : IFilePathService
    {
        private IDALFacade facade;

        public FilePathService(IDALFacade facade)
        {
            this.facade = facade;
        }
    }
}
