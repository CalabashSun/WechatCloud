using System;
using System.Collections.Generic;
using System.Text;
using WechatCloud.Data.Models;
using WechatCloud.Data.PageModels;
using WechatCloud.Services.Repositorys;

namespace WechatCloud.Services.ModelServices
{

    public interface IT_ShopDocIdServices: IRepository<T_ShopDocId>
    {
        PageDataView<T_ShopDocId> GetPageList(string shopName, int page, int pageSize = 10);
    }
    public class T_ShopDocIdServices : Repository<T_ShopDocId>, IT_ShopDocIdServices
    {
        public PageDataView<T_ShopDocId> GetPageList(string shopName, int page, int pageSize = 10)
        {
            PageCriteria criteria = new PageCriteria();
            criteria.Condition = "1=1";
            if (!string.IsNullOrEmpty(shopName) && string.IsNullOrEmpty(shopName))
                criteria.Condition += $" and ShopName like '%{shopName}%'";
            criteria.CurrentPage = page;
            criteria.Fields = "*";
            criteria.PageSize = pageSize;
            criteria.TableName = "T_ShopDocId a";
            criteria.PrimaryKey = "Id";
            return GetAllPaged(criteria);
        }
    }
}
