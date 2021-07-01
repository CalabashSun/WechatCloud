using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WechatCloud.Core.Helper
{
    public class DecryptInfo
    {
        /// <summary>
        /// 解密微信数据
        /// </summary>
        /// <param name="encryptedData">加密的数据</param>
        /// <param name="encryptIv">iv向量</param>
        /// <param name="sessionKey">调用 wx auth.code2Session 来获得</param>
        /// <returns></returns>
        public static string WechatDecrypt(string encryptedData, string encryptIv, string sessionKey)
        {
            //base64解码为字节数组
            var encryptData = Convert.FromBase64String(encryptedData);
            var key = Convert.FromBase64String(sessionKey);
            var iv = Convert.FromBase64String(encryptIv);

            //创建aes对象
            var aes = Aes.Create();

            if (aes == null)
            {
                throw new InvalidOperationException("未能获取Aes算法实例");
            }
            //设置模式为CBC
            aes.Mode = CipherMode.CBC;
            //设置Key大小
            aes.KeySize = 128;
            //设置填充
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;

            //创建解密器
            var de = aes.CreateDecryptor();
            //解密数据
            var decodeByteData = de.TransformFinalBlock(encryptData, 0, encryptData.Length);
            //转换为字符串
            var data = Encoding.UTF8.GetString(decodeByteData);

            return data;
        }
    }
}
