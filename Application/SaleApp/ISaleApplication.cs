using Domain.ProductAgg;
using Domain.SaleAgg;
using Framework.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Sales;

namespace Application.SaleApp
{
    public interface ISaleApplication
    {
        Task<OperationResult> AddSale(CommonViewModel sale);
        Task<OperationResult> DeleteSale(Guid Id);
        Task<OperationResult> UpdateSale(CommonViewModel sale);
        Task<OperationResultWithData<CommonViewModel>> GetSale(Guid Id);
        Task<OperationResultWithData<IList<CommonViewModel>>> GetAllSale();
    }
}
