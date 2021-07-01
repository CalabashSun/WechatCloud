using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WechatCloud.Core.Configuration
{
    public partial class AppSettings
    {
        /// <summary>
        /// Gets or sets cache configuration parameters
        /// </summary>
        public ConnectionsConfig ConnectionsConfig { get; set; } = new ConnectionsConfig();

        /// <summary>
        /// Gets or sets Redis configuration parameters
        /// </summary>
        public RedisConfig RedisConfig { get; set; } = new RedisConfig();

        /// <summary>
        /// Gets or sets additional configuration parameters
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, JToken> AdditionalData { get; set; }

    }
}
