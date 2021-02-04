using System;

namespace TaxRecursive
{
    class Program
    {   
        public static class TaxCalculator
        {
            public static int _Taxable, _SSO, _SumTax, _Net;

            public static int FindNet(int FullRevenue)
            {
                _Taxable = FindTaxable(FullRevenue);
                _SumTax = Sum_Tax();
                _Net = _Taxable - _SumTax;
                return _Net;
            }

            private static int FindTaxable(int FullRevenue)
            {
                int MaxSSORv = 4500000;
                _SSO = Convert.ToInt32(FullRevenue * 0.055f);
                if (FullRevenue < MaxSSORv)
                {
                    _SSO = Convert.ToInt32(FullRevenue * 0.055f);//Get SSO

                }
                else _SSO = Convert.ToInt32(MaxSSORv * 0.055f);//Get Max SSO

                return FullRevenue - _SSO;//Get TR
            }

            private static int Sum_Tax()
            {
                int Mx0 = 1300000;
                int Mx1 = 5000000;
                int Mx2 = 15000000;
                int Mx3 = 25000000;
                int Mx4 = 65000000;

                int T1 = 0, T2 = 0, T3 = 0, T4 = 0, T5 = 0;

                if (_Taxable > Mx4)
                {
                    T1 = Convert.ToInt32((Mx1 - Mx0) * 0.05f);
                    T2 = Convert.ToInt32((Mx2 - Mx1) * 0.10f);
                    T3 = Convert.ToInt32((Mx3 - Mx2) * 0.15f);
                    T4 = Convert.ToInt32((Mx4 - Mx3) * 0.20f);
                    T5 = Convert.ToInt32((_Taxable - Mx4) * 0.25f);
                }
                else if (_Taxable > Mx3)
                {
                    T1 = Convert.ToInt32((Mx1 - Mx0) * 0.05f);
                    T2 = Convert.ToInt32((Mx2 - Mx1) * 0.10f);
                    T3 = Convert.ToInt32((Mx3 - Mx2) * 0.15f);
                    T4 = Convert.ToInt32((_Taxable - Mx3) * 0.20f);
                    T5 = 0;
                }
                else if (_Taxable > Mx2)
                {
                    T1 = Convert.ToInt32((Mx1 - Mx0) * 0.05f);
                    T2 = Convert.ToInt32((Mx2 - Mx1) * 0.10f);
                    T3 = Convert.ToInt32((_Taxable - Mx2) * 0.15f);
                    T4 = 0;
                    T5 = 0;
                }
                else if (_Taxable > Mx1)
                {
                    T1 = Convert.ToInt32((Mx1 - Mx0) * 0.05f);
                    T2 = Convert.ToInt32((_Taxable - Mx1) * 0.10f);
                    T3 = 0;
                    T4 = 0;
                    T5 = 0;
                }
                else if (_Taxable > Mx0)
                {
                    T1 = Convert.ToInt32((_Taxable - Mx0) * 0.05f);
                    T2 = 0;
                    T3 = 0;
                    T4 = 0;
                    T5 = 0;
                }

                else { T1 = T2 = T3 = T4 = T5 = 0; }// FS is below Tax 0%

                return T1 + T2 + T3 + T4 + T5;
            }
        }

        static void Main(string[] args)
        {
            ConsoleKeyInfo CKI;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose Function: \n\nPress 1 to Find Net\nPress 2 to find Revenue...\nPress [Esc] to Exit.");
                CKI = Console.ReadKey();
                if (CKI.Key == ConsoleKey.NumPad1)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Finding Net, Please enter your Revenue:");
                        int RV = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\n");



                        Console.WriteLine("Your Revenue is: {0}", RV.ToString("N0"));
                        Console.WriteLine("Your Net is: {0}", TaxCalculator.FindNet(RV).ToString("N0"));
                        Console.WriteLine("    Taxable Revenue is: {0}", TaxCalculator._Taxable.ToString("N0"));
                        Console.WriteLine("    SSO 5.5% is: {0}", TaxCalculator._SSO.ToString("N0"));
                        Console.WriteLine("    Sum of Tax is: {0}", TaxCalculator._SumTax.ToString("N0"));

                        Console.WriteLine("\n\n\nPress [Esc] to back. Press any key to Find Net again...");
                        CKI = Console.ReadKey();
                    }
                    while (CKI.Key != ConsoleKey.Escape);
                }
                else if (CKI.Key == ConsoleKey.NumPad2)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Finding the True Revenue, Please enter your Current Net:");
                        int Fix_TargetNet = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\n");

                        int Starting_TX, Loop_Guess_TX, Genenated_Net;
                        float ErrorRate;

                        Starting_TX = Convert.ToInt32(Fix_TargetNet * 1.25f);//+25% Max Percentage
                        Loop_Guess_TX = Starting_TX;
                        Genenated_Net = TaxCalculator.FindNet(Starting_TX);
                        ErrorRate = Convert.ToSingle(Genenated_Net - Fix_TargetNet) / Convert.ToSingle(Fix_TargetNet);//Starting Net

                        //Speed Run * 0.99
                        do//>0.5%
                        {
                            ErrorRate = Convert.ToSingle(Genenated_Net - Fix_TargetNet) / Convert.ToSingle(Fix_TargetNet); //Find the Error Rate
                            if (ErrorRate > 0)
                            {
                                Loop_Guess_TX = Convert.ToInt32(Loop_Guess_TX * 0.99f);//Reducing
                                Genenated_Net = TaxCalculator.FindNet(Loop_Guess_TX); //Find Possibility                        
                            }
                            ErrorRate = Convert.ToSingle(Genenated_Net - Fix_TargetNet) / Convert.ToSingle(Fix_TargetNet);
                            Console.WriteLine("Phase 1 - Error Rate: {0}%", (ErrorRate * 100).ToString());
                        }
                        while (ErrorRate > 0.01f);

                        //Reducing Minus Left
                        while (ErrorRate > 0)
                        {
                            int X = Genenated_Net - Fix_TargetNet;
                            Loop_Guess_TX -= X;
                            Genenated_Net = TaxCalculator.FindNet(Loop_Guess_TX); //Find Possibility
                            ErrorRate = Convert.ToSingle(Genenated_Net - Fix_TargetNet) / Convert.ToSingle(Fix_TargetNet);
                            Console.WriteLine("Phase 2 - Error Rate: {0}%", (ErrorRate * 100).ToString());
                        }

                        Console.WriteLine("RV: {0}", Loop_Guess_TX.ToString("N0"));
                        Console.WriteLine("GN: {0}", Genenated_Net.ToString("N0"));



                        Console.WriteLine("\n\n\nPress [Esc] to back. Press any key to Find Revenue again...");
                        CKI = Console.ReadKey();
                    }
                    while (CKI.Key != ConsoleKey.Escape);

                }
                CKI = new ConsoleKeyInfo();//clear key
            }
            while (CKI.Key != ConsoleKey.Escape);




            //Console.WriteLine("Enter your Net Number:");            
            //float Net = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("\n");        

            //Console.WriteLine("Net Number is: {0}", Net.ToString("N0"));

            //Console.ReadKey();
        }

        
    }
}
