using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.Utilit
{
    public class SendSms
    {
        public static async Task<bool> AuthAccount(string PhoneNumber, string Massage)
        {
            var receptor = PhoneNumber;
            var message = Massage;
            var Temp = "DigiShahrAuthAccount";
            var api = new Kavenegar.KavenegarApi("4D4B66616C686B64534544333856706F7A6A35793647497735395A79496C59485644345257546C615137303D");
            api.VerifyLookup(receptor, Massage, Temp);

            return await Task.FromResult(true);
        }

        public static async Task<bool> SuccessDealOrder(string PhoneNumber, string Massage)
        {
            var receptor = PhoneNumber;
            var message = Massage;
            var Temp = "DigiShahrSuccessDealOrder";
            var api = new Kavenegar.KavenegarApi("4D4B66616C686B64534544333856706F7A6A35793647497735395A79496C59485644345257546C615137303D");
            api.VerifyLookup(receptor, Massage, Temp);

            return await Task.FromResult(true);
        }
    }
}
