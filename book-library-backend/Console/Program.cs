using BookLibraryApi.Helpers;
using System.Diagnostics;

for (int i = 0; i < 10; i++)
{
    var stop = Stopwatch.StartNew();
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjRjOWQ3NDY4LWVlNWYtNGJhZC05OWU2LTdkYzM3NWQ4NTA3NSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImVtYWlsQG1haWwucnUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNjU4NzA4NjY2LCJpc3MiOiJ0ZXN0QXBpIiwiYXVkIjoidGVzdFZ1ZSJ9.cvgJzh0NILo2l_deroSvnP3DqYuT05MSVS683yYDMkw";

    string result = JwtHelper.GetIdFromToken(token);

    stop.Stop();

    Console.WriteLine(result);
    Console.WriteLine(stop.Elapsed.ToString());
}