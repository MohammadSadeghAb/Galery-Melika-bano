using Framework.OperationResult;
using ViewModels.Pages.Admin.TotalSales;

namespace Application.TotalSaleApp
{
    public interface ITotalSaleApplication
    {
        Task<OperationResult> AddTotalSale(CommonViewModel totalsale);
        Task<OperationResult> DeleteTotalSale(Guid Id);
        Task<OperationResult> UpdateTotalSale(UpdateViewModel totalsale);
        Task<OperationResultWithData<DetailsViewModel>> GetTotalSale(Guid Id);
        Task<OperationResultWithData<IList<DetailsViewModel>>> GetAllTotalSale();
    }
}
