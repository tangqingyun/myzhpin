using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace OrmExpressionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Names = { "Andy", "Amy", "Mike" };

            Expression<Func<Staff, bool>> func = x => (!Names.Contains(x.Name) && (x.Name == "A" || x.Name.Count() > 5));
            ResolveExpress resolve = new ResolveExpress();
            resolve.ResolveExpression(func);
            Console.WriteLine(resolve.SqlWhere);
            foreach (var item in resolve.Paras)
            {
                Console.WriteLine(item.ParameterName + ":" + item.Value);
            }
            Console.ReadKey();

        }


        public static List<T> FindAs<T>(Expression<Func<T, bool>> func)
        {
            BinaryExpression Binary = func.Body as BinaryExpression;
            string left = ResovleFunc(Binary.Left);
            string right = ResovleLinqToObject(Binary.Right);
            string oper = GetOperator(Binary.NodeType);
            string sql = string.Format("select * from {0} where {1}", typeof(T).Name, left + oper + right);
            Console.WriteLine(sql);
            return null;//将结果返回
        }

        //解析一般的条件，例如x=>x.name==xxxx   x.age==xxx
        public static string ResovleFunc(Expression express)
        {
            var inner = express as BinaryExpression;
            string Name = (inner.Left as MemberExpression).Member.Name;
            object Value = (inner.Right as ConstantExpression).Value;
            var Operator = GetOperator(inner.NodeType);
            string Result = string.Format("({0} {1} '{2}')", Name, Operator, Value);
            return Result;
        }

        //解析linq to object这类扩展方法
        public static string ResovleLinqToObject(Expression expression)
        {
            var MethodCall = expression as MethodCallExpression;
            var MethodName = MethodCall.Method.Name;
            if (MethodName == "Contains")
            {
                object Temp_Vale = (MethodCall.Arguments[0] as ConstantExpression).Value;
                string Value = string.Format("%{0}%", Temp_Vale);
                string Name = (MethodCall.Object as MemberExpression).Member.Name;
                string Result = string.Format("{0} like '{1}'", Name, Value);
                return Result;
            }
            return null;
        }

        public static string GetOperator(ExpressionType expressiontype)
        {
            switch (expressiontype)
            {
                case ExpressionType.And:
                    return "and";
                case ExpressionType.AndAlso:
                    return "and";
                case ExpressionType.Or:
                    return "or";
                case ExpressionType.OrElse:
                    return "or";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                default:
                    throw new Exception(string.Format("不支持{0}此种运算符查找！" + expressiontype));
            }
        }

    }
}
