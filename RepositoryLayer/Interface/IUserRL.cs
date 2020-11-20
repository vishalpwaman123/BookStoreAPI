using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {

        /// <summary>
        /// Abstact Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<RUserRegisterModel> RegisterUser(UserRegisterModel user);

        /// <summary>
        /// Abstact Function For User Login.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<RUserLoginModel> UserLogin(UserLoginModel userLoginModel);

        /// <summary>
        /// Abstact Function For User Login.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        RUserLoginModel ForgetPassword(String EmailId);

        /// <summary>
        /// Abstact Function For User Login.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        RUserLoginModel ResetPassword(string PassWord,  string email);

    }
}
