using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    using System.Linq;

    using BLL.BusinessObjects;
    using BLL.Converters;

    using DAL;
    using DAL.Entities;

    public class VisitService : IVisitService
    {
        private IDALFacade facade;
        private VisitConverter visitConv = new VisitConverter();
        private Visit newVisit;

        public VisitService(IDALFacade facade)
        {
            this.facade = facade;
        }

        public VisitBO Create(VisitBO visit)
        {
            using (var uow = this.facade.UnitOfWork)
            {
                this.newVisit = uow.VisitRepository.Create(this.visitConv.Convert(visit));
                uow.Complete();
                return this.visitConv.Convert(this.newVisit);
            }
        }

        public List<VisitBO> GetAll()
        {
            using (var uow = this.facade.UnitOfWork)
            {
                return uow.VisitRepository.GetAll().Select(this.visitConv.Convert).OrderBy(visit => visit.DateOfVisit)
                    .ToList();
            }
        }

        public VisitBO Get(int id)
        {
            using (var uow = this.facade.UnitOfWork)
            {
                this.newVisit = uow.VisitRepository.Get(id);
                uow.Complete();
                return this.visitConv.Convert(this.newVisit);
            }
        }

        public VisitBO Update(VisitBO visit)
        {
            using (var uow = this.facade.UnitOfWork)
            {
                var visitFromDb = uow.VisitRepository.Get(visit.Id);

                visitFromDb.Title = visit.Title;
                visitFromDb.Description = visit.Description;
                visitFromDb.IsDone = visit.IsDone;
                visitFromDb.DateOfVisit = visit.DateOfVisit;
                uow.Complete();
                return this.visitConv.Convert(visitFromDb);
            }
        }

        public VisitBO Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
