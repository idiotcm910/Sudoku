using System;

namespace Game
{
   public enum Mode_Sudoku
        {
            easy = 25,
            medium = 15,
            hard = 5
        }
    public class Sudoku
    {
        private byte n;
        private byte[] row;
        private byte[] column;
        private byte[] matrix_Con;
        private byte[] result;
        public byte N{get{return n;}}
        private bool flag_Check = false;

        private Random r;

        private int start = 0;

        public Sudoku(byte number_Matrix)
        {
            n = number_Matrix;
            row = new byte[n*n];
            column = new byte[n*n];
            matrix_Con = new byte[n*n];
            result = new byte[n*n];
            r = new Random();
        }

        public void test(byte i, int stop)
        {
            if(start == stop){
                start = 0;
                return;
            }
            if(i == n*n){
                start += 1;
            }
            else{
                //output();
                if(result[i] != 0) test(Convert.ToByte(i + 1), stop);
                else{ 
                    //output();
                    byte i_R = Convert.ToByte(i / 9);
                    byte i_C = Convert.ToByte(i % 9);
                    byte vitri_Matrix = Convert.ToByte((((i_R / 3)*3) + (i_C / 3))*9);
                    byte[] ConditionStop = new byte[9];
                    byte numberCheck = 0;

                    while(numberCheck != 9)
                    {
                        byte index = Convert.ToByte(r.Next(1, 10));
    
                        //condition random
                        if(ConditionStop[index - 1] == 0){
                            byte value = Convert.ToByte(index - 1);
                            if(row[i_R*9 + value] == 0 && column[i_C*9 + value] == 0 && matrix_Con[vitri_Matrix + value] == 0){
                                result[i] = index;
                                row[i_R*9 + value] = Convert.ToByte(i + 1);
                                column[i_C*9 + value] = Convert.ToByte(i + 1);
                                matrix_Con[vitri_Matrix + value] = Convert.ToByte(i + 1);

                                test(Convert.ToByte(i + 1), stop);
                                if(start != stop){
                                    row[i_R*9 + value] = 0;
                                    column[i_C*9 + value] = 0;
                                    matrix_Con[vitri_Matrix + value] = 0;
                                }else{
                                    return;
                                }                 
                            }

                            ConditionStop[index - 1] = 1;
                            numberCheck += 1;
                        }
                    }
                    result[i] = 0;
                }
                
            }
        }

        public void output()
        {
            // test thuat toan
            /* for(byte i = 1; i <= 9; ++i)
            {
                for(byte j = 0; j < t*t; ++j)
                {
                    if(result[j] == i) cout << i << " ";
                    else cout << 0 << " ";
                    if((j + 1) % t == 0) cout << endl;
                }
                cout << "================================================" << endl;
            } */

            // in ra ket qua
            for(byte i = 0; i < n*n; ++i)
            {
                Console.Write("{0} ", result[i]);
                if((i + 1) % n == 0) Console.Write("\n");
            }
            Console.WriteLine("=====================================================");
        } 

        public void RandomStartGame(Mode_Sudoku mode)
        {   
            Random r = new Random();
            for(byte i = 1; i <= Convert.ToByte(mode); ++i)
            {
                byte index = Convert.ToByte(r.Next(0, n*n));
                result[index] += 10;
            }

            for(int i = 0; i < n*n; ++i)
            {
                if(result[i] < 10){
                    byte i_R = Convert.ToByte(i / 9);
                    byte i_C = Convert.ToByte(i % 9);
                    byte vitri_Matrix = Convert.ToByte((((i_R / 3)*3) + (i_C / 3))*9);

                    row[i_R*9 + result[i]] = 0;
                    column[i_C*9 + result[i]] = 0;
                    matrix_Con[vitri_Matrix + result[i]] = 0;
                    result[i] = 0;
                }
                else result[i] -= 10;
            }
        }

        public void AddValueToResult(byte i, byte index, out sbyte check)
        {
            if((index > 9 || index < 1) || (i < 0 || i > n*n)) check = -1;
            else{
                    byte i_R = Convert.ToByte(i / 9);
                    byte i_C = Convert.ToByte(i % 9);
                    byte value = Convert.ToByte(index - 1);
                    byte vitri_Matrix = Convert.ToByte((((i_R / 3)*3) + (i_C / 3))*9);
                    if(row[i_R*9 + value] == 0 && column[i_C*9 + value] == 0 && matrix_Con[vitri_Matrix + value] == 0){
                        result[i] = index;
                        row[i_R*9 + value] = Convert.ToByte(i + 1);
                        column[i_C*9 + value] = Convert.ToByte(i + 1);
                        matrix_Con[vitri_Matrix + value] = Convert.ToByte(i + 1);
                        check = 1;
                    }
                    else check = 0;
            }
        }
    }

    public class Display_Sodoku
    {
        private Sudoku GameOn;
        private Mode_Sudoku mode;
        public Display_Sodoku()
        {
            ScreenMode();
            ScreenPlay();
        }

            
        // man choi chon che do
        private void ScreenMode()
        {
            byte chosen;
            do{
                System.Console.WriteLine("Nhap lua chon: ");
                System.Console.WriteLine("1. Easy \n2. Medium \n3. Hard");
                Console.WriteLine("================================================");
                Console.Write("Nhap lua chon: "); chosen = Convert.ToByte(Console.ReadLine());
                if(chosen < 1 || chosen > 3){
                    Console.WriteLine("Ban nhap sai lua chon!");
                    Console.WriteLine("Nhan bat ky nut de tiep tuc!");
                    Console.ReadLine();
                    Console.Clear();
                }
            }while(chosen < 1 || chosen > 3);

            switch(chosen){
                case 1:
                    mode = Mode_Sudoku.easy;
                    break;
                case 2:
                    mode = Mode_Sudoku.medium;
                    break;
                case 3:
                    mode = Mode_Sudoku.hard;
                    break;
            }
            
            Console.Clear();
        }

        //bat dau choi
        private void ScreenPlay()
        {
            GameOn = new Sudoku(9);

            Console.WriteLine("1.Choi Game \n2.Giai");
            byte answer;
            do{
                Console.Write("Lua Chon: ");
                answer = Convert.ToByte(Console.ReadLine());

                if(answer > 2 || answer < 1)
                    Console.WriteLine("Ban lua chon sai, vui long chon lai!");
            }while(answer > 2 || answer < 1);

            Console.Clear();
            GameOn.RandomStartGame(mode);
            GameOn.output();

            switch(answer)
            {
                case 1:
                    ScreenPlay_ChosenOne();
                    break;
                case 2:
                    GameOn.test(0, 1);
                    GameOn.output();
                    break;
            }
        }



        private void ScreenPlay_ChosenOne()
        {
            int count = GameOn.N - Convert.ToInt32(mode);
            while(count != 0)
            {
                sbyte check;
                do{
                    byte vitri, value;
                    Console.Write("Nhap vi tri: "); vitri = Convert.ToByte(Console.ReadLine());
                    Console.Write("So tai vi tri do: "); value = Convert.ToByte(Console.ReadLine());
                    GameOn.AddValueToResult(vitri, value, out check);
                    if(check == 1) break;
                    else{
                        if(check == -1) Console.WriteLine("Ban nhap khong dung vi tri hoac so tai vi tri do!");
                        else Console.WriteLine("so tai vi tri ban nhap khong dung!");
                        Console.WriteLine("Nhan bat ky nut de tiep tuc!");
                        Console.ReadLine();
                        Console.Clear();
                    }

                }while(check != 1);

                count -= 1;
            }
        }
    }
}