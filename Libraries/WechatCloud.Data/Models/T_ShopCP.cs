using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using WechatCloud.Core.Models;

namespace WechatCloud.Data.Models
{
    [Table("T_ShopCP")]
    public class T_ShopCP: BaseEntity
    {
        public string AppId { get; set; }

        public string ShopId { get; set; }

        public string ShopNo { get; set; }

        public string ShopName { get; set; }

        public string CPJSON { get; set; }

        public string DocId { get; set; }

        public DateTime? UploadTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public DateTime? CreateTime { get; set; }

    }
}
