using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSHARP_Exam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHARP_Exam.Models;

namespace CSHARP_Exam.Utilities.Tests
{
    [TestClass()]
    public class AddClientsTests
    {
        [TestMethod()]
        public void Generate_client_list_Test()
        {
            // Arrange
            AddClients addClientsGenerator = new AddClients(true);
            List<int> clients = new List<int>();
            addClientsGenerator.Generate(clients);
            int sizeBefore = 0;
            int sizeAfter = 0;
            
            // Act
            while (clients.Count()==0)
            {
                Console.WriteLine(clients.Count());
            };
            sizeAfter= clients.Count();

            // Assert
            Assert.AreNotEqual(sizeBefore, sizeAfter, "Function did not generate any new members.");
        }
    }
}