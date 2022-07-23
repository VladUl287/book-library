using BookLibraryApi.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTestJwt
    {
        [TestMethod]
        public void TestGetIdFromToken()
        {
            // arrange 
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjRjOWQ3NDY4LWVlNWYtNGJhZC05OWU2LTdkYzM3NWQ4NTA3NSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImVtYWlsQG1haWwucnUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNjU4NzA4NjY2LCJpc3MiOiJ0ZXN0QXBpIiwiYXVkIjoidGVzdFZ1ZSJ9.cvgJzh0NILo2l_deroSvnP3DqYuT05MSVS683yYDMkw";
            // act
            string result = JwtHelper.GetIdFromToken(token);
            // assert            
            Assert.AreEqual("4c9d7468-ee5f-4bad-99e6-7dc375d85075", result);
        }
    }
}