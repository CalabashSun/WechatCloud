using System;
using System.Collections.Generic;
using System.Text;

namespace WechatCloud.Data.HttpModels.Response
{
    public class uploadimg
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
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string authorization { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string file_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cos_file_id { get; set; }
    }
}
