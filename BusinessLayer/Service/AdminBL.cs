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
    public class AdminBL : IAdminBL
    {

        /// <summary>
        /// RL Reference.
        /// </summary>
        private IAdminRL adminRL;

        /// <summary>
        /// Constructor For Setting UserRL Instance.
        /// </summary>
        /// <param name="userRL"></param>
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        /// <summary>
        /// Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RAdminRegisterModel> RegisterAdmin(AdminRegisterModel user)
        {
            try
            {
                return await this.adminRL.RegisterAdmin(user);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<RAdminLoginModel> AdminLogin(AdminLoginModel adminLoginModel)
        {
            try
            {
                return await this.adminRL.AdminLogin(adminLoginModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public RAdminLoginModel ForgetPassword(String EmailId)
        {
            try
            {
                return this.adminRL.ForgetPassword(EmailId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public RAdminLoginModel ResetPassword(string PassWord, string email)
        {
            try
            {
                return this.adminRL.ResetPassword(PassWord, email);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}
