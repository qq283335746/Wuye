using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.IO;
using Newtonsoft.Json;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.DBUtility;
using TygaSoft.WebHelper;
using TygaSoft.CustomProvider;

namespace TygaSoft.Web.ScriptServices
{
    /// <summary>
    /// AdminService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class AdminService : System.Web.Services.WebService
    {

        #region 菜单导航

        [WebMethod]
        public string GetTreeJsonForMenu()
        {
            string[] roles = Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name);
            var roleList = roles.ToList();
            //if (roleList.Contains("Administrators"))
            //{
            //    roleList.Remove("Administrators");
            //}
            //roleList.Add("Users");
            SitemapHelper.Roles = roleList;
            return SitemapHelper.GetTreeJsonForMenu();
        }

        #endregion

        #region 系统日志

        #endregion

        #region 用户角色

        [WebMethod]
        public string SaveRole(RoleInfo model)
        {
            if (!HttpContext.Current.User.IsInRole("Administrators"))
            {
                return MessageContent.Role_InvalidError;
            }

            model.RoleName = model.RoleName.Trim();
            if (string.IsNullOrEmpty(model.RoleName))
            {
                return MessageContent.Submit_Params_InvalidError;
            }

            if (Roles.RoleExists(model.RoleName))
            {
                return MessageContent.Submit_Exist;
            }

            Guid gId = Guid.Empty;
            if (model.RoleId != null)
            {
                Guid.TryParse(model.RoleId.ToString(), out gId);
            }

            try
            {

                Role bll = new Role();

                if (!gId.Equals(Guid.Empty))
                {
                    bll.Update(model);
                }
                else
                {
                    Roles.CreateRole(model.RoleName);
                }

                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public string DelRole(string itemAppend)
        {
            if (!HttpContext.Current.User.IsInRole("Administrators"))
            {
                return MessageContent.Role_InvalidError;
            }

            itemAppend = itemAppend.Trim();
            if (string.IsNullOrEmpty(itemAppend))
            {
                return MessageContent.Submit_InvalidRow;
            }
            try
            {
                string[] roleIds = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in roleIds)
                {
                    Roles.DeleteRole(item);
                }

                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public string SaveIsLockedOut(string userName)
        {
            if (!HttpContext.Current.User.IsInRole("Administrators"))
            {
                return MessageContent.Role_InvalidError;
            }

            try
            {
                MembershipUser user = Membership.GetUser(userName);
                if (user == null)
                {
                    return "当前用户不存在，请检查";
                }
                if (user.IsLockedOut)
                {
                    if (user.UnlockUser())
                    {
                        return "0";
                    }
                    else
                    {
                        return "操作失败，请联系管理员";
                    }
                }

                return "只有“已锁定”的用户才能执行此操作";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public string SaveIsApproved(string userName)
        {
            if (!HttpContext.Current.User.IsInRole("Administrators"))
            {
                return MessageContent.Role_InvalidError;
            }

            try
            {
                MembershipUser user = Membership.GetUser(userName);
                if (user == null)
                {
                    return "当前用户不存在，请检查";
                }
                if (user.IsApproved)
                {
                    user.IsApproved = false;
                }
                else
                {
                    user.IsApproved = true;
                }

                Membership.UpdateUser(user);

                return user.IsApproved ? "1" : "0";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public string SaveUserInRole(string userName, string roleName, bool isInRole)
        {
            if (!HttpContext.Current.User.IsInRole("Administrators"))
            {
                return MessageContent.Role_InvalidError;
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                return MessageContent.GetString(MessageContent.Request_InvalidArgument, "用户名");
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return MessageContent.GetString(MessageContent.Request_InvalidArgument, "角色");
            }
            try
            {
                if (isInRole)
                {
                    if (!Roles.IsUserInRole(userName, roleName))
                    {
                        Roles.AddUserToRole(userName, roleName);
                    }
                }
                else
                {
                    if (Roles.IsUserInRole(userName, roleName))
                    {
                        Roles.RemoveUserFromRole(userName, roleName);
                    }
                }
                return "1";
            }
            catch (System.Configuration.Provider.ProviderException pex)
            {
                return pex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public string GetUserInRole(string userName)
        {
            try
            {
                string[] roles = Roles.GetRolesForUser(userName);
                if (roles.Length == 0) return "";

                return string.Join(",", roles);
            }
            catch (Exception ex)
            {
                return "异常：" + ex.Message;
            }
        }

        [WebMethod]
        public string DelUser(string userName)
        {
            if (!HttpContext.Current.User.IsInRole("Administrators"))
            {
                return MessageContent.Role_InvalidError;
            }

            try
            {
                Membership.DeleteUser(userName);
                return "1";
            }
            catch (Exception ex)
            {
                return "" + MessageContent.AlertTitle_Ex_Error + "：" + ex.Message;
            }
        }

        [WebMethod]
        public string SaveUser(UserInfo model)
        {
            if (!HttpContext.Current.User.IsInRole("Administrators"))
            {
                return MessageContent.Role_InvalidError;
            }

            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.Password != model.CfmPsw)
            {
                return MessageContent.Request_InvalidCompareToPassword;
            }
            model.UserName = model.UserName.Trim();
            model.Password = model.Password.Trim();
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                model.Email = model.UserName + "tygaweb.com";
            }

            string errMsg = "";

            try
            {
                model.RoleName = model.RoleName.Trim().Trim(',');
                string[] roles = null;
                if (!string.IsNullOrEmpty(model.RoleName))
                {
                    roles = model.RoleName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }

                MembershipCreateStatus status;
                MembershipUser user;

                using (TransactionScope scope = new TransactionScope())
                {
                    user = Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, model.IsApproved, out status);
                    if (roles != null && roles.Length > 0)
                    {
                        Roles.AddUserToRoles(model.UserName, roles);
                    }

                    scope.Complete();
                }

                if (user == null)
                {
                    return EnumMembershipCreateStatus.GetStatusMessage(status);
                }

                errMsg = "1";
            }
            catch (MembershipCreateUserException ex)
            {
                errMsg = EnumMembershipCreateStatus.GetStatusMessage(ex.StatusCode);
            }
            catch (HttpException ex)
            {
                errMsg = ex.Message;
            }

            return errMsg;
        }

        [WebMethod]
        public string ResetPassword(string username)
        {
            ResResult result = null;
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators"))
                {
                    result.ResCode = (int)ResResult.EnumCode.失败;
                    result.Message = MessageContent.Role_InvalidError;
                    return JsonConvert.SerializeObject(result);
                }

                result = new ResResult();
                if (!Membership.EnablePasswordReset)
                {
                    result.ResCode = (int)ResResult.EnumCode.失败;
                    result.Message = "系统不允许重置密码操作，请联系管理员";
                    return JsonConvert.SerializeObject(result);
                }
                var user = Membership.GetUser(username);
                if (user == null)
                {
                    result.ResCode = (int)ResResult.EnumCode.失败;
                    result.Message = "用户【" + username + "】不存在或已被删除，请检查";
                    return JsonConvert.SerializeObject(result);
                }
                string rndPsw = new Random().Next(100000, 999999).ToString();
                if (!user.ChangePassword(user.ResetPassword(), rndPsw))
                {
                    result.ResCode = (int)ResResult.EnumCode.失败;
                    result.Message = "重置密码失败，请稍后再重试";
                    return JsonConvert.SerializeObject(result);
                }
                result.ResCode = (int)ResResult.EnumCode.成功;
                result.Data = rndPsw;
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                result = new ResResult();
                result.ResCode = (int)ResResult.EnumCode.失败;
                result.Message = ex.Message;
                return JsonConvert.SerializeObject(result);
            }
        }

        #endregion

        #region 数据字典

        [WebMethod]
        public string GetJsonForSysEnum()
        {
            SysEnum bll = new SysEnum();
            return bll.GetTreeJson();
        }

        [WebMethod]
        public string GetJsonForDicCode()
        {
            try
            {
                return SysEnumDataProxy.GetJsonForEnumCode("DicCode");
            }
            catch (Exception ex)
            {
                return "异常：" + ex.Message;
            }
        }

        [WebMethod]
        public string SaveSysEnum(SysEnumInfo sysEnumModel)
        {
            if (string.IsNullOrWhiteSpace(sysEnumModel.EnumName))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (string.IsNullOrWhiteSpace(sysEnumModel.EnumCode))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (string.IsNullOrWhiteSpace(sysEnumModel.EnumValue))
            {
                return MessageContent.Submit_Params_InvalidError;
            }

            Guid gId = Guid.Empty;
            Guid.TryParse(sysEnumModel.Id.ToString(), out gId);
            sysEnumModel.Id = gId;

            Guid parentId = Guid.Empty;
            Guid.TryParse(sysEnumModel.ParentId.ToString(), out parentId);
            sysEnumModel.ParentId = parentId;

            SysEnum bll = new SysEnum();
            int effect = -1;

            if (!gId.Equals(Guid.Empty))
            {
                effect = bll.Update(sysEnumModel);
            }
            else
            {
                effect = bll.Insert(sysEnumModel);
            }

            if (effect == 110)
            {
                return MessageContent.Submit_Exist;
            }
            if (effect > 0)
            {
                return "1";
            }
            else return MessageContent.Submit_Error;
        }

        [WebMethod]
        public string DelSysEnum(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return MessageContent.Submit_InvalidRow;
            }
            Guid gId = Guid.Empty;
            Guid.TryParse(id, out gId);
            if (gId.Equals(Guid.Empty))
            {
                return MessageContent.GetString(MessageContent.Submit_Params_GetInvalidRegex, "对应标识值");
            }
            SysEnum bll = new SysEnum();
            bll.Delete(gId);
            return "1";
        }

        #endregion

        #region 省市区

        [WebMethod]
        public string GetJsonForProvinceCity()
        {
            ProvinceCity bll = new ProvinceCity();
            return bll.GetTreeJson();
        }

        [WebMethod]
        public string SaveProvinceCity(ProvinceCityInfo model)
        {
            if (string.IsNullOrWhiteSpace(model.Named))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (string.IsNullOrWhiteSpace(model.Pinyin))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (string.IsNullOrWhiteSpace(model.FirstChar))
            {
                return MessageContent.Submit_Params_InvalidError;
            }

            Guid gId = Guid.Empty;
            Guid.TryParse(model.Id.ToString(), out gId);
            model.Id = gId;

            Guid parentId = Guid.Empty;
            Guid.TryParse(model.ParentId.ToString(), out parentId);
            model.ParentId = parentId;
            model.LastUpdatedDate = DateTime.Now;

            ProvinceCity bll = new ProvinceCity();
            int effect = -1;

            if (!gId.Equals(Guid.Empty))
            {
                effect = bll.Update(model);
            }
            else
            {
                effect = bll.Insert(model);
            }

            if (effect == 110)
            {
                return MessageContent.Submit_Exist;
            }
            if (effect > 0)
            {
                return "1";
            }
            else return MessageContent.Submit_Error;
        }

        [WebMethod]
        public string DelProvinceCity(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return MessageContent.Submit_InvalidRow;
            }
            Guid gId = Guid.Empty;
            Guid.TryParse(id, out gId);
            if (gId.Equals(Guid.Empty))
            {
                return MessageContent.GetString(MessageContent.Submit_Params_GetInvalidRegex, "对应标识值");
            }
            ProvinceCity bll = new ProvinceCity();
            bll.Delete(gId);
            return "1";
        }

        #endregion

        #region 基础信息

        [WebMethod]
        public string SavePropertyCompany(PropertyCompanyInfo model)
        {
            if (string.IsNullOrWhiteSpace(model.CompanyName))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (string.IsNullOrWhiteSpace(model.ShortName))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            Guid gId = Guid.Empty;
            Guid.TryParse(model.Id.ToString(), out gId);
            model.Id = gId;
            Guid provinceCityId = Guid.Empty;
            Guid.TryParse(model.ProvinceCityId.ToString(), out provinceCityId);
            model.ProvinceCityId = provinceCityId;
            model.LastUpdatedDate = DateTime.Now;

            PropertyCompany bll = new PropertyCompany();
            int effect = -1;

            if (!gId.Equals(Guid.Empty))
            {
                effect = bll.Update(model);
            }
            else
            {
                effect = bll.Insert(model);
            }

            if (effect == 110)
            {
                return MessageContent.Submit_Exist;
            }
            if (effect > 0)
            {
                return "1";
            }
            else return MessageContent.Submit_Error;
        }

        [WebMethod]
        public string DelPropertyCompany(string itemsAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                itemsAppend = itemsAppend.Trim();
                if (string.IsNullOrEmpty(itemsAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemsAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                PropertyCompany bll = new PropertyCompany();
                bll.DeleteBatch(items.ToList<object>());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;
            }
            return "1";
        }

        [WebMethod]
        public string SaveResidenceCommunity(ResidenceCommunityInfo model)
        {
            if (string.IsNullOrWhiteSpace(model.CommunityName))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.PropertyCompanyId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.ProvinceCityId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            Guid gId = Guid.Empty;
            Guid.TryParse(model.Id.ToString(), out gId);
            model.Id = gId;
            Guid provinceCityId = Guid.Empty;
            Guid.TryParse(model.ProvinceCityId.ToString(), out provinceCityId);
            model.ProvinceCityId = provinceCityId;
            model.LastUpdatedDate = DateTime.Now;

            ResidenceCommunity bll = new ResidenceCommunity();
            int effect = -1;

            if (!gId.Equals(Guid.Empty))
            {
                effect = bll.Update(model);
            }
            else
            {
                effect = bll.Insert(model);
            }

            if (effect == 110)
            {
                return MessageContent.Submit_Exist;
            }
            if (effect > 0)
            {
                return "1";
            }
            else return MessageContent.Submit_Error;
        }

        [WebMethod]
        public string DelResidenceCommunity(string itemsAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                itemsAppend = itemsAppend.Trim();
                if (string.IsNullOrEmpty(itemsAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemsAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                ResidenceCommunity bll = new ResidenceCommunity();
                bll.DeleteBatch(items.ToList<object>());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;
            }
            return "1";
        }

        [WebMethod]
        public string SaveResidentialBuilding(ResidentialBuildingInfo model)
        {
            if (string.IsNullOrWhiteSpace(model.BuildingCode))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.PropertyCompanyId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.ResidenceCommunityId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            Guid gId = Guid.Empty;
            Guid.TryParse(model.Id.ToString(), out gId);
            model.Id = gId;
            model.LastUpdatedDate = DateTime.Now;

            ResidentialBuilding bll = new ResidentialBuilding();
            int effect = -1;

            if (!gId.Equals(Guid.Empty))
            {
                effect = bll.Update(model);
            }
            else
            {
                effect = bll.Insert(model);
            }

            if (effect == 110)
            {
                return MessageContent.Submit_Exist;
            }
            if (effect > 0)
            {
                return "1";
            }
            else return MessageContent.Submit_Error;
        }

        [WebMethod]
        public string DelResidentialBuilding(string itemsAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                itemsAppend = itemsAppend.Trim();
                if (string.IsNullOrEmpty(itemsAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemsAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                ResidentialBuilding bll = new ResidentialBuilding();
                bll.DeleteBatch(items.ToList<object>());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;
            }
            return "1";
        }

        [WebMethod]
        public string SaveResidentialUnit(ResidentialUnitInfo model)
        {
            if (string.IsNullOrWhiteSpace(model.UnitCode))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.PropertyCompanyId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.ResidenceCommunityId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.ResidentialBuildingId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            Guid gId = Guid.Empty;
            Guid.TryParse(model.Id.ToString(), out gId);
            model.Id = gId;
            model.LastUpdatedDate = DateTime.Now;

            ResidentialUnit bll = new ResidentialUnit();
            int effect = -1;

            if (!gId.Equals(Guid.Empty))
            {
                effect = bll.Update(model);
            }
            else
            {
                effect = bll.Insert(model);
            }

            if (effect == 110)
            {
                return MessageContent.Submit_Exist;
            }
            if (effect > 0)
            {
                return "1";
            }
            else return MessageContent.Submit_Error;
        }

        [WebMethod]
        public string DelResidentialUnit(string itemsAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                itemsAppend = itemsAppend.Trim();
                if (string.IsNullOrEmpty(itemsAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemsAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                ResidentialUnit bll = new ResidentialUnit();
                bll.DeleteBatch(items.ToList<object>());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;
            }
            return "1";
        }

        [WebMethod]
        public string SaveHouse(HouseInfo model)
        {
            if (string.IsNullOrWhiteSpace(model.HouseCode))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.PropertyCompanyId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.ResidenceCommunityId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.ResidentialBuildingId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.ResidentialUnitId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            Guid gId = Guid.Empty;
            Guid.TryParse(model.Id.ToString(), out gId);
            model.Id = gId;
            model.LastUpdatedDate = DateTime.Now;

            House bll = new House();
            int effect = -1;

            if (!gId.Equals(Guid.Empty))
            {
                effect = bll.Update(model);
            }
            else
            {
                effect = bll.Insert(model);
            }

            if (effect == 110)
            {
                return MessageContent.Submit_Exist;
            }
            if (effect > 0)
            {
                return "1";
            }
            else return MessageContent.Submit_Error;
        }

        [WebMethod]
        public string DelHouse(string itemsAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                itemsAppend = itemsAppend.Trim();
                if (string.IsNullOrEmpty(itemsAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemsAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                House bll = new House();
                bll.DeleteBatch(items.ToList<object>());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;
            }
            return "1";
        }

        [WebMethod]
        public string SaveHouseOwner(HouseOwnerInfo model)
        {
            if (string.IsNullOrWhiteSpace(model.HouseOwnerName))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.PropertyCompanyId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.ResidenceCommunityId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.ResidentialBuildingId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.ResidentialUnitId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (model.HouseId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            Guid gId = Guid.Empty;
            Guid.TryParse(model.Id.ToString(), out gId);
            model.Id = gId;
            model.LastUpdatedDate = DateTime.Now;

            HouseOwner bll = new HouseOwner();
            int effect = -1;

            if (!gId.Equals(Guid.Empty))
            {
                effect = bll.Update(model);
            }
            else
            {
                effect = bll.Insert(model);
            }

            if (effect == 110)
            {
                return MessageContent.Submit_Exist;
            }
            if (effect > 0)
            {
                return "1";
            }
            else return MessageContent.Submit_Error;
        }

        [WebMethod]
        public string DelHouseOwner(string itemsAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                itemsAppend = itemsAppend.Trim();
                if (string.IsNullOrEmpty(itemsAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemsAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                HouseOwner bll = new HouseOwner();
                bll.DeleteBatch(items.ToList<object>());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;
            }
            return "1";
        }

        [WebMethod]
        public string SaveUserHouseOwner(UserHouseOwnerInfo model)
        {
            if (model.HouseOwnerId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (string.IsNullOrWhiteSpace(model.UserName))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                return MessageContent.Submit_Params_InvalidError;
            }

            UserHouseOwner bll = new UserHouseOwner();
            int effect = -1;

            MembershipUser user = Membership.GetUser(model.UserName);
            if (user == null)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    user = Membership.CreateUser(model.UserName, model.Password);
                    model.UserId = user.ProviderUserKey;

                    effect = bll.Insert(model);

                    scope.Complete();
                }
            }
            else
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    user.ChangePassword(user.GetPassword(), model.Password);
                    effect = bll.Update(model);

                    scope.Complete();
                }
            }

            if (effect == 110)
            {
                return MessageContent.Submit_Exist;
            }
            if (effect > 0)
            {
                return "1";
            }
            else return MessageContent.Submit_Error;
        }

        [WebMethod]
        public string DelUserHouseOwner(string itemsAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                itemsAppend = itemsAppend.Trim();
                if (string.IsNullOrEmpty(itemsAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemsAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                UserHouseOwner bll = new UserHouseOwner();
                bll.DeleteBatch(items.ToList<object>());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;
            }
            return "1";
        }

        [WebMethod]
        public string DelHouseOwnerNotice(string itemsAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                itemsAppend = itemsAppend.Trim();
                if (string.IsNullOrEmpty(itemsAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemsAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                HouseOwnerNotice bll = new HouseOwnerNotice();
                bll.DeleteBatch(items.ToList<object>());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;
            }
            return "1";
        }

        [WebMethod]
        public string GetPasswordByRandom()
        {
            Random rdm = new Random();
            int len = rdm.Next(6,10);
            string psw = (rdm.NextDouble() * int.MaxValue).ToString().PadLeft(6,'0');
            if(psw.Length > len) psw = psw.Substring(0,len);
            return psw;
        }

        [WebMethod]
        public string SaveHouseOwnerNotice(HouseOwnerNoticeInfo model)
        {
            if (string.IsNullOrWhiteSpace(model.OwnerAppend))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (string.IsNullOrWhiteSpace(model.NoticeAppend))
            {
                return MessageContent.Submit_Params_InvalidError;
            }

            string[] ownerItems = model.OwnerAppend.Trim(',').Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries);
            string[] noticeItems = model.NoticeAppend.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            ResidenceCommunity rcBll = new ResidenceCommunity();
            ResidentialBuilding rbBll = new ResidentialBuilding();
            ResidentialUnit ruBll = new ResidentialUnit();
            House hBll = new House();
            HouseOwner hoBll = new HouseOwner();

            List<string> hoList = new List<string>();
            DataTable honDt = new DataTable();
            honDt.Columns.Add(new DataColumn("Id", typeof(Guid)));
            honDt.Columns.Add(new DataColumn("HouseOwnerId", typeof(Guid)));
            honDt.Columns.Add(new DataColumn("NoticeId", typeof(Guid)));
            honDt.Columns.Add(new DataColumn("Status", typeof(byte)));
            honDt.Columns.Add(new DataColumn("IsRead", typeof(bool)));
            honDt.Columns.Add(new DataColumn("LastUpdatedDate", typeof(DateTime)));

            #region 对应送达目标

            switch (model.ReachType)
            {
                case 0:
                    foreach (string companyItem in ownerItems)
                    {
                        var communityArr = rcBll.GetCommunityByCompanyId(companyItem);
                        if (communityArr == null || communityArr.Count() == 0) continue;
                        foreach (string communityItem in communityArr)
                        {
                            var buildingArr = rbBll.GetBuildingByCommunityId(communityItem);
                            if (buildingArr == null || buildingArr.Count() == 0) continue;
                            foreach (var buildingItem in buildingArr)
                            {
                                var unitArr = ruBll.GetUnitByBuildingId(buildingItem);
                                if (unitArr == null || unitArr.Count() == 0) continue;
                                foreach (var unitItem in unitArr)
                                {
                                    var houseArr = hBll.GetHouseByUnitId(unitItem);
                                    if (houseArr == null || houseArr.Count() == 0) continue;
                                    foreach (var houseItem in houseArr)
                                    {
                                        var houseOwnerArr = hoBll.GetHouseOwnerByHouseId(houseItem);
                                        if (houseOwnerArr == null || houseOwnerArr.Count() == 0) continue;
                                        foreach (var houseOwnerItem in houseOwnerArr)
                                        {
                                            hoList.Add(houseOwnerItem);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 1:
                    foreach (string communityItem in ownerItems)
                    {
                        var buildingArr = rbBll.GetBuildingByCommunityId(communityItem);
                        if (buildingArr == null || buildingArr.Count() == 0) continue;
                        foreach (var buildingItem in buildingArr)
                        {
                            var unitArr = ruBll.GetUnitByBuildingId(buildingItem);
                            if (unitArr == null || unitArr.Count() == 0) continue;
                            foreach (var unitItem in unitArr)
                            {
                                var houseArr = hBll.GetHouseByUnitId(unitItem);
                                if (houseArr == null || houseArr.Count() == 0) continue;
                                foreach (var houseItem in houseArr)
                                {
                                    var houseOwnerArr = hoBll.GetHouseOwnerByHouseId(houseItem);
                                    if (houseOwnerArr == null || houseOwnerArr.Count() == 0) continue;
                                    foreach (var houseOwnerItem in houseOwnerArr)
                                    {
                                        hoList.Add(houseOwnerItem);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    foreach (var buildingItem in ownerItems)
                    {
                        var unitArr = ruBll.GetUnitByBuildingId(buildingItem);
                        if (unitArr == null || unitArr.Count() == 0) continue;
                        foreach (var unitItem in unitArr)
                        {
                            var houseArr = hBll.GetHouseByUnitId(unitItem);
                            if (houseArr == null || houseArr.Count() == 0) continue;
                            foreach (var houseItem in houseArr)
                            {
                                var houseOwnerArr = hoBll.GetHouseOwnerByHouseId(houseItem);
                                if (houseOwnerArr == null || houseOwnerArr.Count() == 0) continue;
                                foreach (var houseOwnerItem in houseOwnerArr)
                                {
                                    hoList.Add(houseOwnerItem);
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    foreach (var unitItem in ownerItems)
                    {
                        var houseArr = hBll.GetHouseByUnitId(unitItem);
                        if (houseArr == null || houseArr.Count() == 0) continue;
                        foreach (var houseItem in houseArr)
                        {
                            var houseOwnerArr = hoBll.GetHouseOwnerByHouseId(houseItem);
                            if (houseOwnerArr == null || houseOwnerArr.Count() == 0) continue;
                            foreach (var houseOwnerItem in houseOwnerArr)
                            {
                                hoList.Add(houseOwnerItem);
                            }
                        }
                    }
                    break;
                case 4:
                    foreach (var houseItem in ownerItems)
                    {
                        var houseOwnerArr = hoBll.GetHouseOwnerByHouseId(houseItem);
                        if (houseOwnerArr == null || houseOwnerArr.Count() == 0) continue;
                        foreach (var houseOwnerItem in houseOwnerArr)
                        {
                            hoList.Add(houseOwnerItem);
                        }
                    }
                    break;
                case 5:
                    foreach (var houseOwnerItem in ownerItems)
                    {
                        hoList.Add(houseOwnerItem);
                    }
                    break;
                default:
                    break;
            }

            #endregion

            if (hoList.Count() == 0)
            {
                return "不存在任何业主，无法下发通知，请检查";
            }

            foreach (var noticeItem in noticeItems)
            {
                foreach (var houseOwnerItem in hoList)
                {
                    DataRow dr = honDt.NewRow();
                    dr["Id"] = Guid.NewGuid();
                    dr["HouseOwnerId"] = Guid.Parse(houseOwnerItem);
                    dr["NoticeId"] = Guid.Parse(noticeItem);
                    dr["Status"] = 1;
                    dr["IsRead"] = false;
                    dr["LastUpdatedDate"] = DateTime.Now;

                    honDt.Rows.Add(dr);
                   
                }
            }

            HouseOwnerNotice honBll = new HouseOwnerNotice();
            honBll.BulkCopy(honDt);

            return "1";
        }

        #endregion

        #region 投诉保修

        [WebMethod]
        public string SaveComplainRepair(ComplainRepairInfo model)
        {
            if (model.SysEnumId.Equals(Guid.Empty))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (string.IsNullOrWhiteSpace(model.Phone))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (string.IsNullOrWhiteSpace(model.Descri))
            {
                return MessageContent.Submit_Params_InvalidError;
            }

            model.UserId = WebCommon.GetUserId();

            Guid gId = Guid.Empty;
            Guid.TryParse(model.Id.ToString(), out gId);
            model.Id = gId;
            model.LastUpdatedDate = DateTime.Now;

            ComplainRepair bll = new ComplainRepair();
            int effect = -1;

            if (!gId.Equals(Guid.Empty))
            {
                effect = bll.Update(model);
            }
            else
            {
                effect = bll.Insert(model);
            }

            if (effect == 110)
            {
                return MessageContent.Submit_Exist;
            }
            if (effect > 0)
            {
                return "1";
            }
            else return MessageContent.Submit_Error;
        }

        [WebMethod]
        public string DelComplainRepair(string itemAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                itemAppend = itemAppend.Trim();
                if (string.IsNullOrEmpty(itemAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                ComplainRepair bll = new ComplainRepair();
                bll.DeleteBatch(items.ToList<object>());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;
            }
            return "1";
        }

        [WebMethod]
        public string DealStatusBatch(string itemAppend, string statusAppend)
        {
            string errorMsg = "";

            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend))
                {
                    return MessageContent.Submit_Params_InvalidError;
                }
                if (string.IsNullOrWhiteSpace(statusAppend))
                {
                    return MessageContent.Submit_Params_InvalidError;
                }

                string[] idArr = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] statusArr = statusAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                Dictionary<Guid, int> dic = new Dictionary<Guid, int>();
                for (int i = 0; i < idArr.Length; i++)
                {
                    dic.Add(Guid.Parse(idArr[i]), int.Parse(statusArr[i])+1);
                }

                ComplainRepair crBll = new ComplainRepair();
                crBll.DealStatusBatch(dic);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }

            if (!string.IsNullOrEmpty(errorMsg)) return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;

            return "1";
        }

        #endregion

        #region 类别字典

        [WebMethod]
        public string GetJsonForContentType()
        {
            ContentType bll = new ContentType();
            return bll.GetTreeJson();
        }

        [WebMethod]
        public string SaveContentType(ContentTypeInfo model)
        {
            if (string.IsNullOrWhiteSpace(model.TypeName))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (string.IsNullOrWhiteSpace(model.TypeCode))
            {
                return MessageContent.Submit_Params_InvalidError;
            }
            if (string.IsNullOrWhiteSpace(model.TypeValue))
            {
                return MessageContent.Submit_Params_InvalidError;
            }

            Guid gId = Guid.Empty;
            Guid.TryParse(model.Id.ToString(), out gId);
            model.Id = gId;

            Guid parentId = Guid.Empty;
            Guid.TryParse(model.ParentId.ToString(), out parentId);
            model.ParentId = parentId;
            model.LastUpdatedDate = DateTime.Now;

            ContentType bll = new ContentType();
            int effect = -1;

            if (!gId.Equals(Guid.Empty))
            {
                effect = bll.Update(model);
            }
            else
            {
                effect = bll.Insert(model);
            }

            if (effect == 110)
            {
                return MessageContent.Submit_Exist;
            }
            if (effect > 0)
            {
                return "1";
            }
            else return MessageContent.Submit_Error;
        }

        [WebMethod]
        public string DelContentType(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return MessageContent.Submit_InvalidRow;
            }
            Guid gId = Guid.Empty;
            Guid.TryParse(id, out gId);
            if (gId.Equals(Guid.Empty))
            {
                return MessageContent.GetString(MessageContent.Submit_Params_GetInvalidRegex, "对应标识值");
            }
            ContentType bll = new ContentType();
            bll.Delete(gId);
            return "1";
        }

        [WebMethod]
        public string GetJsonForContentTypeByTypeCode(string typeCode)
        {
            ContentType bll = new ContentType();
            return bll.GetTreeJsonForContentTypeByTypeCode(typeCode);
        }

        #endregion

        #region 公告资讯广告

        [WebMethod]
        public string DelAnnouncement(string itemsAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                itemsAppend = itemsAppend.Trim();
                if (string.IsNullOrEmpty(itemsAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemsAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                Announcement bll = new Announcement();
                bll.DeleteBatch(items.ToList<object>());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;
            }
            return "1";
        }

        [WebMethod]
        public string DelNotice(string itemsAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                itemsAppend = itemsAppend.Trim();
                if (string.IsNullOrEmpty(itemsAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemsAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                Notice bll = new Notice();
                bll.DeleteBatch(items.ToList<object>());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;
            }
            return "1";
        }

        [WebMethod]
        public string DelAdvertisement(string itemAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                itemAppend = itemAppend.Trim();
                if (string.IsNullOrEmpty(itemAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                IList<object> list = items.ToList<object>();

                Advertisement bll = new Advertisement();
                if (bll.DeleteBatchByJoin(list))
                {
                    return "1";
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return MessageContent.AlertTitle_Ex_Error + "：" + errorMsg;
            }
            return "1";
        }

        [WebMethod]
        public string DelPictureAdvertisement(string itemAppend)
        {
            string errorMsg = string.Empty;
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators"))
                {
                    return MessageContent.Role_InvalidError;
                }

                itemAppend = itemAppend.Trim();
                if (string.IsNullOrEmpty(itemAppend))
                {
                    return MessageContent.Submit_InvalidRow;
                }

                string[] items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string inIds = "";
                Guid gId = Guid.Empty;
                foreach (string item in items)
                {
                    if (!Guid.TryParse(item, out gId))
                    {
                        throw new ArgumentException(MessageContent.GetString(MessageContent.Submit_Params_GetInvalidRegex, item));
                    }
                    inIds += string.Format("'{0}',", item);
                }

                PictureAdvertisement bll = new PictureAdvertisement();
                var list = bll.GetList(" and Id in (" + inIds.Trim(',') + ")");
                if (list != null || list.Count() > 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        foreach (var model in list)
                        {
                            string dir = Server.MapPath("~" + string.Format("{0}{1}", model.FileDirectory, model.RandomFolder));

                            if (Directory.Exists(dir))
                            {
                                string[] subDirArr = Directory.GetDirectories(dir);
                                if (subDirArr != null)
                                {
                                    foreach (string subDir in subDirArr)
                                    {
                                        string[] dirFiles = Directory.GetFiles(subDir);
                                        foreach (string file in dirFiles)
                                        {
                                            if (File.Exists(file))
                                            {
                                                File.Delete(file);
                                            }
                                        }
                                        Directory.Delete(subDir, true);
                                    }
                                }
                                Directory.Delete(dir, true);
                            }
                            dir = Server.MapPath("~" + string.Format("{0}{1}", model.FileDirectory, model.FileName));
                            if (File.Exists(dir))
                            {
                                File.Delete(dir);
                            }

                        }

                        bll.DeleteBatch(items.ToList<object>());

                        scope.Complete();
                    }
                }

                return "1";
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }

            return MessageContent.GetString(MessageContent.Submit_Ex_Error, errorMsg);
        }

        #endregion
    }
}
