using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace WechatCloud.Data.XML
{
    [XmlType("XMLWX")]
    public class XMLWX
    {
        [XmlElement("appid")]
        [Required]
        public string appid { get; set; } = "wxd1bf5f6977351373";

        [XmlElement("attach")]
        [Required]
        public string attach { get; set; } = "CRM卷包4";

        [XmlElement("bank_type")]
        [Required]
        public string bank_type { get; set; } = "BOC_DEBIT";

        [XmlElement("cash_fee")]
        [Required]
        public string cash_fee { get; set; } = "990";

        [XmlElement("cash_fee_type")]
        [Required]
        public string cash_fee_type { get; set; } = "CNY";

        [XmlElement("fee_type")]
        [Required]
        public string fee_type { get; set; } = "CNY";

        [XmlElement("is_subscribe")]
        [Required]
        public string is_subscribe { get; set; } = "Y";

        [XmlElement("mch_id")]
        [Required]
        public string mch_id { get; set; } = "1583413271";

        [XmlElement("nonce_str")]
        [Required]
        public string nonce_str { get; set; } = "KHJ1RPV9g3TBeIrX";

        [XmlElement("openid")]
        [Required]
        //下单用户号
        public string openid { get; set; }

        [XmlElement("out_trade_no")]
        [Required]
        //商户订单号
        public string out_trade_no { get; set; }

        [XmlElement("result_code")]
        [Required]
        public string result_code { get; set; } = "SUCCESS";

        [XmlElement("return_code")]
        [Required]
        public string return_code { get; set; } = "SUCCESS";

        [XmlElement("return_msg")]
        [Required]
        public string return_msg { get; set; } = "OK";

        [XmlElement("sign")]
        [Required]
        public string sign { get; set; } = "89F968CE83C991EB46613AA3D39E8430";

        [XmlElement("time_end")]
        [Required]
        public string time_end { get; set; } = "20210506213745";

        [XmlElement("total_fee")]
        [Required]
        public string total_fee { get; set; } = "990";

        [XmlElement("trade_state")]
        [Required]
        public string trade_state { get; set; } = "SUCCESS";

        [XmlElement("trade_state_desc")]
        [Required]
        public string trade_state_desc { get; set; } = "支付成功";

        [XmlElement("trade_type")]
        [Required]
        public string trade_type { get; set; } = "JSAPI";

        [XmlElement("transaction_id")]
        [Required]
        public string transaction_id { get; set; } = "4200001004202105065643609237";
    }
}
