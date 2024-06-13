using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace ContentAwareResize
{
  // *****************************************
  // *****************************************
  public class ContentAwareResize
  {
    public struct coord
    {
      public int row;
      public int column;
    }
    //========================================================================================================
    /// <summary>
    /// Develop an efficient algorithm to get the minimum vertical seam to be removed
    /// </summary>
    /// <param name="energyMatrix">2D matrix filled with the calculated energy for each pixel in the image</param>
    /// <param name="Width">Image's width</param>
    /// <param name="Height">Image's height</param>
    /// <returns>BY REFERENCE: The min total value (energy) of the selected seam in "minSeamValue" & List of points of the selected min vertical seam in seamPathCoord</returns>

	public bool IsCorrect(int Width, int Height, int i, int j) {
    	return (0 <= i && i < Height) && (0 <= j && j < Width);
	}

	public void FindMinimum(ref int[,] dp, int Width, int Height, int cur_i, int cur_j, ref int min_i, ref int  min_j) {
		int n1, n2, n3;

		n1 = IsCorrect(Width, Height, cur_i - 1, cur_j - 1) ? dp[cur_i - 1, cur_j - 1] : int.MaxValue; // upper-left neighbor 
		n2 = IsCorrect(Width, Height, cur_i - 1, cur_j) ? dp[cur_i - 1, cur_j] : int.MaxValue; // direct-above neighbor 
		n3 = IsCorrect(Width, Height, cur_i - 1, cur_j + 1) ? dp[cur_i - 1, cur_j + 1] : int.MaxValue; // upper-right neighbor 

		int min_n = n1;
		min_i = cur_i - 1;
		min_j = cur_j - 1;
		if (n2 < min_n)
		{
			min_n = n2;
			min_j = cur_j;	
		}

		if (n3 < min_n)
		{
			min_n = n3;
			min_j = cur_j + 1;
		}
			
	}
    /*
         old function:
            Time Complexity: w * h
            Memory Complexity: w * h^2 = h^3
            =================================
            public void CalculateSeamsCost(int[,] energyMatrix, int Width, int Height, ref int minSeamValue, ref List<coord> seamPathCoord) {
        // Write your code here 

		// dp is a 2D matrix. Each cell has a minimized cost that maps to its corresponding vertical path 
		Tuple<int, List<coord>>[,] dp = new Tuple<int, List<coord>>[Height, Width];
		
		// initialize the 1st row
		for (int j = 0; j < Width; j++) { 
			dp[0, j] = Tuple.Create(energyMatrix[0, j], new List<coord>());
			dp[0, j].Item2.Add(new coord { row = 0, column = j });
		}
		
		// build the dp matrix (tabulation)
		for (int i = 1; i < Height; i++) { // starts from the 2nd row
			for (int j = 0; j < Width; j++) {
				// Find the cell that has the minimum energy of the 3 neighbors above the current cell
				int min_i = 0;
				int min_j = 0;
				FindMinimum(ref dp, Width, Height, i, j, ref min_i, ref min_j);
				dp[i, j] = Tuple.Create(dp[min_i, min_j].Item1 + energyMatrix[i, j], new List<coord>(dp[min_i, min_j].Item2));
				dp[i, j].Item2.Add(new coord { row = i, column = j });
			}
		}


		// find the minimum cost in the last row (the correct answer)
		minSeamValue = dp[Height-1, 0].Item1;
		seamPathCoord = dp[Height-1, 0].Item2;
		for (int j = 1; j < Width; j++) {
			if (dp[Height-1, j].Item1 < minSeamValue) {
				minSeamValue = dp[Height-1, j].Item1;
				seamPathCoord = dp[Height-1, j].Item2;
			}
		}
  }
            =================================
         new function: 
            
              Time Complexity: O(w * h)
              Memory Complexity: O(w * h + h)
			
			  Deeper analysis on the difference between the memory complexity of CalculateSeamsCost() and CalculateSeamsCost2() functions:
			
				Assumption: w will be equal to a small factor * h, so it will belong to a range like [0.5 * h, 2 * h]
			
			  	CalculateSeamsCost():
					Memory Complexity: O(w * h^2) ~ O(h^3) 
			
				CalculateSeamsCost2(): 
					Memory Complexity: O(w * h + h) ~ O(h^2)
            
       */

	
    // improved function CalculateSeamsCost2
    public void CalculateSeamsCost(int[,] energyMatrix, int Width, int Height, ref int minSeamValue, ref List<coord> seamPathCoord) {
        // Write your code here
        
        // dp is a 2D matrix. Each cell has a minimized cost that maps to its corresponding vertical path 
        int[,] dp = new int[Height, Width];
		
        // initialize the 1st row
        for (int j = 0; j < Width; j++)  
            dp[0, j] = energyMatrix[0, j];

        int min_i = 0;
        int min_j = 0;
        
        // build the dp matrix (tabulation)
        for (int i = 1; i < Height; i++) { // starts from the 2nd row
            for (int j = 0; j < Width; j++) {
                // Find the cell that has the minimum energy of the 3 neighbors above the current cell
                min_i = 0;
                min_j = 0;
                FindMinimum(ref dp, Width, Height, i, j, ref min_i, ref min_j);
                dp[i, j] = dp[min_i, min_j] + energyMatrix[i, j];
            }
        }

        // find the minimum cost in the last row (the correct answer)
        min_i = Height-1;
        min_j = 0;
        minSeamValue = dp[min_i, min_j];
        for (int j = 1; j < Width; j++) {
            if (dp[min_i, j] < minSeamValue) {
                minSeamValue = dp[min_i, j];
                min_j = j;
            }
        }

        // build the whole path starting from minSeamValue index
        seamPathCoord = new List<coord>();
        seamPathCoord.Add(new coord { row = min_i, column = min_j });
        for (int i = Height-2; i >= 0; i--) { // assumptions: Height >= 2
            FindMinimum(ref dp, Width, Height, min_i, min_j, ref min_i, ref min_j);
            seamPathCoord.Add(new coord { row = min_i, column = min_j });
        }
    }

    // *****************************************
    // *****************************************

    public MyColor[,] _imageMatrix;
    public int[,] _energyMatrix;
    public int[,] _verIndexMap;
    public ContentAwareResize(string ImagePath)
    {
      _imageMatrix = ImageOperations.OpenImage(ImagePath);
      _energyMatrix = ImageOperations.CalculateEnergy(_imageMatrix);
      int _height = _energyMatrix.GetLength(0);
      int _width = _energyMatrix.GetLength(1);
    }
    public void CalculateVerIndexMap(int NumberOfSeams, ref int minSeamValueFinal, ref List<coord> seamPathCoord)
    {
      int Width = _imageMatrix.GetLength(1);
      int Height = _imageMatrix.GetLength(0);

      int minSeamValue = -1;
      _verIndexMap = new int[Height, Width];
      for (int i = 0; i < Height; i++)
        for (int j = 0; j < Width; j++)
          _verIndexMap[i, j] = int.MaxValue;

      bool[] RemovedSeams = new bool[Width];
      for (int j = 0; j < Width; j++)
        RemovedSeams[j] = false;

      for (int s = 1; s <= NumberOfSeams; s++)
      {
        CalculateSeamsCost(_energyMatrix, Width, Height, ref minSeamValue, ref seamPathCoord);
        minSeamValueFinal = minSeamValue;

        //Search for Min Seam # s
        int Min = minSeamValue;

        //Mark all pixels of the current min Seam in the VerIndexMap
        if (seamPathCoord.Count != Height)
          throw new Exception("You selected WRONG SEAM");
        for (int i = Height - 1; i >= 0; i--)
        {
          if (_verIndexMap[seamPathCoord[i].row, seamPathCoord[i].column] != int.MaxValue)
          {
            string msg = "overalpped seams between seam # " + s + " and seam # " + _verIndexMap[seamPathCoord[i].row, seamPathCoord[i].column];
            throw new Exception(msg);
          }
          _verIndexMap[seamPathCoord[i].row, seamPathCoord[i].column] = s;
          //remove this seam from energy matrix by setting it to max value
          _energyMatrix[seamPathCoord[i].row, seamPathCoord[i].column] = 100000;
        }

        //re-calculate Seams Cost in the next iteration again
      }
    }
    public void RemoveColumns(int NumberOfCols)
    {
      int Width = _imageMatrix.GetLength(1);
      int Height = _imageMatrix.GetLength(0);
      _energyMatrix = ImageOperations.CalculateEnergy(_imageMatrix);

      int minSeamValue = 0;
      List<coord> seamPathCoord = null;
      //CalculateSeamsCost(_energyMatrix,Width,Height,ref minSeamValue, ref seamPathCoord);
      CalculateVerIndexMap(NumberOfCols, ref minSeamValue, ref seamPathCoord);

      MyColor[,] OldImage = _imageMatrix;
      _imageMatrix = new MyColor[Height, Width - NumberOfCols];
      for (int i = 0; i < Height; i++)
      {
        int cnt = 0;
        for (int j = 0; j < Width; j++)
        {
          if (_verIndexMap[i, j] == int.MaxValue)
            _imageMatrix[i, cnt++] = OldImage[i, j];
        }
      }

    }
    #endregion
  }
}
