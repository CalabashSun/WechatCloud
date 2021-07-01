using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WechatCloud.Core;
using WechatCloud.Core.Helper;
using WechatCloud.Data.XML;
using WechatCloud.Services.ModelServices;
using WechatCloud.Web.Models;

namespace WechatCloud.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IT_FoodPicServices _foodPicServices;

        public HomeController(ILogger<HomeController> logger
            ,IT_FoodPicServices foodPicServices)
        {
            _logger = logger;
            _foodPicServices = foodPicServices;
        }

        public IActionResult Index()
        {
            var idinfo= CommonHelper.GenrateId();
            return View();
        }

        [HttpPost]
        public IActionResult XMLDataSer(string xmldata)
        {
            Stream stream = HttpContext.Current.Request.InputStream;
            Byte[] postBytes = new Byte[stream.Length];
            stream.Read(postBytes, 0, (Int32)stream.Length);
            string postString = Encoding.UTF8.GetString(postBytes);
            var xmlserialize = new XmlSerializeHelper();
            var xmlStringModel = xmlserialize.DESerializer<XMLWX>(xmldata);
            var result = "{'code': 'SUCCESS','message':'" + xmlStringModel.out_trade_no + "' }";
            return Ok(result);
        }

        /// <summary>
        /// 获取菜品数据
        /// </summary>
        /// <returns></returns>
        public string GetFoodList(string foodId,string foodName, int page,int limit)
        {
            var result = _foodPicServices.GetPageList(foodId,foodName, page,limit);
            if (result.Items.Count > 0)
            {
                return JsonConvert.SerializeObject(result);
            }
            return "";
        }





        /// <summary>
        /// 上传图片到微信
        /// </summary>
        /// <returns></returns>
        public string PostToWechat(int id)
        {
            var result= _foodPicServices.ExcuteUpload(id);
            return result;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
