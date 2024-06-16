# ContentAwareResize_GUI
## Table of Contents
- [Abstract](#abstract)
- [Introduction](#introduction)
- [Methodology](#methodology)
- [Results](#results)
- [Responsibilities](#responsibilities)
- [Acknowledgements](acknowledgements)
- [Conclusion](#conclusion)

## Abstract
This project develops an efficient algorithm for identifying the minimum vertical seam to be removed from an image. 
The algorithm is crucial for image resizing and content-aware image editing applications. 
We present a detailed analysis of the algorithm's performance, including its time and memory complexities. 
Our findings demonstrate the effectiveness of the proposed algorithm in minimizing seam removal while maintaining image quality. 

## Introduction
Image resizing is a fundamental operation in image processing, with applications ranging from web design to medical imaging. 
Traditional resizing methods often lead to distortion or loss of important image features. 
To address this issue, content-aware resizing techniques have been developed, which involve identifying and removing seams with low energy from the image. 
In this project, we aim to improve the efficiency of the seam removal process by devising an algorithm to find the minimum vertical seam in an image.

## Methodology
We implemented an algorithm to calculate the minimum vertical seam using dynamic programming. 
The algorithm traverses the image from top to bottom, computing the cumulative energy of each pixel based on its local neighborhood. 
We created two versions of the algorithm and then compared them, analyzing their time and memory complexities. 
The implementation was done in C# programming language.

## Results
Our experiments show that the 2nd version achieved a significant improvement in efficiency compared to the previous one. 
The space complexity of the new algorithm is $O(w * h + h) \approx O(h^2)$, where w is the width and h is the height of the image. 
This represents a notable reduction compared to the previous implementation, which had a space complexity of $O(w * h^2) \approx O(h^3)$. 
We provide detailed performance metrics and visualizations to illustrate the algorithm's effectiveness in identifying the minimum vertical seam.
 - ### Example:
   ![Example 2](https://github.com/MohamedMostafa259/ContentAwareResize_GUI/blob/main/Examples/Example2.png)

## Responsibilities
I developed two versions of the algorithm, but my teaching assistants, in the Algorithms and Design Analysis course at Egypt University of Informatics, provided the remaining files (The only file I edited is [ContentAwareResize.cs](https://github.com/MohamedMostafa259/ContentAwareResize_GUI/blob/main/ContentAwareResize/ContentAwareResize.cs)).

## Acknowledgements
I would like to thank my colleague, [Mohamed Ibrahim](https://github.com/22-101058), for his assistance and support in this project.

## Conclusion
In conclusion, our study presents a novel algorithm for efficiently identifying the minimum vertical 
seam in images. By leveraging dynamic programming techniques, we implemented a very efficient algorithm in terms of memory and time complexities. 
The proposed algorithm holds promise for enhancing various image processing applications, particularly those requiring content-aware resizing. 
 - ### Project Demo:
   [click here](https://drive.google.com/file/d/1RDOiqiy65Fe-iIIqBnbIfcEI6h8fVrug/view?usp=drive_link) to see the 9-minute project demo.
   My part is from $5:21$ to the end, explaining why my 2nd version of the algorithm is more efficient than the 1st one.
   An example of the program's output is from $5:05$ to $5:18$
