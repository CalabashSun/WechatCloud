using Dapper;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WechatCloud.Core.Helper;
using WechatCloud.Data.HttpModels.Response;
using WechatCloud.Data.Models;
using WechatCloud.Data.PageModels;
using WechatCloud.Services.Repositorys;

namespace WechatCloud.Services.ModelServices
{
    public interface IT_FoodPicServices: IRepository<T_FoodPic>
    {
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="fooid">菜品id</param>
        /// <param name="fooName">菜品名称</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        PageDataView<T_FoodPic> GetPageList(string fooid, string fooName, int page, int pageSize = 10);
        /// <summary>
        /// 上传指定数据
        /// </summary>
        /// <param name="id"></param>
        string ExcuteUpload(int id);
        /// <summary>
        /// 批量上传数据
        /// </summary>
        void ExcuteBatchUpload();
    }
    public class T_FoodPicServices:Repository<T_FoodPic>, IT_FoodPicServices
    {
        private readonly IT_TOKENServices _tokenServices;

        public T_FoodPicServices(IT_TOKENServices tokenServices)
        {
            _tokenServices = tokenServices;
        }

        public PageDataView<T_FoodPic> GetPageList(string fooid, string fooName, int page, int pageSize = 10)
        {
            PageCriteria criteria = new PageCriteria();
            criteria.Condition = "1=1";
            if (!string.IsNullOrEmpty(fooid)&&string.IsNullOrEmpty(fooName))
                criteria.Condition += $" and DP_ID like '%{fooid}%'";
            if (!string.IsNullOrEmpty(fooName)&&string.IsNullOrEmpty(fooid))
                criteria.Condition += $" and DP_Name like '%{fooName}%'";
            if (!string.IsNullOrEmpty(fooName) && !string.IsNullOrEmpty(fooid))
                criteria.Condition += $" and (DP_Name like '%{fooName}%' or DP_ID like '%{fooid}%')";
            criteria.CurrentPage = page;
            criteria.Fields = "*";
            criteria.PageSize = pageSize;
            criteria.TableName = "T_FoodPic a";
            criteria.PrimaryKey = "Id";
            return GetAllPaged(criteria);
        }
        public string ExcuteUpload(int id)
        {
            var unhandleData = Find(id);
            var tokenInfos = _tokenServices.GetTOKEN(unhandleData.AppId);
            var result = "";
            //上传缩略图
            if (!string.IsNullOrEmpty(unhandleData.Pic_Path))
            {
                var uploadresult=UploadImgInfo(tokenInfos.ACCESS_TOKEN,tokenInfos.env,unhandleData.File_Path,unhandleData.Pic_Path);
                if (uploadresult.Contains("access"))
                    return "accesstoken time out";
                result = uploadresult.Contains("success") ? "缩略图上传成功-|-" : "缩略图上传失败-|-";
            }
            if (!string.IsNullOrEmpty(unhandleData.Pic_Path_One))
            {
                var uploadresult = UploadImgInfo(tokenInfos.ACCESS_TOKEN, tokenInfos.env, unhandleData.File_Path_One, unhandleData.Pic_Path_One);
                if (uploadresult.Contains("access"))
                    return "accesstoken time out";
                result += uploadresult.Contains("success") ? "轮播图_1上传成功-|-" : "轮播图_1上传失败-|-";
            }
            if (!string.IsNullOrEmpty(unhandleData.Pic_Path_Two))
            {
                var uploadresult = UploadImgInfo(tokenInfos.ACCESS_TOKEN, tokenInfos.env, unhandleData.File_Path_Two, unhandleData.Pic_Path_Two);
                if (uploadresult.Contains("access"))
                    return "accesstoken time out";
                result += uploadresult.Contains("success") ? "轮播图_2上传成功-|-" : "轮播图_2上传失败-|-";
            }
            if (!string.IsNullOrEmpty(unhandleData.Pic_Path_Three))
            {
                var uploadresult = UploadImgInfo(tokenInfos.ACCESS_TOKEN, tokenInfos.env, unhandleData.File_Path_Three, unhandleData.Pic_Path_Three);
                if (uploadresult.Contains("access"))
                    return "accesstoken time out";
                result += uploadresult.Contains("success") ? "轮播图_3上传成功-|-" : "轮播图_3上传失败-|-";
            }
            if (!string.IsNullOrEmpty(unhandleData.Video_Path))
            {
                var uploadresult = UploadImgInfo(tokenInfos.ACCESS_TOKEN, tokenInfos.env, unhandleData.File_Path_Video, unhandleData.Video_Path);
                if (uploadresult.Contains("access"))
                    return "accesstoken time out";
                result += uploadresult.Contains("success") ? "视频上传成功-|-" : "视频上传失败-|-";
            }
            unhandleData.IsHandle = true;
            unhandleData.UploadTime = DateTime.Now;
            unhandleData.HandleResult = result;
            _Conn.Update<T_FoodPic>(unhandleData);
            return result;
        }
        public void ExcuteBatchUpload()
        {
            var unhandleSql = "select * from T_FoodPic where IsHandle=0 and datediff(day,UpdateTime,getdate())=0";
            var unhandleData = _Conn.Query<T_FoodPic>(unhandleSql);
            var tokenInfos = _tokenServices.GetAll().ToList();
            foreach (var data in unhandleData)
            {
                var result = "";
                var currentToken = tokenInfos.First(p => p.appid == data.AppId);
                //上传缩略图
                if (!string.IsNullOrEmpty(data.Pic_Path))
                {
                    var uploadresult = UploadImgInfo(currentToken.ACCESS_TOKEN, currentToken.env, data.File_Path, data.Pic_Path);
                    result = uploadresult.Contains("success") ? "缩略图上传成功-|-" : "缩略图上传失败-|-";
                }
                if (!string.IsNullOrEmpty(data.Pic_Path_One))
                {
                    var uploadresult = UploadImgInfo(currentToken.ACCESS_TOKEN, currentToken.env, data.File_Path_One, data.Pic_Path_One);
                    result += uploadresult.Contains("success") ? "轮播图_1上传成功-|-" : "轮播图_1上传失败-|-";
                }
                if (!string.IsNullOrEmpty(data.Pic_Path_Two))
                {
                    var uploadresult = UploadImgInfo(currentToken.ACCESS_TOKEN, currentToken.env, data.File_Path_Two, data.Pic_Path_Two);
                    result += uploadresult.Contains("success") ? "轮播图_2上传成功-|-" : "轮播图_2上传失败-|-";
                }
                if (!string.IsNullOrEmpty(data.Pic_Path_Three))
                {
                    var uploadresult = UploadImgInfo(currentToken.ACCESS_TOKEN, currentToken.env, data.File_Path_Three, data.Pic_Path_Three);
                    result += uploadresult.Contains("success") ? "轮播图_3上传成功-|-" : "轮播图_3上传失败-|-";
                }
                if (!string.IsNullOrEmpty(data.Video_Path))
                {
                    var uploadresult = UploadImgInfo(currentToken.ACCESS_TOKEN, currentToken.env, data.File_Path_Video, data.Video_Path);
                    result += uploadresult.Contains("success") ? "视频上传成功-|-" : "视频上传失败-|-";
                }
                data.IsHandle = true;
                data.UploadTime = DateTime.Now;
                data.HandleResult = result;
                _Conn.Update<T_FoodPic>(data);
            }
        }
        private string UploadImgInfo(string accessToken, string env, string filepath,string picPath)
        {
            var postUrl = $"https://api.weixin.qq.com/tcb/uploadfile?access_token={accessToken}";
            var postData = "{\"env\":\"" +env + "\",\"path\":\"" + filepath + "\"}";
            var postResult = HttpHelper.Post(postUrl, null, postData, "Utf-8");
            var retrunInfo = JsonConvert.DeserializeObject<uploadimg>(postResult);
            if (retrunInfo.errcode == 0)
            {
                var postInfo = new Dictionary<string, string>();
                postInfo["key"] = filepath;
                postInfo["Signature"] = retrunInfo.authorization;
                postInfo["x-cos-security-token"] = retrunInfo.token;
                postInfo["x-cos-meta-fileid"] = retrunInfo.cos_file_id;
                var uploadResult = HttpHelper.PostFileWechat(retrunInfo.url, picPath, postInfo);
                if (!string.IsNullOrEmpty(uploadResult))
                {
                    return picPath + " upload error.";
                }
                return picPath + "upload success.";
            }
            else {
                return "upload accesstoken error";
            }

        }
    }
}
