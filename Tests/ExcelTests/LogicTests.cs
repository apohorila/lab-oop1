using Xunit;
using System;
using Grammars;
using System.Collections.Generic;
using System.Linq;


namespace ExcelTests;

public class LogicTests
{
    [Fact]
    public void Test1_Arithmetic_Precedence()
    {
        double result = Calculator.Evaluate("15+7*6");
        Assert.Equal(57.0, result);
    }

    [Fact]
    public void Test2_CircularDependency_ThrowsException()
    {
        Calculator.sheet = new Sheet();
        string cellA1 = "A1";
        string cellB1 = "B1";

        Calculator.sheet.Cells[cellA1] = new Grammars.Cell("10");
        Calculator.sheet.Cells[cellB1] = new Grammars.Cell("5");

        Calculator.sheet.EditCell(cellB1, "A1+1");
        Assert.Throws<System.Exception>(() =>
        {
            Calculator.sheet.EditCell(cellA1, "B1+1");
        });
    }
    [Fact]
    public void Test3_CascadingDependencyUpdate()
    {
        Calculator.sheet = new Sheet();
        string cellA1 = "A1";
        string cellB1 = "B1";
        string cellC1 = "C1";

        Calculator.sheet.Cells[cellA1] = new Grammars.Cell("10");
        Calculator.sheet.EditCell(cellB1, "A1+5");
        Calculator.sheet.EditCell(cellC1, "B1*2");

        Calculator.sheet.EditCell(cellA1, "5");  
        Assert.Equal("20", Calculator.sheet.Cells[cellC1].Value);
        Assert.Equal("10", Calculator.sheet.Cells[cellB1].Value);
    }
}
