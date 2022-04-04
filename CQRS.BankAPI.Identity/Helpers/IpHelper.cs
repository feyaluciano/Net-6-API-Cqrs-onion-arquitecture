using System.Net;

namespace CQRS.BankAPI.Identity.Helpers
{
    public class IpHelper
    {
        public static string GetIpAddress()
        {

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {

                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }    
            }
            return string.Empty;
        }
    }
}
