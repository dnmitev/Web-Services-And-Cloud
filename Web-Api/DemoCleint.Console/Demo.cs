namespace DemoCleint.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;

    using StudentSystem.Models;

    internal class Demo
    {
        private static void Main()
        {
            var client = new HttpClient
            {
                // the Uri can be diffetent running this on other PC, note this while seeing my HW
                BaseAddress = new Uri("http://localhost:57748/")
            };

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Students/All").Result;
            if (response.IsSuccessStatusCode)
            {
                // NOTE: .ReadAsAsync<>() cames from NuGet package System.Net.Http.Formatting
                // http://stackoverflow.com/questions/10399324/where-is-httpcontent-readasasync
                var students = response.Content.ReadAsAsync<IEnumerable<Student>>().Result;

                foreach (var st in students)
                {
                    Console.WriteLine(string.Format("{0} {1}", st.FirstName, st.LastName));
                }
            }
        }
    }
}