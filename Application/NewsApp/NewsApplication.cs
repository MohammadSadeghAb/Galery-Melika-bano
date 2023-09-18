using Domain.NewsAgg;
using Domain.ProductAgg;
using Framework.OperationResult;
using Resources.Messages;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.NEWS;

namespace Application.NewsApp
{
    public class NewsApplication : INewsApplication
    {
        private readonly INewsRepository _repository;

        public NewsApplication(INewsRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> AddNews(CommonViewModel news)
        {
            var res = new OperationResult();

            if (_repository.Exist(x => x.Title == news.Title))
            {
                res.AddErrorMessage(string.Format(Errors.AlreadyExists, DataDictionary.Title));
            }

            if (0 < res.ErrorMessages.Count)
            {
                res.Succeeded = false;
                return res;
            }

            var _news = new News()
            {
                Title = news.Title,
                PicName = news.PicName,
                Ordering = news.Ordering,
                IsActive = news.IsActive,
                Description = news.Description,
            };

            await _repository.CreateAsync(_news);
            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResult> DeleteNews(Guid Id)
        {
            var res = new OperationResult();
            var newsForDelete = await _repository.GetAsync(Id);

            if (newsForDelete == null)
            {
                res.AddErrorMessage(Errors.ThereIsNotAnyDataWithThisId);
            }

            if (res.ErrorMessages.Count > 0)
            {
                res.Succeeded = false;
                return res;
            }

            _repository.Remove(newsForDelete);

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResultWithData<IList<CommonViewModel>>> GetAllNews()
        {
            var res = new OperationResultWithData<IList<CommonViewModel>>();

            var _news = await _repository.GetAllAsync();

            var _data = new List<CommonViewModel>();

            foreach (var news in _news)
            {
                _data.Add(new CommonViewModel
                {
                    Id = news.Id,
                    Title = news.Title,
                    PicName = news.PicName,
                    IsActive = news.IsActive,
                    Ordering = news.Ordering,
                    Description = news.Description,
                    InsertDateTime = news.InsertDateTime,
                    UpdateDateTime = news.UpdateDateTime,
                });
            }

            res.Data = _data;

            return res;
        }

        public async Task<OperationResultWithData<News>> GetNewsByTitle(string title)
        {
            var res = new OperationResultWithData<News>();

            var news = await _repository.GetByTitle(title);

            res.Data = news;

            return res;
        }

        public async Task<OperationResultWithData<CommonViewModel>> GetNews(Guid? Id)
        {
            var res = new OperationResultWithData<CommonViewModel>();

            var news = await _repository.GetAsync(Id);

            var _news = new CommonViewModel
            {
                Id = news.Id,
                Title = news.Title,
                PicName = news.PicName,
                IsActive = news.IsActive,
                Ordering = news.Ordering,
                Description = news.Description,
                InsertDateTime = news.InsertDateTime,
                UpdateDateTime = news.UpdateDateTime,
            };

            res.Data = _news;

            return res;
        }

        public async Task<OperationResult> UpdateNews(CommonViewModel news)
        {
            var res = new OperationResult();

            var newsForUpdate = await _repository.GetAsync(news.Id.Value);

            if (newsForUpdate == null)
            {
                res.AddErrorMessage(Errors.ThereIsNotAnyDataWithThisId);
                res.Succeeded = false;
                return res;
            }

            if (0 < res.ErrorMessages.Count)
            {
                res.Succeeded = false;
                return res;
            }

            newsForUpdate.SetUpdateDateTime();

            newsForUpdate.Title = news.Title;
            newsForUpdate.IsActive = news.IsActive;
            newsForUpdate.Ordering = news.Ordering;
            newsForUpdate.Description = news.Description;

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }
    }
}
