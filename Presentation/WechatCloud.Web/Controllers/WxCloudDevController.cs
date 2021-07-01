using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WechatCloud.Services.ModelServices;

namespace WechatCloud.Web.Controllers
{
    public class WxCloudDevController : Controller
    {
        private readonly IT_FoodPicServices _foodPicServices;
        private readonly IT_ShopCPServices _shopCPServices;

        public WxCloudDevController(IT_FoodPicServices foodPicServices
        ,IT_ShopCPServices shopCPServices)
        {
            _foodPicServices = foodPicServices;
            _shopCPServices = shopCPServices;
        }

        public IActionResult UpdateFoodJson(string shopId)
        {
            var result = _shopCPServices.HandleCpJson(shopId);
            return Ok(result);
        }
        public IActionResult BatchUpload()
        {
            try {
                _foodPicServices.ExcuteBatchUpload();
                return Ok("{\"message\":\"success\"}");
            } catch (Exception ex) {
                return Ok("{\"message\":\""+ex.Message+"\"}");
            }
        }
    }
}
