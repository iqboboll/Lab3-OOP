using System;
using System.Collections.Generic;

public class Vector
{
    private int size;
    private double[] data;

    public Vector(int size)
    {
        this.size = size;
        data = new double[size];
    }

    public int getSize() => this.size;

    public void setElement(int index, double val)
    {
        data[index] = val;
    }

    public double getElement(int index)
    {
        return data[index];
    }
}

public class Matrix
{
    private int row;
    private int column;
    private double[,] data;

    public Matrix(int r, int c)
    {
        row = r;
        column = c;
        data = new double[r, c];
    }

    public int getRow() => this.row;
    public int getColumn() => this.column;

    public double getElement(int r, int c) => this.data[r, c];

    public void setElement(int r, int c, double val)
    {
        this.data[r, c] = val;
    }
}

public class MatrixRepository
{
    private List<Matrix> matrices = new List<Matrix>();
    public void addMatrix(Matrix m) => this.matrices.Add(m);
    public Matrix getMatrix(int index) => this.matrices[index];
}

public class VectorRepository
{
    private List<Vector> vectors = new List<Vector>();
    public void addVector(Vector v) => this.vectors.Add(v);
    public Vector getVector(int index) => this.vectors[index];
}

public class DimensionValidator
{
    public bool checkCompatibility(Matrix m1, Matrix m2)
    {
        return m1.getRow() == m2.getRow() && m1.getColumn() == m2.getColumn();
    }

    public bool checkCompatibility(Vector v1, Vector v2)
    {
        return v1.getSize() == v2.getSize();
    }
}

public class Calculator
{
    private DimensionValidator validator = new DimensionValidator();

    // --- Addition ---
    public Vector Add(Vector v1, Vector v2)
    {
        if (!this.validator.checkCompatibility(v1, v2)) return null;
        Vector vect = new Vector(v1.getSize());
        for (int i = 0; i < v1.getSize(); i++)
            vect.setElement(i, v1.getElement(i) + v2.getElement(i));
        return vect;
    }

    public Matrix Add(Matrix m1, Matrix m2)
    {
        if (!this.validator.checkCompatibility(m1, m2)) return null;
        Matrix mat = new Matrix(m1.getRow(), m1.getColumn());
        for (int i = 0; i < m1.getRow(); i++)
            for (int j = 0; j < m1.getColumn(); j++)
                mat.setElement(i, j, m1.getElement(i, j) + m2.getElement(i, j));
        return mat;
    }

    // --- Subtraction ---
    public Vector Subtract(Vector v1, Vector v2)
    {
        if (!this.validator.checkCompatibility(v1, v2)) return null;
        Vector vect = new Vector(v1.getSize());
        for (int i = 0; i < v1.getSize(); i++)
            vect.setElement(i, v1.getElement(i) - v2.getElement(i));
        return vect;
    }

    public Matrix Subtract(Matrix m1, Matrix m2)
    {
        if (!this.validator.checkCompatibility(m1, m2)) return null;
        Matrix mat = new Matrix(m1.getRow(), m1.getColumn());
        for (int i = 0; i < m1.getRow(); i++)
            for (int j = 0; j < m1.getColumn(); j++)
                mat.setElement(i, j, m1.getElement(i, j) - m2.getElement(i, j));
        return mat;
    }

    public void displayResult(Vector v)
    {
        if (v == null) { Console.WriteLine("Operation failed: Incompatible dimensions."); return; }
        Console.Write("[ ");
        for (int i = 0; i < v.getSize(); i++) Console.Write(v.getElement(i) + " ");
        Console.WriteLine("]");
    }

    public void displayResult(Matrix m)
    {
        if (m == null) { Console.WriteLine("Operation failed: Incompatible dimensions."); return; }
        for (int i = 0; i < m.getRow(); i++)
        {
            for (int j = 0; j < m.getColumn(); j++) Console.Write(m.getElement(i, j) + "\t");
            Console.WriteLine();
        }
    }
}

class Program
{
    static MatrixRepository MatrixRepo = new MatrixRepository();
    static VectorRepository vectorRepo = new VectorRepository();
    static Calculator calc = new Calculator();

    static void Main(string[] args)
    {
        Console.WriteLine("---- VECTOR OPERATIONS ----");
        captureVectorInput(1);
        captureVectorInput(2);

        Vector v1 = vectorRepo.getVector(0);
        Vector v2 = vectorRepo.getVector(1);

        Console.Write("\nVector Addition: ");
        calc.displayResult(calc.Add(v1, v2));
        Console.Write("Vector Subtraction: ");
        calc.displayResult(calc.Subtract(v1, v2));

        Console.WriteLine("\n---------------------------\n");

        Console.WriteLine("---- MATRIX OPERATIONS ----");
        captureMatrixInput(1);
        captureMatrixInput(2);

        Matrix m1 = MatrixRepo.getMatrix(0);
        Matrix m2 = MatrixRepo.getMatrix(1);

        Console.WriteLine("\nMatrix Addition:");
        calc.displayResult(calc.Add(m1, m2));
        Console.WriteLine("\nMatrix Subtraction:");
        calc.displayResult(calc.Subtract(m1, m2));

        Console.WriteLine("\nTask complete. Press any key to exit.");
        Console.ReadKey();
    }

    static void captureVectorInput(int num)
    {
        Console.WriteLine($"\nInput for Vector {num}:");
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine());
        Vector v = new Vector(size);
        for (int i = 0; i < size; i++)
        {
            Console.Write($" Element {i}: ");
            v.setElement(i, double.Parse(Console.ReadLine()));
        }
        vectorRepo.addVector(v);
    }

    static void captureMatrixInput(int num)
    {
        Console.WriteLine($"\nInput for Matrix {num}:");
        Console.Write("Rows: ");
        int r = int.Parse(Console.ReadLine());
        Console.Write("Columns: ");
        int c = int.Parse(Console.ReadLine());
        Matrix m = new Matrix(r, c);
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                Console.Write($" Element [{i},{j}]: ");
                m.setElement(i, j, double.Parse(Console.ReadLine()));
            }
        }
        MatrixRepo.addMatrix(m);
    }
}