using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using WechatCloud.Core.Models;

namespace WechatCloud.Data.Models
{
    [Table("T_FoodPic")]
    public class T_FoodPic: BaseEntity
    {
        public string AppId { get; set; }

        public string DP_ID { get; set; }

        public string DP_Name { get; set; }

        public string Pic_Path { get; set; }

        public string File_Path { get; set; }

        public string Pic_Path_One { get; set; }

        public string File_Path_One { get; set; }

        public string Pic_Path_Two { get; set; }

        public string File_Path_Two { get; set; }

        public string Pic_Path_Three { get; set; }

        public string File_Path_Three { get; set; }

        public string Video_Path { get; set; }

        public string File_Path_Video { get; set; }

        public bool IsHandle { get; set; }

        public string HandleResult { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime? UploadTime { get; set; }

        public DateTime CreateTime { get; set; }

        public string LatestCreater { get; set; }

    }
}
