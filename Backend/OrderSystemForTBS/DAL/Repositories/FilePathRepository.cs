using System;
using System.Collections.Generic;
using System.Text;
using DAL.Context;
using DAL.IRepositories;

namespace DAL.Repositories
{
    public class FilePathRepository : IFilePathRepository
    {
        private OrderSystemContext _context;

        public FilePathRepository(OrderSystemContext context)
        {
            _context = context;
        }
    }
}
