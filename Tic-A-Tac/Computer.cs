using System;

namespace Tic_A_Tac
{
        class Computer : GameObject
        {
                private PlayerMarker marker;
                public int nextMove;

                public Computer(PlayerMarker marker)
                {
                        this.marker = marker;
                }

                public void ChooseCoord(Board board)
                {
                        Random random = new();
                        List<int> available = board.GetAvailableMoves();
                        int guess = random.Next(available.Count);
                        nextMove = available[guess];
                }


        }
}
