using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WechatCloud.Core.Helper;
using WechatCloud.Data.HttpModels.Response;
using WechatCloud.Data.Models;
using WechatCloud.Services.Repositorys;

namespace WechatCloud.Services.ModelServices
{
    public interface IT_ShopCPServices : IRepository<T_ShopCP>
    {
        string HandleCpJson(string shopId);
    }

    public class T_ShopCPServices : Repository<T_ShopCP>, IT_ShopCPServices
    {
        private readonly IT_TOKENServices _tokenServices;

        public T_ShopCPServices(IT_TOKENServices tokenServices)
        {
            _tokenServices = tokenServices;
        }
        public string HandleCpJson(string shopId)
        {
            var unhandleDataSql = $"select * from T_ShopCP where ShopId='{shopId}'";
            var unhandleData = _Conn.QueryFirstOrDefault<T_ShopCP>(unhandleDataSql);
            if(unhandleData!=null)
            {
                //判断是新增还是修改
                if (!string.IsNullOrEmpty(unhandleData.DocId))
                {
                    //修改
                    var tokenInfos = _tokenServices.GetTOKEN(unhandleData.AppId);
                    var postUrl = $"https://api.weixin.qq.com/tcb/databaseupdate?access_token={tokenInfos.ACCESS_TOKEN}";
                    var postJosn = "{\"env\":\"" + tokenInfos.env + "\",\"query\":\"db.collection(\\\"cp_info\\\")" +
                        ".doc(\\\""+unhandleData.DocId+"\\\").set({data:" + unhandleData.CPJSON + "})\"}";
                    var updateResult = HttpHelper.PostJsonData(postUrl, postJosn);
                    return updateResult;
                }
                else{
                    var tokenInfos = _tokenServices.GetTOKEN(unhandleData.AppId);
                    var postUrl = $"https://api.weixin.qq.com/tcb/databaseadd?access_token={tokenInfos.ACCESS_TOKEN}";
                    var postJosn = "{\"env\":\"" + tokenInfos.env + "\",\"query\":\"db.collection(\\\"cp_info\\\").add({data:" + unhandleData.CPJSON + "})\"}";
                    var postResult = HttpHelper.PostJsonData(postUrl, postJosn);
                    //更新docId
                    var resultInfo = JsonConvert.DeserializeObject<insertcpjson>(postResult);
                    if (resultInfo.errcode == 0)
                    {
                        unhandleData.DocId = resultInfo.id_list[0];
                        unhandleData.UploadTime = DateTime.Now;
                        Update(unhandleData);
                    }
                    return postResult;
                }

            }
            return "";            
        }
    }
}
