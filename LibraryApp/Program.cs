using LibraryApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp
{
    public class Program
    {


        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<BookContext>();

                GenerateBookData(context);
                GenerateAuthorData(context,2);
                GenerateCustomerData(context);


                context.SaveChanges();
            }

            host.Run();
        }
        /// <summary>
        /// Generate Books
        /// </summary>
        /// <param name="context"></param>
        private static void GenerateBookData(BookContext context)
        {

            if (context.Books.Any())
                return;

            var book1 = new Book
            {
                Name = "testi kirja",
                Description = "testi elokuvan desc...",
                AdditionalInformation = "Testi elokuvan addinfo...",
                Publishers = new List<Publisher>() { new Publisher { Name = "suomenkirjapaino", Where = "jyväskylä" } },
                Isbn = "0103930191230",
                NumberOfCopies = 12,
                Categories = new List<Category>() { new Category { Name="komedia"} },
                Year =2012,
                Language="Suomi"
         



            };



            context.Books.Add(book1);

            var book2 = new Book
            {
                Name = "testi kirja 2",
                Description = "testi elokuvan 2 desc...",
                AdditionalInformation = "Testi elokuvan 2 addinfo...",
                Publishers = new List<Publisher>() { new Publisher { Name = "helsinkinginkirjapaino", Where = "helsinki" } },
                Isbn = "01334812381",
                NumberOfCopies = 6,
                Categories = new List<Category>() { new Category { Name = "kauhu" } },
                Year =2012,
                Language = "Suomi"


            };



            context.Books.Add(book2);
        }
        
        
        /// <summary>
        /// Generates Authors To given Book
        /// </summary>
        /// <param name="context"></param>
        /// <param name="bookId"></param>
        private static void GenerateAuthorData(BookContext context, long bookId)
 
        {
            if (context.Person.Any())
                return;

            var authorPerson = new Person();
            authorPerson.FirstName = "Kati";
            authorPerson.LastName = "Kirjailija";


            context.Person.Add(authorPerson);

            var author = new Author();
            author.BookId = bookId;
            author.PersonId = authorPerson.Id;
            authorPerson.Author = author;

            context.Author.Add(author);

           
        }

        /// <summary>
        /// Generates CustomerData 
        /// </summary>
        /// <param name="context"></param>
        private static void GenerateCustomerData(BookContext context)
        {
            if (context.Customers.Any())
                return;
            var customer = new Customer
            {
                FirstName = "Aatu",
                LastName = "Manninen",
                PhoneNumber = "0405555555",
                Email = "aatu@gmail.com",
                Address = "Yrskis 35",
                Lendings = new List<Lending>() { new Lending {  BookId=1  } }




            };
            context.Customers.Add(customer);


            var customer1 = new Customer
            {
                FirstName = "Jenni",
                LastName = "Toivainne",
                PhoneNumber = "0406666666",
                Email = "jenni@gmail.com",
                Address = "Laajis 25",
                Lendings = new List<Lending>() { new Lending {  BookId = 2} }


            };
                context.Customers.Add(customer1);

            }





            public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
