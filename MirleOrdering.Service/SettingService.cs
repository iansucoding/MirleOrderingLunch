using MirleOrdering.Data.Models;
using MirleOrdering.Repo;
using MirleOrdering.Service.Interfaces;
using MirleOrdering.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MirleOrdering.Service
{
    public class SettingService : ISettingService
    {
        private readonly IRepository<Setting> _repository;
        public SettingService(IRepository<Setting> repository)
        {
            _repository = repository;
        }

        protected SettingViewModel ConvertToViewModel(Setting setting) => new SettingViewModel
        {
            SettingId = setting.Id,
            StopHourOn = setting.StopHourOn,
            Announcement = setting.Announcement,
        };

        public SettingViewModel GetById(long id)
        {
            var user = _repository.GetById(id);
            if (user != null)
            {
                return ConvertToViewModel(user);
            }
            return null;
        }
        public IEnumerable<SettingViewModel> GetAll()
        {
            return _repository.GetAll().Select(user => ConvertToViewModel(user));
        }
        public IEnumerable<SettingViewModel> Find(Func<Setting, bool> predicate)
        {
            return _repository.GetQueryable().Where(predicate).Select(user => ConvertToViewModel(user));
        }

        public ReturnViewModel Create(SettingBaseModel model)
        {
            var result = new ReturnViewModel();
            var entity = new Setting
            {
                StopHourOn = model.StopHourOn,
                Announcement = model.Announcement,
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
        public ReturnViewModel Update(SettingViewModel model)
        {
            var result = new ReturnViewModel();
            var entity = _repository.GetById(model.SettingId);
            if (entity == null)
            {
                result.Message = "setting not found";
                return result;
            }
            entity.StopHourOn = model.StopHourOn;
            entity.Announcement = model.Announcement;
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
                result.Message = "setting not found";
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

        public SettingViewModel GetLastOne()
        {
            var entity =  _repository.GetQueryable().OrderByDescending(x => x.AddedOn).FirstOrDefault();
            if(entity != null)
            {
                return ConvertToViewModel(entity);
            }
            return null;
        }
    }
}
