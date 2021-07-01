using System;
using System.Collections.Generic;
using System.Text;

namespace WechatCloud.Data.HttpModels.Response
{
    //如果好用，请收藏地址，帮忙分享。
    public class insertcpjson
    {
        /// <summary>
        /// 
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> id_list { get; set; }
    }
}
