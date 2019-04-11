using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace pazel_Game
{
    public enum Difficulty
    {
        Easy, Moderate, Difficult
    }
    enum Moves
    {
        Left, Up, Right, Down
    }
    class Pazel
    {
        private readonly int SIZE;
        static public Random random = new Random();
        public int[,] Matrix;

        public Pazel(int size, Difficulty difficulty)
        {
            SIZE = size;
            Matrix = new int[SIZE, SIZE];
            for (int i = 0; i < SIZE * SIZE - 1; i++)
                this[i] = i + 1;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    for (int i = 0; i < SIZE * SIZE; i++)
                        RandomMove();
                    break;
                case Difficulty.Moderate:
                    for (int i = 0; i < SIZE * SIZE * 3; i++)
                        RandomMove();
                    break;
                case Difficulty.Difficult:
                    for (int i = 0; i < SIZE * SIZE - 1; i++)
                    {
                        int rand = random.Next(SIZE * SIZE);
                        Swap(i, rand);
                    }
                    MakeSlovable();
                    break;
                default:
                    break;
            }
        }

        public int this[int i]
        {
            get { return Matrix[i / SIZE, i % SIZE]; }
            set { Matrix[i / SIZE, i % SIZE] = value; }
        }
        public void LeftKey(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Rotation_90_deg();
            Rotation_90_deg();
            RightMove();
            Rotation_90_deg();
            Rotation_90_deg();
        }
        public void UpKey(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Rotation_90_deg();
            RightMove();
            Rotation_90_deg();
            Rotation_90_deg();
            Rotation_90_deg();
        }
        public void RightKey(object sender, System.Windows.Input.KeyEventArgs e)
        {
            RightMove();
        }
        public void DownKey(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Rotation_90_deg();
            Rotation_90_deg();
            Rotation_90_deg();
            RightMove();
            Rotation_90_deg();
        }

        void RightMove()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE - 1; j++)
                {
                    if (Matrix[i, j] == 0)
                    {
                        Swap(ref Matrix[i, j], ref Matrix[i, j + 1]);
                        break;
                    }
                }
            }
        }

        List<Moves> AvalibleMoves()
        {
            List<Moves> result = new List<Moves> { Moves.Left, Moves.Up, Moves.Right, Moves.Down };
            int i = 0;
            for (; i < SIZE * SIZE; i++)
                if (this[i] == 0)
                    break;
            if (i % SIZE == 0)
                result.Remove(Moves.Left);
            if (i % SIZE == SIZE - 1)
                result.Remove(Moves.Right);
            if (i / SIZE == 0)
                result.Remove(Moves.Up);
            if (i / SIZE == SIZE - 1)
                result.Remove(Moves.Down);
            return result;
        }
        void RandomMove()
        {
            List<Moves> avalibleMoves = AvalibleMoves();
            int rand = random.Next(avalibleMoves.Count());
            switch (avalibleMoves[rand])
            {
                case Moves.Left:
                    LeftKey(this, null);
                    break;
                case Moves.Up:
                    UpKey(this, null);
                    break;
                case Moves.Right:
                    RightKey(this, null);
                    break;
                case Moves.Down:
                    DownKey(this, null);
                    break;
                default:
                    break;
            }

        }
        void Rotation_90_deg()
        {
            int[,] NewMatrix = new int[SIZE, SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    NewMatrix[i, j] = Matrix[SIZE - 1 - j, i];
                }
            }
            this.Matrix = NewMatrix;
        }
        void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }
        void Swap(int index_i, int index_j)
        {
            Swap(ref Matrix[index_i / SIZE, index_i % SIZE], ref Matrix[index_j / SIZE, index_j % SIZE]);
        }

        int getInvCount()
        {
            int inv_count = 0;
            for (int i = 0; i < SIZE * SIZE - 1; i++)
            {
                for (int j = i + 1; j < SIZE * SIZE; j++)
                {
                    if (this[j] != 0 && this[i] != 0 && this[i] > this[j])
                        inv_count++;
                }
            }
            return inv_count;
        }

        // find Position of blank from bottom 
        int rowOfBlank()
        {
            int i = 0;
            for (; i < SIZE * SIZE - 1; i++)
            {
                if (this[i] == 0)
                    break;
            }
            return SIZE - i / SIZE;
        }

        // This function returns true if given 
        // instance of N*N - 1 puzzle is solvable 
        bool isSolvable()
        {
            // Count inversions in given puzzle 
            int invCount = getInvCount();

            // If grid is odd, return true if inversion 
            // count is even. 
            if (SIZE % 2 == 1)
                return (invCount % 2 == 0);

            else     // grid is even 
            {
                int position = rowOfBlank();
                if (position % 2 == 1)
                    return (invCount % 2 == 0);
                else
                    return invCount % 2 == 1;
            }
        }

        void MakeSlovable()
        {
            if (isSolvable())
                return;
            if (this[0] != 0 && this[1] != 0)   //else - swap the 2 first non-zero
                Swap(0, 1);
            else if (this[0] != 0)
                Swap(0, 2);
            else 
                Swap(1, 2);
        }

        public bool Win()
        {
            for (int i = 0; i < SIZE * SIZE - 1; i++)
                if (this[i] != i + 1)
                    return false;
            return true;
        }

    }
}
