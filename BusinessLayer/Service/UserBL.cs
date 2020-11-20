using BusinessLayer.Interface;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UserBL : IUsersBL
    {

        /// <summary>
        /// RL Reference.
        /// </summary>
        private IUserRL userRL;

        /// <summary>
        /// Constructor For Setting UserRL Instance.
        /// </summary>
        /// <param name="userRL"></param>
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        /// <summary>
        /// Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RUserRegisterModel> RegisterUser(UserRegisterModel user)
        {
            try
            {
                return await this.userRL.RegisterUser(user);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

       public async Task<RUserLoginModel> UserLogin(UserLoginModel user)
        {
            try
            {
                return await this.userRL.UserLogin(user);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        public RUserLoginModel ForgetPassword(String EmailId)
        {
            try
            {
                return this.userRL.ForgetPassword(EmailId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public RUserLoginModel ResetPassword(string PassWord,  string email)
        {
            try
            {
                return this.userRL.ResetPassword(PassWord, email);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
