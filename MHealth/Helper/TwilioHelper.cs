using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Jwt.AccessToken;

namespace MHealth.Helper
{
    public class TwilioHelper
    {
        public string GenerateCode( string username)
        {
            // These values are necessary for any access token
            const string twilioAccountSid = "AC861cc9e047e9ebed0cc411bffa22bb76";
            const string twilioApiKey = "SK3297d49d323e5231d0327403aed505e2";
            const string twilioApiSecret = "ApWMgQk3PJjkS8woWW3f212SuUy5u1R1";

            // These are specific to Video
            const string identity = "use1r";

            // Create a Video grant for this token
            var grant = new VideoGrant();
            grant.Room = "cool room";

            var grants = new HashSet<IGrant> { grant };

            // Create an Access Token generator
            var token = new Token(
                twilioAccountSid,
                twilioApiKey,
                twilioApiSecret,
                identity: username,
                grants: grants);

            //Console.WriteLine(token.ToJwt());
            return token.ToJwt();
        }



        public string GenerateCode(string username,Guid appointment_id)
        {
            // These values are necessary for any access token
            const string twilioAccountSid = "AC861cc9e047e9ebed0cc411bffa22bb76";
            const string twilioApiKey = "SK3297d49d323e5231d0327403aed505e2";
            const string twilioApiSecret = "ApWMgQk3PJjkS8woWW3f212SuUy5u1R1";

            // These are specific to Video
            const string identity = "use1r";

            // Create a Video grant for this token
            var grant = new VideoGrant();
            //grant.Room = "cool room";
            grant.Room = appointment_id.ToString();
            var grants = new HashSet<IGrant> { grant };

            // Create an Access Token generator
            var token = new Token(
                twilioAccountSid,
                twilioApiKey,
                twilioApiSecret,
                identity: username,
                grants: grants);

            //Console.WriteLine(token.ToJwt());
            return token.ToJwt();
        }
    }
}
