using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LixinCommon
{
    /// <summary>
    /// 淘宝IP地址库帮助类。
    /// 提供查询ip地址信息功能。
    /// 服务的主页:http://ip.taobao.com
    /// 作者：lixin
    /// 作者Email：lixin@lixin.me
    /// 日期：2014年3月30日
    /// 备注：为了保障服务正常运行，每个用户的访问频率需小于10qps。
    /// </summary>
    public class TaobaoIPHelper
    {
        /// <summary>
        /// 查询ip信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static TaobaoIP GetIP(string ip)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            try
            {
                string data = client.DownloadString("http://ip.taobao.com/service/getIpInfo.php?ip=" + ip);
                //当code==1时，表示失败
                if (data.IndexOf("\"code\":1") > -1)
                {
                    return new TaobaoIP()
                    {
                        code = 1,
                        errorMsg = data
                    };
                }
                using (System.IO.MemoryStream mm = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    System.Runtime.Serialization.Json.DataContractJsonSerializer myJson = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(TaobaoIP));
                    TaobaoIP item = (TaobaoIP)myJson.ReadObject(mm);

                    return item;
                }
            }
            catch (Exception ex)
            {
                return new TaobaoIP()
                {
                    code = 1,
                    errorMsg = ex.Message,
                    data = new TaobaoIP_Data() { }
                };
            }
        }
        /// <summary>
        /// 根据域名返回域名的ip信息
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static TaobaoIP[] GetDomain(string domain)
        {
            System.Net.IPAddress[] ips = System.Net.Dns.GetHostAddresses(domain);
            TaobaoIP[] result = new TaobaoIP[ips.Length];
            for (int i = 0, j = ips.Length; i < j; i++)
            {
                result[i] = GetIP(ips[i].ToString());
            }
            return result;
        }
    }
    public struct TaobaoIP
    {
        /// <summary>
        /// 0：成功；1：失败
        /// </summary>
        public int code;
        /// <summary>
        /// ip响应信息
        /// </summary>
        public TaobaoIP_Data data;
        /// <summary>
        /// 错误信息
        /// </summary>
        [NonSerialized]
        public string errorMsg;
    }
    public struct TaobaoIP_Data
    {
        /// <summary>
        /// ip地址
        /// </summary>
        public string ip;
        /// <summary>
        /// 国家
        /// </summary>
        public string country;
        /// <summary>
        /// 区域
        /// </summary>
        public string area;
        /// <summary>
        /// 省份
        /// </summary>
        public string region;
        /// <summary>
        /// 城市
        /// </summary>
        public string city;
        /// <summary>
        /// 运营商
        /// </summary>
        public string isp;
        public string country_id;
        public string area_id;
        public string region_id;
        public string city_id;
        public string county_id;
        public string isp_id;
    }
}
