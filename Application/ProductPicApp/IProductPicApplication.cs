using Framework.OperationResult;
using ViewModels.Pages.Admin.ProductPics;

namespace Application.ProductPicApp
{
    public interface IProductPicApplication
    {
        Task<OperationResult> AddProductPic(CommonViewModel productpic);
        Task<OperationResult> DeleteProductPic(Guid Id);
        Task<OperationResult> UpdateProductPic(CommonViewModel productpic);
        Task<OperationResultWithData<CommonViewModel>> GetProductPic(Guid Id);
        Task<OperationResultWithData<IList<CommonViewModel>>> GetAllProductPic();
    }
}
