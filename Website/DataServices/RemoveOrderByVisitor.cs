using System;
using System.Linq;
using System.Linq.Expressions;

namespace NuGetGallery
{
    public class RemoveOrderByVisitor : ExpressionVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name.Equals("OrderBy", StringComparison.Ordinal) || node.Method.Name.Equals("ThenBy", StringComparison.Ordinal))
            {
                return Visit(node.Arguments.First());
            }
            return base.VisitMethodCall(node);
        }
    }
}
