using System.Collections;
using System.Data;
using NQuery.Iterators;
using NQuery.Symbols;

namespace NQuery.Tests.Evaluation
{
    public class WindowFunctionTests
    {
        [Fact]
        public void WindowFunction_ParseOverClause()
        {
            var dataContext = NorthwindDataContext.Instance;
            var query = Query.Create(dataContext,
                """
                SELECT 
                    t.OrderId, 
                    t.CustomerId, 
                    ROW_NUMBER() OVER (PARTITION BY t.EmployeeID ORDER BY t.OrderId), 
                    ROW_NUMBER() OVER (PARTITION BY t.ShipVia, t.EmployeeId ORDER BY t.OrderId DESC) 
                FROM Orders t
                """);

            var orders = (SchemaTableSymbol)dataContext.Tables.Single(s => s.Name == "Orders");
            var rows = orders.Definition.GetRows().Cast<DataRow>().Select(s => new[] { s["OrderId"], s["CustomerId"], s["EmployeeID"], s["ShipVia"] }).ToList();
            // Order rows by EmployeeID, OrderId
            var orderedRows1 = rows.ToList();
            orderedRows1.Sort((x, y) =>
            {
                var c = Comparer.Default.Compare(x[2], y[2]);
                if (c != 0)
                    return c;

                c = Comparer.Default.Compare(x[0], y[0]);
                return c;
            });
            var expectedRowNumbers1 = new int[orderedRows1.Count];
            expectedRowNumbers1[0] = 1;
            for (var i = 1; i < orderedRows1.Count; i++)
            {
                expectedRowNumbers1[i] = Comparer.Default.Compare(orderedRows1[i - 1][2], orderedRows1[i][2]) == 0
                    ? expectedRowNumbers1[i - 1] + 1
                    : 1;
            }

            // Order rows by ShipVia, EmployeeID, OrderId
            var neg = new NegatedComparer(Comparer.Default);
            var orderedRows2 = rows.ToList();
            orderedRows2.Sort((x, y) =>
            {
                var c = Comparer.Default.Compare(x[3], y[3]);
                if (c != 0)
                    return c;

                c = Comparer.Default.Compare(x[2], y[2]);
                if (c != 0)
                    return c;

                c = neg.Compare(x[0], y[0]);
                return c;
            });
            var expectedRowNumbers2 = new int[orderedRows2.Count];
            expectedRowNumbers2[0] = 1;
            for (var i = 1; i < orderedRows2.Count; i++)
            {
                expectedRowNumbers2[i] = Comparer.Default.Compare(orderedRows2[i - 1][3], orderedRows2[i][3]) == 0
                    && Comparer.Default.Compare(orderedRows2[i - 1][2], orderedRows2[i][2]) == 0
                        ? expectedRowNumbers2[i - 1] + 1
                        : 1;
            }

            using var data = query.ExecuteReader();

            while (data.Read())
            {
                for (var i = 0; i < data.ColumnCount; i++)
                {
                    var orderId = (int)data[0]!;

                    var r1 = (int)data[2]!;
                    var expected1 = expectedRowNumbers1[orderedRows1.FindIndex(s => (int)s[0] == orderId)];
                    Assert.Equal(expected1, r1);

                    var r2 = (int)data[3]!;
                    var expected2 = expectedRowNumbers2[orderedRows2.FindIndex(s => (int)s[0] == orderId)];
                    Assert.Equal(expected2, r2);
                }
            }
        }
    }
}