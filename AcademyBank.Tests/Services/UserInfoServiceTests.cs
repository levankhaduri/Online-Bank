using AcademyBank.BLL.Services.Implementations;
using AcademyBank.Models;
using AcademyBank.Tests.Mocks.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.Tests.Services
{
    [TestClass]
    public class UserInfoServiceTests
    {
        [TestMethod]
        public void Create()
        {
            //Arrange
            var userInfo = new UserInfo() { Id = 1 };
            var userInfoRepository = new MockIRepository<UserInfo>()
                .MockCreate(userInfo);
            var userInfoService = new UserInfoService(userInfoRepository.Object);

            //Act
            var createdUserInfo = userInfoService.Create(userInfo);

            //Assert
            Assert.IsNotNull(createdUserInfo);
        }
        [TestMethod]
        public void Get()
        {
            //Arrange
            var userInfos = new List<UserInfo>() { new UserInfo() { Id = 1 } };
            var userInfoRepository = new MockIRepository<UserInfo>()
                .MockGet(userInfos);
            var userInfoService = new UserInfoService(userInfoRepository.Object);

            //Act
            var getUserInfos = userInfoService.Get();

            //Assert
            Assert.IsNotNull(getUserInfos);
        }
        [TestMethod]
        public void GetById()
        {
            //Arrange
            var userInfo = new UserInfo() { Id = 1 };
            var userInfoRepository = new MockIRepository<UserInfo>()
                .MockGetById(userInfo);
            var userInfoService = new UserInfoService(userInfoRepository.Object);

            //Act
            var userInfoById = userInfoService.GetById(1);

            //Assert
            Assert.IsNotNull(userInfoById);
        }
        [TestMethod]
        public void Update()
        {
            //Arrange
            var userInfo = new UserInfo() { Id = 1 };
            var userInfoRepository = new MockIRepository<UserInfo>()
                .MockUpdate(userInfo);
            var userInfoService = new UserInfoService(userInfoRepository.Object);

            //Act
            var updatedUserInfo = userInfoService.Update(userInfo);

            //Assert
            Assert.IsNotNull(updatedUserInfo);
        }
    }
}
