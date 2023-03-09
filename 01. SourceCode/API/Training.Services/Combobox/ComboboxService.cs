using Microsoft.EntityFrameworkCore;
using NTS.Common;
using Training.Models.Entities;
using Training.Models.Models.Combobox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Services.Combobox
{
    public class ComboboxService : IComboboxService
    {
        private TrainingContext _sqlContext;

        public ComboboxService(TrainingContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        //public async Task<List<ComboboxModel>> GetAllTuyenBienGioi()
        //{
        //    List<ComboboxModel> listCombobox = new List<ComboboxModel>();
        //    try
        //    {
        //        listCombobox = _sqlContext.TuyenBG.OrderBy(r => r.Order).Select(s => new ComboboxModel()
        //        {
        //            Id = s.IdTuyenBG,
        //            Name = s.Name
        //        }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return listCombobox;
        //}
    }
}
