using Backend.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Controllers
{
    public class IdentityController : ControllerBase
    {
        [HttpPost]
        [Route("/[controller]/[action]")]
        public ActionResult<LoginResultModel> Login([FromBody] UserPasswordModel input)
        {
            var hashAlgorithm = SHA512.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(input.Password);
            var passwordHash = BitConverter.ToString(hashAlgorithm.ComputeHash(passwordBytes)).Replace("-", "").ToLower();

            // checking master password
            if (passwordHash != "e0469addd8d57a3623494096dabc19bebca1a038c9da696940b3f853d106a6ecfa5bd60ce8e72884efa3bd92b930da178fd616f40facad654212d7c2f8817dd4")
            {
                // login unsuccessful
                var failModel = new LoginResultModel { UserName = input.Email, HasSucceeded = false, Message = "Login failed" };
                return failModel;
            }

            // successful login
            var successModel = new LoginResultModel { UserName = input.Email, HasSucceeded = true, Message = "You have successfully logged in." };
            return successModel;
        }


    }
}