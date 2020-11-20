using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAdminRL
    {

        /// <summary>
        /// Abstact Function For Register Admin.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<RAdminRegisterModel> RegisterAdmin(AdminRegisterModel adminRegisterModel);

        /// <summary>
        /// Abstact Function For Login Admin.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<RAdminLoginModel> AdminLogin(AdminLoginModel adminLoginModel);

        /// <summary>
        /// Abstact Function For Admin Login.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        RAdminLoginModel ForgetPassword(String EmailId);

        /// <summary>
        /// Abstact Function For Admin Login.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        RAdminLoginModel ResetPassword(string PassWord, string email);

    }
}
