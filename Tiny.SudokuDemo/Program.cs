/*
 * 数独（すうどく，Sudoku）是一种运用纸、笔进行演算的逻辑游戏。
 * 玩家需要根据9×9盘面上的已知数字，推理出所有剩余空格的数字，
 * 并满足每一行、每 一列、每一宫内的数字均含1-9，不重复。 每一
 * 道合格的数独谜题都有且仅有唯一答案，推理方法也以此为基础，任
 * 何无解或多解的题目都是不合格的。
 * --------------------------------
 * LooneyLoong
 * 2013
 * http://www.netstructs.com
 * --------------------------------
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiny.SudokuDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Tiny.Sudoku.Core.SudokuSeed sdk = new Sudoku.Core.SudokuSeed();
            int[,] sudoku = sdk.Germination(Sudoku.Core.SudokuSeed.XxYy.X);

            for (int X = 0; X < 9; X++)
            {
                for (int Y = 0; Y < 9; Y++)
                {
                    Console.Write("{0}, ", sudoku[X, Y]);
                }
                Console.Write("\r\n");
            }
            Console.WriteLine("\r\nPress Any Key To Exit...");
            Console.ReadKey(true);
        }
    }
}
