using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeligatesInDottNet
{
    class Program
    {
        static void Main()
        {
            //int res = Square(2, 3, "exp");
            Myclass myclass = new Myclass();
            //int res = SquareUsingDelegateParameter(2, 3, specialMeth);

            //int res = SquareUsingDelegateParameter(2, 3, 
            // delegate (int a, int b)
            // {
            //    return 10 * (a * b);
            // }
            //    );

            int res = SquareUsingDelegateParameter(2, 3, (a, b) => 10 * (a * b));

            //res = specialMeth(6, 9);

            Console.WriteLine(res);
        }

        static int specialMeth(int a, int b)
        {
            return 10 * (a * b);
        }

        static void Main2(string[] args)
        {
            Myclass obj = new Myclass();

            funcBinary x = null;

            Random r = new Random();
            int num = r.Next(1, 6);

            switch (num)
            {
                case 1:
                    //we are saving the function context inside the delegate variable
                    x = new funcBinary(obj.add);//upto .net 1.1
                    break;
                case 2:
                    x = obj.mul;
                    break;
                case 3:
                    x = obj.sub;
                    break;
                case 4:
                    x = obj.div;
                    break;
                case 5:
                    x = obj.powr;
                    break;

            }

            //int res = x.Invoke(3, 4);
            //Console.WriteLine("result :" + res);
            x.BeginInvoke(3, 4, ProcessResult, x);

            Console.WriteLine("Continuing with the usual task....");

            Console.ReadKey();

        }

        public static void ProcessResult(IAsyncResult token)
        {
            funcBinary func = (funcBinary)token.AsyncState;

            int res = func.EndInvoke(token);
            Console.WriteLine(res);
        }

        static int Square(int x, int y, string operationType)
        {
            int res = 0;
            Myclass myclass = new Myclass();
            if (operationType == "add")
            {
                res = x * x + y * y;  //myclass.add(x * x, y * y);
            }
            else if (operationType == "sub")
            {
                res = myclass.sub(x * x, y * y);
            }
            else if (operationType == "mul")
            {
                res = myclass.mul(x * x, y * y);
            }
            else if (operationType == "div")
            {
                res = myclass.div(x * x, y * y);
            }
            return res;
        }

        static int SquareUsingDelegateParameter(int x, int y, funcBinary binary)
        {
            int a = x * x;
            int b = y * y;
           
            int res = binary.Invoke(a, b);
            

            return res;
        }


    }

    public delegate int funcBinary(int num1, int num2);

    class Myclass
    {

        public int add(int a, int b)
        {
            Console.WriteLine("Invoking Add...");

            System.Threading.Thread.Sleep(4000);

            return a + b;
        }
        public int sub(int num1, int num2)
        {
            Console.WriteLine("Invoking Subtraction");
            return num1 - num2;
        }
        public int mul(int a, int b)
        {
            Console.WriteLine("Invoking Multiplication");
            return a * b * 4;
        }
        public int div(int a, int b)
        {
            Console.WriteLine("Invoking Division");
            return a / b;
        }

        public int powr(int baseNum, int exponent)
        {
            int mul = 1;
            for (int i = 1; i <= exponent; i++)
            {
                mul = mul * baseNum;
            }
            Console.WriteLine("Invoking Exponent");
            return mul;
        }

        public int Square(int num)
        {
            Console.WriteLine("Invoking Square");
            return num * num;
        }

    }
}
