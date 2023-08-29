using Domain.NewsAgg;
using Domain.ProductAgg;
using Framework.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.NEWS;

namespace Application.NewsApp
{
    public interface INewsApplication
    {
        Task<OperationResult> AddProduct(CommonViewModel news);
        Task<OperationResult> DeleteProduct(Guid Id);
        Task<OperationResult> UpdateProduct(CommonViewModel news);
        Task<OperationResultWithData<CommonViewModel>> GetProduct(Guid? Id);
        Task<OperationResultWithData<IList<CommonViewModel>>> GetAllProduct();
        Task<OperationResultWithData<News>> GetNewsByTitle(string title);
    }
}
