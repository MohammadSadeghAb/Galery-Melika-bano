using Framework.OperationResult;
using ViewModels.Pages.Admin.TotalSales;

namespace Application.TotalSaleApp
{
    public interface ITotalSaleApplication
    {
        Task<OperationResult> AddTotalSale(CommonViewModel totalsale);
        Task<OperationResult> DeleteTotalSale(Guid Id);
        Task<OperationResultWithData<CommonViewModel>> GetTotalSale(Guid Id);
        Task<OperationResultWithData<IList<CommonViewModel>>> GetAllTotalSale();
    }
}
