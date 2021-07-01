using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using WechatCloud.Data.Models;
using WechatCloud.Services.Repositorys;

namespace WechatCloud.Services.ModelServices
{
    public interface IT_TOKENServices: IRepository<T_TOKEN>
    {
        T_TOKEN GetTOKEN(string appId);
    }

    public class T_TOKENServices : Repository<T_TOKEN>, IT_TOKENServices
    {
        public T_TOKEN GetTOKEN(string appId)
        {
            var sqlInfo = $"select * from T_TOKEN where appid='{appId}'";
            var result=_Conn.QueryFirstOrDefault<T_TOKEN>(sqlInfo);
            return result;
        }
    }
}
