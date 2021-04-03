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
        private byte checkResult;
        public byte N{get{return n;}}
        private bool flag_Check = false;

        public Sudoku(byte number_Matrix)
        {
            n = number_Matrix;
            row = new byte[n*n];
            column = new byte[n*n];
            matrix_Con = new byte[n*n];
            result = new byte[n*n];
        }

        public void test(byte i)
        {
            if(flag_Check == true) return;
            if(i == n*n){
                output();
                flag_Check = true;
            }
            else{
                //output();
                for(byte index = 1; index <= 9; ++index)
                {
                    byte i_R = Convert.ToByte(i / 9);
                    byte i_C = Convert.ToByte(i % 9);
                    byte value = Convert.ToByte(index - 1);
                    byte vitri_Matrix = Convert.ToByte((((i_R / 3)*3) + (i_C / 3))*9);
                    if(row[i_R*9 + value] == 0 && column[i_C*9 + value] == 0 && matrix_Con[vitri_Matrix + value] == 0){
                        result[i] = index;
                        row[i_R*9 + value] = Convert.ToByte(i + 1);
                        column[i_C*9 + value] = Convert.ToByte(i + 1);
                        matrix_Con[vitri_Matrix + value] = Convert.ToByte(i + 1);

                        test(Convert.ToByte(i + 1));
                        if(flag_Check != true){
                            row[i_R*9 + value] = 0;
                            column[i_C*9 + value] = 0;
                            matrix_Con[vitri_Matrix + value] = 0;
                        }else{
                            return;
                        }                 
                    }
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
            checkResult = Convert.ToByte(n - mode);
            Random r = new Random();
            for(byte i = 1; i <=Convert.ToByte(mode); ++i)
            {
                sbyte check;
                do{
                    AddValueToResult(Convert.ToByte(r.Next(0, 64)),Convert.ToByte(r.Next(1, 9)), out check);
                    if(check == 1) break;
                }while(check != 1);
            }
        }

        public void AddValueToResult(byte index_Result, byte Value, out sbyte check)
        {
            if((Value > 9 || Value < 1) || (index_Result < 0 || index_Result > n*n)) check = -1;
            else{
                byte i_R = Convert.ToByte(index_Result / 9);
                byte i_C = Convert.ToByte(index_Result % 9);
                byte value_i = Convert.ToByte(Value - 1);
                byte vitri_Matrix = Convert.ToByte((((i_R / 3)*3) + (i_C / 3))*9);
                if(row[i_R*9 + value_i] == 0 && column[i_C*9 + value_i] == 0 && matrix_Con[vitri_Matrix + value_i] == 0){
                    result[index_Result] = Value;
                    row[i_R*9 + value_i] = Convert.ToByte(index_Result + 1);
                    column[i_C*9 + value_i] = Convert.ToByte(index_Result + 1);
                    matrix_Con[vitri_Matrix + value_i] = Convert.ToByte(index_Result + 1);
                    check = 1;
                }
                else check = 0;
            }
        }
    }

    public class Display_Sodoku
    {
        private Sudoku GameOn;
        public Display_Sodoku()
        {
            GameOn = new Sudoku(9);

            GameOn.output();

            Console.WriteLine("1.Choi Game \n2.Giai");
            byte answer;
            do{
                Console.Write("Lua Chon: ");
                answer = Convert.ToByte(Console.ReadLine());

                if(answer > 2 || answer < 1)
                    Console.WriteLine("Ban lua chon sai, vui long chon lai!");
            }while(answer > 2 || answer < 1);

            switch(answer)
            {
                case 1:
                    GameOn.RandomStartGame(Mode_Sudoku.easy);
                    GameOn.output();
                    break;
                case 2:
                    GameOn.test(0);
                    GameOn.output();
                    break;
            }
        }

        private void Chosen_One()
        {
            Console.Clear();
            GameOn.RandomStartGame(Mode_Sudoku.easy);
            GameOn.output();

            for(byte index = 0; index < GameOn.N*GameOn.N; ++index)
            {
                byte c1, c2;
                Console.Write("Nhap vi tri: "); c1 = Convert.ToByte(Console.ReadLine());
                Console.Write("Gia tri: "); c2 = Convert.ToByte(Console.ReadLine());
            }
        }
    }
}