using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using WechatCloud.Core.Models;

namespace WechatCloud.Data.Models
{
    [Table("T_TOKEN")]
    public class T_TOKEN : BaseEntity
    {

        public string pp { get; set; }

        public string appid { get; set; }

        public string secret { get; set; }

        public string env { get; set; }

        public string ACCESS_TOKEN { get; set; }

        public string BZ { get; set; }

    }
}
