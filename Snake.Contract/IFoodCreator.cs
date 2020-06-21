using Snake.Contract.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Contract
{
    public interface IFoodCreator
    {
        Food CreateFoodPiece(int borderHeight, int borderWidth);
    }
}
