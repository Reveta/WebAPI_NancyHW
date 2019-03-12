using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Hosting.Self;
using Nancy.ModelBinding;

namespace WebAPI_HW {
    internal class Program {
        public static void Main(string[] args) {
            using (var host = new NancyHost(new Uri("http://127.0.0.1:1234"))) {
                host.Start();
                Console.ReadKey();
            }
        }
    }


    class Dinosaur {
        public string Name { get; set; }
        public int HeightInFeet { get; set; }
        public string Status { get; set; }
    }

    public class DinosaurModule : NancyModule {
        private static List<Dinosaur> dinosaurs = new List<Dinosaur>() {
            new Dinosaur() {
                Name = "Kierkegaard",
                HeightInFeet = 6,
                Status = "Inflated"
            }
        };

        public DinosaurModule() {
            int next = new Random().Next(0, 999999);
            Get["/H"] = parameters => string.Format("<html><header><title>This is title</title></header><body>Hello world {0}</body></html>", next).ToString();
            Get["/dinosaurs/{id}"] = parameters => dinosaurs[parameters.id - 1];
            Post["/dinosaurs"] = parameters => {
                var model = this.Bind<Dinosaur>();
                dinosaurs.Add(model);
                return dinosaurs.Count.ToString();
            };
        }
    }
}