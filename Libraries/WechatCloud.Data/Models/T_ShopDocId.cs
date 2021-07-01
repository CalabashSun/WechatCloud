using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using WechatCloud.Core.Models;

namespace WechatCloud.Data.Models
{
    [Table("T_ShopDocId")]
    public class T_ShopDocId: BaseEntity
    {

        public string AppId { get; set; }

        public string ShopId { get; set; }

        public string ShopNo { get; set; }

        public string ShopName { get; set; }

        public string CpDocId { get; set; }

        public DateTime? UploadTime { get; set; }

        public DateTime? CreateTime { get; set; }

    }
}
