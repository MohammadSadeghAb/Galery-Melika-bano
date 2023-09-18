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
        Task<OperationResult> AddNews(CommonViewModel news);
        Task<OperationResult> DeleteNews(Guid Id);
        Task<OperationResult> UpdateNews(CommonViewModel news);
        Task<OperationResultWithData<CommonViewModel>> GetNews(Guid? Id);
        Task<OperationResultWithData<IList<CommonViewModel>>> GetAllNews();
        Task<OperationResultWithData<News>> GetNewsByTitle(string title);
    }
}
