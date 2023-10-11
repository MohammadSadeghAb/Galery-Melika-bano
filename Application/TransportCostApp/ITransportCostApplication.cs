using Domain.ProductAgg;
using Domain.TransportCostAgg;
using Framework.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.TransportCosts;

namespace Application.TransportCostApp
{
    public interface ITransportCostApplication
    {
        Task<OperationResult> AddTransportCost(CommonViewModel transportcost);
        Task<OperationResult> DeleteTransportCost(Guid Id);
        Task<OperationResult> UpdateTransportCost(CommonViewModel transportcost);
        Task<OperationResultWithData<CommonViewModel>> GetTransportCost(Guid? Id);
        Task<OperationResultWithData<IList<CommonViewModel>>> GetAllTransportCost();
        Task<OperationResultWithData<TransportCost>> GetByWeight(int weight);
    }
}
