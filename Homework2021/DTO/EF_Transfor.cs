using Homework2021.Content;
using Homework2021.EFORM;
using Homework2021.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.DTO
{
    public class EF_Transfor
    {
        protected User Transfor(EF_User DataEntry)
        {
            
            return new User()
            {
                Id=DataEntry.Id,
                UserName=DataEntry.UserName,
                UserId=DataEntry.UserId,
                Password=DataEntry.Password,
                Enable=DataEntry.Enable,
                CreateDate=DataEntry.CreateDate,
                ModifyDate=DataEntry.ModifyDate
            };
        }
        protected Group Transfor(EF_Group DataEntry)
        {
            return new Group()
            {
                Id=DataEntry.Id,
                GroupName=DataEntry.GroupName,
                Desc=DataEntry.Desc,
                CreateDate=DataEntry.CreateDate,
                ModifyDate=DataEntry.ModifyDate
            };
        }

        protected UserRefGroup Transfor(EF_UserrefGroup DataEntry)
        {
            return new UserRefGroup()
            {
                Id=DataEntry.Id,
                UserId=DataEntry.User_Id,
                GroupId=DataEntry.Group_Id,
                CreateDate=DataEntry.CreateDate,
                ModifyDate=DataEntry.ModifyDate
            };
        }
        protected Customer Transfor(EF_Customer DataEntry)
        {
            return new Customer()
            {
                Id = DataEntry.Id,
                Name = DataEntry.Name,
                Email = DataEntry.Email,
                Address = DataEntry.Address,
                Company = DataEntry.Company,
                Phone = DataEntry.Phone,
                UserId = DataEntry.UserId,
                CreateDate = DataEntry.CreateDate,
                ModifyDate = DataEntry.ModifyDate
            };
        }
    }
}
