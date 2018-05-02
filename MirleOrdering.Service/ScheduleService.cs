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
    public class ScheduleService : IScheduleService
    {
        private readonly IRepository<Schedule> _repository;
        private readonly ICategoryService _categoryService;

        public ScheduleService(IRepository<Schedule> repository, ICategoryService categoryService)
        {
            _repository = repository;
            _categoryService = categoryService;
        }
        protected ScheduleViewModel ConvertToViewModel(Schedule schedule) => new ScheduleViewModel
        {
            ScheduleId = schedule.Id,
            AvailableOn = schedule.AvailableOn,
            Remark = schedule.Remark,
            CategoryId = schedule.Category == null ? -1 : schedule.Category.Id,
            CategoryName = schedule.Category == null ? null : schedule.Category.Name
        };

        public ScheduleViewModel GetById(long id)
        {
            var entity = _repository.GetQueryable()
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
            return entity == null ? null : ConvertToViewModel(entity);
        }
        public IEnumerable<ScheduleViewModel> GetAll()
        {
            return _repository.GetQueryable()
                .Include(x => x.Category)
                .OrderBy(x => x.AvailableOn)
                .Select(x => ConvertToViewModel(x));
        }
        public IEnumerable<ScheduleViewModel> Find(Func<Schedule, bool> predicate)
        {
            return _repository.GetQueryable()
                .Include(x => x.Category)
                .Where(predicate)
                .OrderBy(x => x.AvailableOn)
                .Select(x => ConvertToViewModel(x));
        }

        public ReturnViewModel Create(ScheduleBaseModel model)
        {
            var result = new ReturnViewModel();
            var entity = new Schedule
            {
                AvailableOn = model.AvailableOn.Date,
                Remark = model.Remark,
                CategoryId = model.CategoryId,
                AddedOn = DateTime.Now
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
            result.Message = entity.Id.ToString();
            result.IsSuccess = true;
            return result;
        }
        public ReturnViewModel Update(ScheduleViewModel model)
        {
            var result = new ReturnViewModel();
            var entity = _repository.GetById(model.ScheduleId);
            if (entity == null)
            {
                result.Message = "schedule not found";
                return result;
            }
            entity.AvailableOn = model.AvailableOn.Date;
            entity.Remark = model.Remark;
            entity.CategoryId = model.CategoryId;
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
                result.Message = "schedule not found";
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

        public CategoryViewModel GetTodaySchedule()
        {
            var today = DateTime.Now.Date;
            var schedule =  _repository.Find(x => x.AvailableOn == today).FirstOrDefault();
            if (schedule == null)
            {
                return null;
            }
            return _categoryService.GetByIdWithProducts(schedule.CategoryId);
        }
    }
}
