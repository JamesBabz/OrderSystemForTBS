using System;
using System.Collections.Generic;
using System.Text;
using BLL.BusinessObjects;

namespace BLL
{
    public interface IPropositionService
    {
        //C
        PropositionBO Create(PropositionBO bo);
        //R
        List<PropositionBO> GetAllById(int customerId);
        PropositionBO Get(int Id);

        List<int> allFileIds();
            //U
        PropositionBO Update(PropositionBO bo);
        //D
        PropositionBO Delete(int Id);
    }
}
