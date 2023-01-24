using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSHARP_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARP_Exam.Models.Tests
{
    [TestClass()]
    public class CheckTests
    {
        [TestMethod()]
        public void add_amount_to_check_Test()
        {
            // Arrange
            Table table = new Table(1,1,1,(bool)true);
            Guid sample = Guid.NewGuid();
            Check check = new Check(table,sample);
            double addition = 9.99;
            double expected = 9.99;

            // Act
            check.add_amount(addition);

            // Assert
            double actual = check.Amount;
            Assert.AreEqual(expected, actual, 0.001, "Amount did not transfer correctly");
        }

        [TestMethod()]
        public void get_time_from_check_Test()
        {
            // Arrange
            Table table = new Table(1, 1, 1, (bool)true);
            Guid sample = Guid.NewGuid();
            Check check = new Check(table, sample);

            // Act
            var currentdate = DateTime.Now;
            check.Timestamp = currentdate;

            // Assert
            Assert.AreEqual(currentdate, check.Timestamp, "Date did not transfer correctly");
        }
    }
}