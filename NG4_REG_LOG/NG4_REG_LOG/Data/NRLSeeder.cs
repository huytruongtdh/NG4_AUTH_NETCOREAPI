using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NG4_REG_LOG.Data
{
    public class NRLSeeder
    {
        //private readonly NRLDbContext _ctx;
        //public NRLSeeder(NRLDbContext ctx)
        //{
        //    _ctx = ctx;
        //}



        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger<LoginModel> _logger;
        //private readonly IEmailSender _emailSender;

        public NRLSeeder(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            //ILogger<LoginModel> logger,
            //IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_logger = logger;
            //_emailSender = emailSender;
        }

        //public void Seed()
        //{
        //    //_ctx.Database.EnsureCreated();

        //    if (_userManager.Users.Count() <= 1)
        //    {
        //        // need to create sample data
        //        var existedUserNames = _userManager.Users
        //            .Select(s => s.UserName)
        //            .ToList();

        //        for (int i = 0; i < 50; i++)
        //        {
        //            string ranStr = GenerateUniqueRandString(existedUserNames);
        //            string email = ranStr + "@gmail.com";
        //            string password = "P@ssw0rd";

        //            var user = new ApplicationUser { UserName = ranStr, Email = email };
        //            var result = _userManager.CreateAsync(user, password).Result;

        //            if (result.Succeeded)
        //            {

        //                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //                var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
        //                await _emailSender.SendEmailConfirmationAsync(Input.Email, callbackUrl);

        //                await _signInManager.SignInAsync(user, isPersistent: false);
        //                return LocalRedirect(Url.GetLocalUrl(returnUrl));
        //            }

        //            existedUserNames.Add(ranStr);
        //        }
        //    }
        //}

        #region Ultilities


        private string GenerateUniqueRandString(List<string> existedUserNames)
        {
            string randStr = RandomString(5);

            if (existedUserNames.Contains(randStr))
                return GenerateUniqueRandString(existedUserNames);

            return randStr;
        }

        const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
        Random rand = new Random();
        private string RandomString(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz";
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var c = pool[rand.Next(0, pool.Length)];
                builder.Append(c);
            }

            return builder.ToString();
        }


        //private string RandomString2(int length)
        //{
        //    var chars = Enumerable.Range(0, length)
        //        .Select(x => pool[rand.Next(0, pool.Length)]);
        //    return new string(chars.ToArray());
        //}

        #endregion
    }
}
