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

namespace Tiny.Sudoku.Core
{
    /// <summary>
    /// 生成一个数独基础数据
    /// </summary>
    public class SudokuSeed
    {
        public SudokuSeed() { }

        /// <summary>
        /// 元素移动方式
        /// 0-------------------------->X
        /// |
        /// |
        /// |
        /// |
        /// |
        /// |
        /// |
        /// v Y
        /// </summary>
        public enum XxYy : int
        {
            /// <summary>
            /// 沿X方向移动，并保持Y方向的元素顺序不变
            /// </summary>
            X = 0,
            /// <summary>
            /// 沿Y方向移动，并保持X方向的元素顺序不变
            /// </summary>
            Y = 1
        }

        /// <summary>
        /// 123456789
        /// 234567891
        /// 345678912
        /// 456789123
        /// 567891234
        /// 678912345
        /// 789123456
        /// 891234567
        /// 912345678
        /// <para>播种</para>
        /// </summary>
        /// <returns>返回一个有序数字序列</returns>
        private int[,] Seeding()
        {
            int[,] XY = new int[9, 9];
            int[] element = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int offset = 0;
            for (int y = 0; y < 9; y++)
            {
                int index = offset;
                for (int x = 0; x < 9; x++)
                {
                    if (index > 8)
                    {
                        index = 0;
                    }
                    XY[x, y] = element[index];
                    index++;
                }
                offset++;
            }
            return XY;
        }
        
        /// <summary>
        /// --------------------
        /// | 123 | 456 | 789 | 
        /// | 456 | 789 | 123 |　 一
        /// | 789 | 123 | 456 | 
        /// --------------------
        /// | 234 | 567 | 891 | 
        /// | 567 | 891 | 234 | 　二
        /// | 891 | 234 | 567 | 
        /// --------------------
        /// | 345 | 678 | 912 | 
        /// | 678 | 912 | 345 | 　三
        /// | 912 | 345 | 678 | 
        /// --------------------
        /// Y3 -> Y1; Y1 -> Y3　一 <--> 二
        /// Y6 -> Y2; Y2 -> Y6　一 <--> 三
        /// Y5 -> Y7; Y7 -> Y5　二 <--> 三
        /// </summary>
        public int[,] Germination(XxYy xxyy)
        {
            bool isX = (xxyy == XxYy.X) ? true : false;
            int[,] newborn = this.Seeding();
            List<int>[] LIX = new List<int>[9];//用于暂存数据
            List<int>[] LIY = new List<int>[9];//用于暂存数据
            int index = 0;

            if (isX)
            {
                LIX = new List<int>[9] { new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>() };
                for (int x = 0; x < 9; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        LIX[index].Add(newborn[x, y]);
                    }
                    index++;
                }
            }
            else
            {
                LIY = new List<int>[9] { new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>() };
                for (int y = 0; y < 9; y++)
                {
                    for (int x = 0; x < 9; x++)
                    {
                        LIY[index].Add(newborn[x, y]);
                    }
                    index++;
                }
            }

            #region XxYy.Y
            /*
        1, 2, 3, 4, 5, 6, 7, 8, 9
        4, 5, 6, 7, 8, 9, 1, 2, 3
        7, 8, 9, 1, 2, 3, 4, 5, 6
        2, 3, 4, 5, 6, 7, 8, 9, 1
        5, 6, 7, 8, 9, 1, 2, 3, 4
        8, 9, 1, 2, 3, 4, 5, 6, 7
        3, 4, 5, 6, 7, 8, 9, 1, 2
        6, 7, 8, 9, 1, 2, 3, 4, 5
        9, 1, 2, 3, 4, 5, 6, 7, 8
         */
            if (!isX)
            {
                int y1 = 0;
                while (true)
                {
                    int x = 0;
                    int y2 = 0;
                    switch (y1)
                    {
                        case 1:
                        case 5:
                            for (x = 0; x < 9; x++)
                            {
                                y2 = y1 + 2;
                                newborn[x, y1] = LIY[y2][x];
                                newborn[x, y2] = LIY[y1][x];
                            }
                            break;
                        case 2:
                            for (x = 0; x < 9; x++)
                            {
                                y2 = y1 + 4;
                                newborn[x, y1] = LIY[y2][x];
                                newborn[x, y2] = LIY[y1][x];
                            }
                            break;
                        case 0:
                        case 3:
                        case 4:
                        case 6:
                        case 7:
                        case 8:
                            break;
                        default: break;
                    }
                    y1++;
                    if (y1 > 8)
                    {
                        break;
                    }
                }
            }
            else
            {
                /* XxYy.Y
                1, 2, 3, 4, 5, 6, 7, 8, 9
                4, 5, 6, 7, 8, 9, 1, 2, 3
                7, 8, 9, 1, 2, 3, 4, 5, 6
                2, 3, 4, 5, 6, 7, 8, 9, 1
                5, 6, 7, 8, 9, 1, 2, 3, 4
                8, 9, 1, 2, 3, 4, 5, 6, 7
                3, 4, 5, 6, 7, 8, 9, 1, 2
                6, 7, 8, 9, 1, 2, 3, 4, 5
                9, 1, 2, 3, 4, 5, 6, 7, 8
                 */
                int x1 = 0;
                while (true)
                {
                    int y = 0;
                    int x2 = 0;
                    switch (x1)
                    {
                        case 1:
                        case 5:
                            x2 = x1 + 2;
                            for (y = 0; y < 9; y++)
                            {
                                newborn[x1, y] = LIX[x2][y];
                                newborn[x2, y] = LIX[x1][y];
                            }
                            break;
                        case 2:
                            x2 = x1 + 4;
                            for (y = 0; y < 9; y++)
                            {
                                newborn[x1, y] = LIX[x2][y];
                                newborn[x2, y] = LIX[x1][y];
                            }
                            break;
                        case 0:
                        case 3:
                        case 4:
                        case 6:
                        case 7:
                        case 8:
                            break;
                        default: break;
                    }
                    x1++;
                    if (x1 > 8)
                    {
                        break;
                    }
                }
            }
            #endregion
            return newborn;
        }
    }
}
