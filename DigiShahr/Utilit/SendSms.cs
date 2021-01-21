using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.Utilit
{
    public class SendSms
    {
        public static async Task<bool> Send(string PhoneNumber, string Massage, string Temp)
        {
            var api = new Kavenegar.KavenegarApi("4D4B66616C686B64534544333856706F7A6A35793647497735395A79496C59485644345257546C615137303D");
            api.VerifyLookup(PhoneNumber, Massage, Temp);

            return await Task.FromResult(true);
        }
    }
}
