using Microsoft.EntityFrameworkCore;
using MirleOrdering.Data.Models;
using MirleOrdering.Repo;
using MirleOrdering.Service.Interfaces;
using MirleOrdering.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MirleOrdering.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        protected CategoryViewModel ConvertToViewModel(Category category) => new CategoryViewModel
        {
            CategoryId = category.Id,
            CategoryName = category.Name,
            Description = category.Description,
            Seq = category.Seq,
            Products = category.Products == null ? null : category.Products.Select(prodcut => new ProductViewModel
            {
                ProductId = prodcut.Id,
                ProductName = prodcut.Name,
                Price = prodcut.Price,
                Description = prodcut.Description,

            })
        };

        public CategoryViewModel GetById(long id)
        {
            return ConvertToViewModel(_repository.GetById(id));
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            return _repository.GetQueryable().OrderBy(c => c.Seq).Select(category => ConvertToViewModel(category));
        }
        public IEnumerable<CategoryViewModel> Find(Func<Category, bool> predicate)
        {
            return _repository.GetQueryable().Where(predicate).ToList().Select(category => ConvertToViewModel(category));
        }

        public ReturnViewModel Create(CategoryBaseModel model)
        {
            var result = new ReturnViewModel();
            var entity = new Category
            {
                Name = model.CategoryName,
                Description = model.Description,
                AddedOn = DateTime.Now,
            };
            try
            {
                _repository.Create(entity);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
            result.IsSuccess = true;
            result.Message = entity.Id.ToString();
            return result;
        }

        public ReturnViewModel Update(CategoryViewModel model)
        {
            var result = new ReturnViewModel();
            var entity = _repository.GetById(model.CategoryId);
            if (entity == null)
            {
                result.Message = "category not found";
                return result;
            }
            entity.Name = model.CategoryName;
            entity.Description = model.Description;
            entity.Seq = model.Seq;
            entity.ModifiedOn = DateTime.Now;

            try
            {
                _repository.Update(entity);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }

            result.IsSuccess = true;
            return result;
        }

        public ReturnViewModel Delete(long id)
        {
            var result = new ReturnViewModel();
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                result.Message = "category not found";
                return result;
            }
            try
            {
                _repository.Delete(entity);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }

            result.IsSuccess = true;
            return result;
        }

        public CategoryViewModel GetByIdWithProducts(long id)
        {
            var result = _repository.GetQueryable()
                .Where(c => c.Id == id)
                .Include(c => c.Products)
                .FirstOrDefault();

            return ConvertToViewModel(result);
        }

        public IEnumerable<CategoryViewModel> GetAllWithProducts()
        {
            return _repository.GetQueryable()
                .Include(c => c.Products)
                .OrderBy(c => c.Seq)
                .Select(category => ConvertToViewModel(category));
        }

        public IEnumerable<SelectionViewModel> GetSelection()
        {
            return _repository.GetQueryable().OrderBy(c => c.Seq).ToList().Select(c => new SelectionViewModel
            {
                Value = c.Id,
                Text = c.Name
            });
        }
    }
}
