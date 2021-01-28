using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxRecursive
{
    class Program
    {
        static void Main(string[] args)
        {
            string NetString = Console.ReadLine();
            float Net = Convert.ToInt32(NetString);
            float FullS = 0f;

            float Lv0 = 1300000;
            float Lv1 = 5000000;
            float Lv2 = 15000000;
            float Lv3 = 25000000;
            float Lv4 = 65000000;

            Console.WriteLine("Net Number is: {0}",Net.ToString("N0"));


            if (Net > 0 && Net <= 1300000)
                FullS = Net;
            else if (Net > Lv0 && Net < Lv1)
            {
                int Hypo = (int)(Net * 1.04f);// let it be BIG
                Console.WriteLine("Hypothese Now: {0}", Hypo.ToString("N0"));
                int Expecting = (int)(Hypo - (Hypo - Lv0) * 0.05);//Expecting must be close to Net. Expecting VS Net.
                Console.WriteLine("Expecting Now: {0}", Expecting.ToString("N0"));
                float Rate = (Expecting - Net) /Net;
                Console.WriteLine("Rate: {0}%", Rate);

                int loopCount = 0;

                while (Rate > 0.0f)//decrease to the the expecting number
                {
                    if (Rate > 0.001)
                        Hypo -= 2000;
                    else if (Rate > 0.0005)
                        Hypo -= 1000;
                    else if (Rate > 0.0002)
                        Hypo -= 100;
                    else if (Rate > 0.0001)
                        Hypo -= 10;
                    else if (Rate > 0.00005)
                        Hypo -= 2;
                    else Hypo--;
                    Console.WriteLine("New Hypo: {0}", Hypo.ToString("N0"));
                    Expecting = (int)(Hypo - ((Hypo - Lv0) * 0.05));
                    Console.WriteLine("Expecting: {0}", Expecting.ToString("N0"));
                    Rate = (Expecting - Net) / Net;
                    Console.WriteLine("Rate: {0}%\n\n", Rate);

                    loopCount++;
                }
                Console.WriteLine("{0} Loop(s), Rate: {1}%", loopCount, (Rate * 100f).ToString());
                Hypo--;
                Console.WriteLine("Salary After SSO is: {0}", Hypo.ToString("N0"));
                if (Hypo > 4252500)
                    Console.WriteLine("Full Salary is: {0}", (Hypo+247500).ToString("N0"));
                else
                    Console.WriteLine("Full Salary is: {0}", (Hypo / 0.945).ToString("N0"));


            }
            else if (Net > Lv1 & Net < Lv2)
            {
                int Hypo = (int)(Net * 1.08f);// let it be BIG
                Console.WriteLine("Hypothese Now: {0}", Hypo.ToString("N0"));
                int Expecting = (int)(Hypo - (Hypo - (Lv0 + Lv1)) * 0.1f - ((Lv1-Lv0) * 0.05f));//Expecting must be close to Net. Expecting VS Net.
                Console.WriteLine("Expecting Now: {0}", Expecting.ToString("N0"));
                float Rate = (Expecting - Net) / Net;
                Console.WriteLine("Rate: {0}%", Rate);

                int loopCount = 0;

                while (Rate > 0.0f)//decrease to the the expecting number
                {
                    if (Rate > 0.001)
                        Hypo -= 2000;
                    else if (Rate > 0.0005)
                        Hypo -= 1000;
                    else if (Rate > 0.0002)
                        Hypo -= 100;
                    else if (Rate > 0.0001)
                        Hypo -= 10;
                    else if (Rate > 0.00005)
                        Hypo -= 2;
                    else Hypo--;
                    Console.WriteLine("New Hypo: {0}", Hypo.ToString("N0"));
                    Expecting = (int)(Hypo - ((Hypo - Lv0) * 0.05));
                    Console.WriteLine("Expecting: {0}", Expecting.ToString("N0"));
                    Rate = (Expecting - Net) / Net;
                    Console.WriteLine("Rate: {0}%\n\n", Rate);

                    loopCount++;
                }
                Console.WriteLine("{0} Loop(s), Rate: {1}%", loopCount, (Rate * 100f).ToString());
                Hypo--;
                Console.WriteLine("Your Net: {0}", Net.ToString("N0"));
                Console.WriteLine("Salary After SSO is: {0}", Hypo.ToString("N0"));
                if (Hypo > 4252500)
                    Console.WriteLine("Full Salary is: {0}", (Hypo + 247500).ToString("N0"));
                else
                    Console.WriteLine("Full Salary is: {0}", (Hypo / 0.945).ToString("N0"));


            }

            Console.ReadKey();
        }
    }
}
