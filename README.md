这是一个通过调用淘宝ip地址库实现ip地址查询的功能类。
用法很简单，示例代码如下：
            TaobaoIP result1 = TaobaoIPHelper.GetIP("8.8.8.8");
            if (result1.code == 0)
            {
                Console.WriteLine(string.Format("调用成功。该ip的国家为{0},省份为{1},城市为{2},运营商为{3}"
                    , result1.data.country, result1.data.region, result1.data.city, result1.data.isp)
                    );
            }
            else
            {
                Console.WriteLine("失败，原因为："+result1.errorMsg);
            }
            TaobaoIP[] result2 = TaobaoIPHelper.GetDomain("lixin.me");
            foreach (TaobaoIP item in result2)
            {
                if (item.code == 0)
                {
                    Console.WriteLine(string.Format("调用成功。该ip的国家为{0},省份为{1},城市为{2},运营商为{3}"
                    , item.data.country, item.data.region, item.data.city, item.data.isp)
                    );
                }
                else
                {
                    Console.WriteLine("失败，原因为：" + item.errorMsg);
                }
            }
