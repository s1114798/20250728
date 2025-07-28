using System;
using System.Collections.Generic;

namespace Practise7
{

    class Program
    {
        // 產生一組不重複的 4 位數字（答案）
        static string GenerateAnswer()
        {
            Random rand = new Random(); // 建立隨機物件
            HashSet<int> digits = new HashSet<int>(); // 儲存不重複的數字

            // 持續產生直到有 4 個不重複的數字為止
            while (digits.Count < 4)
            {
                digits.Add(rand.Next(0, 10)); // 隨機取 0~9 加入集合（不重複）
            }

            // 將數字集合轉為字串
            string answer = "";
            foreach (int digit in digits)
            {
                answer += digit.ToString();
            }

            return answer; // 回傳四位數字的答案字串
        }

        // 方法：比對玩家輸入與答案，計算幾A幾B
        static void GetAB(string guess, string answer, out int A, out int B)
        {
            A = 0; // 數字與位置正確的個數
            B = 0; // 數字正確但位置錯誤的個數

            for (int i = 0; i < 4; i++)
            {
                if (guess[i] == answer[i])
                {
                    A++; // 數字與位置都正確，加一個 A
                }
                else if (answer.Contains(guess[i]))
                {
                    B++; // 數字正確但位置錯誤，加一個 B
                }
            }
        }

        // 主程式進入點
        static void Main()
        {
            string answer = GenerateAnswer(); // 產生答案（四位不重複數字）
            int count = 0; // 紀錄玩家猜了幾次

            Console.WriteLine("==== 猜數字遊戲 ====");
            Console.WriteLine("請輸入四位不重複的數字");
            Console.WriteLine("A 表示數字與位置正確的個數");
            Console.WriteLine("B 表示數字正確但位置錯誤的個數");

            // 玩家最多可以猜 10 次
            while (count < 10)
            {
                Console.Write($"\n第 {count + 1} 次猜：");
                string guess = Console.ReadLine(); // 讀取玩家輸入

                // 驗證輸入格式：
                // 必須是長度為 4 的字串
                // 必須是整數
                // 不可有重複的數字
                if (guess.Length != 4 || !int.TryParse(guess, out _) || new HashSet<char>(guess).Count != 4)
                {
                    Console.WriteLine("格式錯誤，請輸入不重複的數字！");
                    continue; // 結束這回合，繼續下一輪
                }

                count++; // 增加猜測次數

                // 計算玩家這次輸入與答案的比對結果（幾 A 幾 B）
                GetAB(guess, answer, out int A, out int B);

                // 顯示結果，例如 2A1B
                Console.WriteLine($"{A}A{B}B");

                // 若猜中 4A，表示全部正確，遊戲成功
                if (A == 4)
                {
                    Console.WriteLine($"\n 答對了！你在第 {count} 次猜中答案！");
                    break; // 結束遊戲
                }
            }

            // 若 10 次內未猜中，顯示遊戲結束與正確答案
            if (count >= 10)
            {
                Console.WriteLine($"\n 沒答對，遊戲結束！");
                Console.WriteLine($"正確答案是：{answer}");
            }
        }
    }

}
