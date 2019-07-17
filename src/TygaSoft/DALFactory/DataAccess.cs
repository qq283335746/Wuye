using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using TygaSoft.IDAL;

namespace TygaSoft.DALFactory
{
    public sealed class DataAccess
    {
        private static readonly string[] paths = ConfigurationManager.AppSettings["WebDAL"].Split(',');

        public static IPropertyCompany CreatePropertyCompany()
        {
            string className = paths[0] + ".PropertyCompany";
            return (IPropertyCompany)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IResidenceCommunity CreateResidenceCommunity()
        {
            string className = paths[0] + ".ResidenceCommunity";
            return (IResidenceCommunity)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IResidentialBuilding CreateResidentialBuilding()
        {
            string className = paths[0] + ".ResidentialBuilding";
            return (IResidentialBuilding)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IResidentialUnit CreateResidentialUnit()
        {
            string className = paths[0] + ".ResidentialUnit";
            return (IResidentialUnit)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IHouse CreateHouse()
        {
            string className = paths[0] + ".House";
            return (IHouse)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IHouseOwner CreateHouseOwner()
        {
            string className = paths[0] + ".HouseOwner";
            return (IHouseOwner)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IUserHouseOwner CreateUserHouseOwner()
        {
            string className = paths[0] + ".UserHouseOwner";
            return (IUserHouseOwner)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IHouseOwnerNotice CreateHouseOwnerNotice()
        {
            string className = paths[0] + ".HouseOwnerNotice";
            return (IHouseOwnerNotice)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IComplainRepair CreateComplainRepair()
        {
            string className = paths[0] + ".ComplainRepair";
            return (IComplainRepair)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static INotice CreateNotice()
        {
            string className = paths[0] + ".Notice";
            return (INotice)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IAnnouncement CreateAnnouncement()
        {
            string className = paths[0] + ".Announcement";
            return (IAnnouncement)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IAdvertisement CreateAdvertisement()
        {
            string className = paths[0] + ".Advertisement";
            return (IAdvertisement)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IAdvertisementItem CreateAdvertisementItem()
        {
            string className = paths[0] + ".AdvertisementItem";
            return (IAdvertisementItem)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IAdvertisementLink CreateAdvertisementLink()
        {
            string className = paths[0] + ".AdvertisementLink";
            return (IAdvertisementLink)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IPictureAdvertisement CreatePictureAdvertisement()
        {
            string className = paths[0] + ".PictureAdvertisement";
            return (IPictureAdvertisement)Assembly.Load(paths[1]).CreateInstance(className);
        } 

        public static IContentType CreateContentType()
        {
            string className = paths[0] + ".ContentType";
            return (IContentType)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IContentDetail CreateContentDetail()
        {
            string className = paths[0] + ".ContentDetail";
            return (IContentDetail)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IContentPicture CreateContentPicture()
        {
            string className = paths[0] + ".ContentPicture";
            return (IContentPicture)Assembly.Load(paths[1]).CreateInstance(className);
        } 

        public static IProvinceCity CreateProvinceCity()
        {
            string className = paths[0] + ".ProvinceCity";
            return (IProvinceCity)Assembly.Load(paths[1]).CreateInstance(className);
        } 

        public static ISysEnum CreateSysEnum()
        {
            string className = paths[0] + ".SysEnum";
            return (ISysEnum)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IRole CreateRole()
        {
            string className = paths[0] + ".Role";
            return (IRole)Assembly.Load(paths[1]).CreateInstance(className);
        }

    }
}
